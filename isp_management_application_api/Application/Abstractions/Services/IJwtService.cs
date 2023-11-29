using System.Security.Claims;

namespace Application.Abstractions.Services
{
    public interface IJwtService : ITransientService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool IsPasswordVerified(string password, byte[] passwordHash, byte[] passwordSalt);
        string BuildToken(List<Claim> claims, DateTime expiredTime);
    }
}
