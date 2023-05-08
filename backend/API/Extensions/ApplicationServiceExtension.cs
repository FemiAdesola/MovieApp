using API.Helper;
using API.Services.Implementations;
using API.Services.Interface;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
     
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IFileStorage, AzureFileStorage>();
            // services.AddScoped<IFileStorage, LocalFileStorage>();
            services.AddHttpContextAccessor();
              services.AddSingleton<GeometryFactory>(
                NtsGeometryServices
                .Instance
                .CreateGeometryFactory(srid:4326));
            return services;
        }
    }
}