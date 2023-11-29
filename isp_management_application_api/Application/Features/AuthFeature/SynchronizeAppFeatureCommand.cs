using Domain.Attributes;
using Domain.Entities;
using Domain.Enums;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthFeature;

public class SynchronizeAppFeatureCommand : ICommand<IApiResult>
{
    internal class SynchronizeAppFeatureCommandHandler : IRequestHandler<SynchronizeAppFeatureCommand, IApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SynchronizeAppFeatureCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IApiResult> Handle(SynchronizeAppFeatureCommand request, CancellationToken cancellationToken)
        {
            var allExistingFeatures = await _unitOfWork.AppFeature.Queryable
                .ToListAsync();

            var appFeature = Enum.GetValues(typeof(AppFeaturesEnum))
                .Cast<AppFeaturesEnum>()
                .Select(x => new AppFeature
                {
                    Id = (int)x,
                    FeatureName = x.GetAttribute<AppFeatureAttribute>().FeatureName,
                    Description = x.GetAttribute<AppFeatureAttribute>().Description
                }).ToList();

            if (allExistingFeatures.Count == appFeature.Count)
                return ApiResult.Success("Sync has been successfully completed. No change found.");

            List<int> ids = allExistingFeatures.Select(x => x.Id)
                .ToList();

            var addableFeature = appFeature
                .Where(x => !ids.Contains(x.Id))
                .ToList();

            await _unitOfWork.AppFeature.AddRangeAsync(addableFeature);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResult.Success($"Sync has been successfully completed. {addableFeature.Count} newly added.");
        }
    }
}
