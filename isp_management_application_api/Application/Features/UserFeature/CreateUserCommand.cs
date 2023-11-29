using Application.Abstractions.Services;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Features.UserFeature;

public class CreateUserCommand : IQuery<IApiResult>
{
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public int? RoleId { get; set; }
    public string LoginName { get; set; } = string.Empty;
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<IApiResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var isLoginNameExist = await _unitOfWork.User.IsExsitAsync(x => x.LoginName.ToLower() == request.LoginName.ToLower().Trim());
                if (isLoginNameExist) return ApiResult.Fail("Login name is already exist.");

            }
            catch (Exception ex)
            {

                throw;
            }

            var isUserEmailExist = await _unitOfWork.User.IsExsitAsync(x => x.UserEmail.ToLower() == request.UserEmail.ToLower().Trim());
            if (isUserEmailExist) return ApiResult.Fail("One user is already registered with this email.");

            var isUserMobileExist = await _unitOfWork.User.IsExsitAsync(x => x.UserMobileNumber.Trim().ToLower() == request.MobileNumber.Trim().ToLower());
            if (isUserMobileExist) return ApiResult.Fail("One user is already registered with this mobile number.");

            // Generate Password Hash & Salt
            _jwtService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                UserName = request.UserName,
                UserEmail = request.UserEmail.Trim(),
                LoginName = request.LoginName.Trim(),
                UserMobileNumber = request.MobileNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = null                
            };

            await _unitOfWork.User.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return ApiResult.Success("User created successfully.");
        }
    }
}
