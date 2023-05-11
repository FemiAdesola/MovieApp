using System.Text;
using API.Database;
using API.Helper;
using API.Services.Implementations;
using API.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
            services.AddSingleton(provider => new MapperConfiguration(config =>
            {
                var geometryFactor = provider.GetRequiredService<GeometryFactory>();
                config.AddProfile(new MappingProfile(geometryFactor));
            }).CreateMapper());
            
             services.AddScoped<IAuthenticationRepo, AuthenticationRepo>();
            services.AddSingleton<GeometryFactory>(
                NtsGeometryServices
                .Instance
                .CreateGeometryFactory(srid:4326));
            services.AddHttpContextAccessor();

            // for authetication services
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            //

            return services;
        }
    }
}