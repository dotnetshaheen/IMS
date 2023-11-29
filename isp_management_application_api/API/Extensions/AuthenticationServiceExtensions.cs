using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class AuthenticationServiceExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(x =>
             {
                 var signingSecretkey = Encoding.UTF8.GetBytes(configuration.GetSection("JwtConfiguration:SigningSecretKey").Value!);
                 var encryptionSecretkey = Encoding.UTF8.GetBytes(configuration.GetSection("JwtConfiguration:EncriptionSecretKey").Value!);

                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(signingSecretkey),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     TokenDecryptionKey = new SymmetricSecurityKey(encryptionSecretkey)
                 };
                 x.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException) && context.Response.StatusCode == 401)
                         {
                             context.Response.Headers.Add("Access-Control-Expose-Headers", "Token-Expired");
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                 };
             });
            return services;
        }
    }
}
