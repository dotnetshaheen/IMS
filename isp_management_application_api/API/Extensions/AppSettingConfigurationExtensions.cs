using Shared.OptionDtos;

namespace API.Extensions
{
    public static class AppSettingConfigurationExtensions
    {
        public static IServiceCollection AddAppSettingsConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection(nameof(JwtConfiguration)));
            return services;
        }
    }
}
