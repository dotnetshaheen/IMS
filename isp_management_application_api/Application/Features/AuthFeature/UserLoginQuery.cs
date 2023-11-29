using Application.Abstractions.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Features.AuthFeature;

public class UserLoginQuery : IQuery<IApiResult>
{
    public string LoginName { get; set; }
    public string Password { get; set; } = string.Empty;
    internal class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, IApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        public UserLoginQueryHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<IApiResult> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.User.Queryable
                .Where(x => x.LoginName.Trim().ToLower() == request.LoginName.ToLower().Trim())
                .FirstOrDefaultAsync();

            bool isInvalidUser = currentUser is null or { IsActive: 0 } or { IsDeleted: 1 };
            if (isInvalidUser)
            {
                return ApiResult.Fail("Login Access Deny.");
            }

            bool isPasswordValid = _jwtService.IsPasswordVerified(request.Password, currentUser.PasswordHash, currentUser.PasswordSalt);
            if (!isPasswordValid)
            {
                return ApiResult.Fail("Invalid Username or Password");
            }

            var exp = DateTime.Now.AddHours(6);
            var tokenExpiredTime = DateTimeOffset.Parse(exp.ToString()).ToUnixTimeSeconds();

            var claims = new List<Claim>
            {
                new Claim("uid", currentUser.Id.ToString()),
                new Claim("rid", "0"),
                new Claim("exp", tokenExpiredTime.ToString())
            };

            var token = _jwtService.BuildToken(claims, exp);

            var response = new
            {
                Token = token
            };

            return ApiResult<dynamic>.Success(response);
        }
    }
}
