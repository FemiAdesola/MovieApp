// using System.IdentityModel.Tokens.Jwt;
// using System.Text;
// using API.Database;
// using API.EXception;
// using API.Services.Implementations;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
// using API.Services.Interface;
// using API.Extensions;

// namespace API
// {
//     public class Startup
//     {
//         public Startup(IConfiguration configuration)
//         {
//             JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
//             Configuration = configuration;
//         }

//         public IConfiguration Configuration { get; }
//         public void ConfigureServices(IServiceCollection services)
//         {
//             services.AddDbContext<AppDbContext>();
//             services.AddScoped<IFileStorage, LocalFileStorage>();
//             services.AddHttpContextAccessor();

//             services.AddControllers(options =>
//             {
                
//                 options.Filters.Add(typeof(MyException));
//                 options.Filters.Add(typeof(ParseBadRequest));
//             }).ConfigureApiBehaviorOptions(BadRequestBehavior.Parse);

//           services.AddApplicationServices(Configuration);
//             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(options =>
//                {
//                    options.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        ValidateIssuer = false,
//                        ValidateAudience = false,
//                        ValidateLifetime = true,
//                        ValidateIssuerSigningKey = true,
//                        IssuerSigningKey = new SymmetricSecurityKey(
//                            Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"]!)),
//                        ClockSkew = TimeSpan.Zero
                    
//                    };
//                });

//             services.AddAuthorization(options =>
//             {
//                 options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "admin"));
//             });

//             services.AddSwaggerGen();
//             services.AddSwaggerDocumentation();

//             services.AddCors(options =>
//                 {
//                     options.AddPolicy("CorsPolicy", builder =>
//                         {
//                             builder
//                                .WithOrigins("http://localhost:3000")
//                               .AllowAnyOrigin()
//                               .AllowAnyHeader()
//                               .AllowAnyMethod()
//                               .WithExposedHeaders(new string[] { "totalAmountOfRecords" });
//                         });
//                 });
//         }
//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//         {
//             //Configure the HTTP request pipeline.
//                 if (env.IsDevelopment())
//                 {
//                     app.UseSwagger();
//                     app.UseSwaggerUI();
//                 }

//             app.UseSwaggerDocumentation();
//             app.UseHttpsRedirection();
//             app.UseStaticFiles();
//             app.UseRouting();
//             app.UseCors("CorsPolicy");
//             app.UseAuthentication();
//             app.UseAuthorization();
//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllers();
//             });
//         }
//     }
// }

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using API.Database;
using API.EXception;
using API.Services.Implementations;
using API.Services.Interface;
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Database
            services.AddDbContext<AppDbContext>();

            // Dependency Injection
            services.AddScoped<IFileStorage, LocalFileStorage>();
            services.AddHttpContextAccessor();

            // Controllers + Exception Filters
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(MyException));
                options.Filters.Add(typeof(ParseBadRequest));
            }).ConfigureApiBehaviorOptions(BadRequestBehavior.Parse);

            // Custom application services
            services.AddApplicationServices(Configuration);

            // JWT Authentication
            var secret = Configuration["AppSettings:Secret"];
            if (string.IsNullOrEmpty(secret))
            {
                throw new Exception("Missing AppSettings:Secret configuration value.");
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            // Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "admin"));
            });

            // Swagger
            services.AddSwaggerGen();
            services.AddSwaggerDocumentation();

            // ✅ Dynamic CORS setup
            var frontendUrl = Configuration["FRONTEND_URL"] ?? "http://localhost:3003";

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .WithOrigins(frontendUrl)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("totalAmountOfRecords");
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Swagger (enabled in both Dev & Prod)
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // ✅ Apply dynamic CORS
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Console.WriteLine($"✅ App started in {env.EnvironmentName} mode");
            Console.WriteLine($"Frontend URL: {Configuration["FRONTEND_URL"]}");
            Console.WriteLine($"Database: {Configuration.GetConnectionString("DefaultConnection")}");
        }
    }
}
