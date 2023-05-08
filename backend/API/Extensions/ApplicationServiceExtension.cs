using API.Helper;
using API.Services.Implementations;
using API.Services.Interface;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
     
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IFileStorage, AzureFileStorage>();
            return services;
        }
    }
}