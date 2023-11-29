using System.Text.Json.Serialization;
using System.Text.Json;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvcController();
            services.AddCurrentUserService();            
            services.AddMiscellaneousService(configuration);
            return services;
        }
        private static IServiceCollection AddMvcController(this IServiceCollection services)
        {
            //services.AddControllers()
            //    .AddMvcOptions(option =>
            //    {
            //        option.AllowEmptyInputInBodyModelBinding = true;
            //        option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            //    })
            //    .AddJsonOptions(option =>
            //    {
            //        option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //        option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            //    })
            //    .ConfigureApiBehaviorOptions(option =>
            //    {
            //        option.InvalidModelStateResponseFactory = context =>
            //        {
            //            var errorsInModelState = context.ModelState
            //                .Where(x => x.Value.Errors.Count > 0)
            //                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray());
            //            var errors = errorsInModelState.Select(x => x.Value[0]).ToList();

            //            var validationError = ApiResult.ValidationError(errors);
            //            var result = new ObjectResult(validationError);
            //            result.StatusCode = validationError.StatusCode;
            //            return result;
            //        };
            //    });

            return services;
        }
        private static IServiceCollection AddCurrentUserService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            return services;
        }
       
        private static IServiceCollection AddMiscellaneousService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppSettingsConfigurations(configuration);
            return services;
        }
    }
}
