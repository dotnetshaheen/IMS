using Application.Abstractions.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.OptionDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfiguration _options; 
        public JwtService(IOptions<JwtConfiguration> options)
        {
            _options = options.Value;
        }

        public string BuildToken(List<Claim> claims, DateTime expiredTime)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SigningSecretKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(_options.EncriptionSecretKey);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                Expires = expiredTime,
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }        

        public bool IsPasswordVerified(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
