using Domain.Attributes;
using Domain.Entities;
using Domain.Enums;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthFeature;

public class SynchoronizeRightsCommand : ICommand<IApiResult>
{
    internal class SynchoronizeRightsCommandHandler : IRequestHandler<SynchoronizeRightsCommand, IApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SynchoronizeRightsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IApiResult> Handle(SynchoronizeRightsCommand request, CancellationToken cancellationToken)
        {
            var existingRights = await _unitOfWork.Right.Queryable
                .ToListAsync();

            var rights = Enum.GetValues(typeof(RightsEnum))
                .Cast<RightsEnum>()
                .Select(x => new Right
                {
                    Id = (int)x,
                    RightsName = x.ToString().Replace("_"," "),
                    AppFeatureId = x.GetAttribute<RightAttribute>().AppFeatureId                    
                })
                .ToList();

            List<int> ids = existingRights.Select(x => x.Id)
                .ToList();

            var addableRights = rights
                .Where(x => !ids.Contains(x.Id))
                .ToList();

            if (!addableRights.Any())
            {
                return ApiResult.Success("Sync has been successfully completed. No change found.");
            }


            var globalRoles = await _unitOfWork.Role.Queryable
                .Where(x => x.IsGlobalRole == 1)
                .ToListAsync();

            // Transaction 
            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                if (!globalRoles.Any())
                {
                    var role = new Role
                    {
                        RoleName = "Super Admin",
                        IsGlobalRole = 1
                    };

                    await _unitOfWork.Role.AddAsync(role);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    globalRoles.Add(role);
                }

                var roleRightMapping = new List<RoleRight>();

                foreach (var role in globalRoles)
                {
                    foreach (var right in addableRights)
                    {
                        var roleRight = new RoleRight
                        {
                            RoleId = role.Id,
                            RightsId = right.Id
                        };
                        roleRightMapping.Add(roleRight);
                    }
                }

                await _unitOfWork.Right.AddRangeAsync(rights);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.RoleRight.AddRangeAsync(roleRightMapping);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return ApiResult.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return ApiResult.Fail();
            }                       
        }
    }
}
