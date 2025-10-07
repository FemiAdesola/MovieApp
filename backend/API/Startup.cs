using System.IdentityModel.Tokens.Jwt;
using System.Text;
using API.Database;
using API.EXception;
using API.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using API.Services.Interface;
using API.Extensions;

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
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IFileStorage, LocalFileStorage>();
            services.AddHttpContextAccessor();

            services.AddControllers(options =>
            {

                options.Filters.Add(typeof(MyException));
                options.Filters.Add(typeof(ParseBadRequest));
            }).ConfigureApiBehaviorOptions(BadRequestBehavior.Parse);

          services.AddApplicationServices(Configuration);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"]!)),
                       ClockSkew = TimeSpan.Zero

                   };
               });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "admin"));
            });

            services.AddSwaggerGen();
            services.AddSwaggerDocumentation();

            services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy", builder =>
                        {
                            builder
                               .WithOrigins("http://localhost:3000")
                              .AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .WithExposedHeaders(new string[] { "totalAmountOfRecords" });
                        });
                });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Configure the HTTP request pipeline.
                if (env.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

            app.UseSwaggerDocumentation();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}




// // using System.IdentityModel.Tokens.Jwt;
// // using System.Text;
// // using API.Database;
// // using API.EXception;
// // using API.Services.Implementations;
// // using API.Services.Interface;
// // using API.Extensions;
// // using Microsoft.AspNetCore.Authentication.JwtBearer;
// // using Microsoft.IdentityModel.Tokens;

// // namespace API
// // {
// //     public class Startup
// //     {
// //         public Startup(IConfiguration configuration)
// //         {
// //             JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
// //             Configuration = configuration;
// //         }

// //         public IConfiguration Configuration { get; }

// //         public void ConfigureServices(IServiceCollection services)
// //         {
// //             // Database
// //             services.AddDbContext<AppDbContext>();

// //             // Dependency Injection
// //             services.AddScoped<IFileStorage, LocalFileStorage>();
// //             services.AddHttpContextAccessor();

// //             // Controllers + Exception Filters
// //             services.AddControllers(options =>
// //             {
// //                 options.Filters.Add(typeof(MyException));
// //                 options.Filters.Add(typeof(ParseBadRequest));
// //             }).ConfigureApiBehaviorOptions(BadRequestBehavior.Parse);

// //             // Custom application services
// //             services.AddApplicationServices(Configuration);

// //             // JWT Authentication
// //             var secret = Configuration["AppSettings:Secret"];
// //             if (string.IsNullOrEmpty(secret))
// //             {
// //                 throw new Exception("Missing AppSettings:Secret configuration value.");
// //             }

// //             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
// //                 .AddJwtBearer(options =>
// //                 {
// //                     options.TokenValidationParameters = new TokenValidationParameters
// //                     {
// //                         ValidateIssuer = false,
// //                         ValidateAudience = false,
// //                         ValidateLifetime = true,
// //                         ValidateIssuerSigningKey = true,
// //                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
// //                         ClockSkew = TimeSpan.Zero
// //                     };
// //                 });

// //             // Authorization
// //             services.AddAuthorization(options =>
// //             {
// //                 options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "admin"));
// //             });

// //             // Swagger
// //             services.AddSwaggerGen();
// //             services.AddSwaggerDocumentation();

// //             // ✅ Dynamic CORS setup
// //             var frontendUrl = Configuration["FRONTEND_URL"] ?? "http://localhost:3003";

// //             services.AddCors(options =>
// //             {
// //                 options.AddPolicy("CorsPolicy", builder =>
// //                 {
// //                     builder
// //                         .WithOrigins(frontendUrl)
// //                         .AllowAnyHeader()
// //                         .AllowAnyMethod()
// //                         .WithExposedHeaders("totalAmountOfRecords");
// //                 });
// //             });
// //         }

// //         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
// //         {
// //             // Swagger (enabled in both Dev & Prod)
// //             app.UseSwagger();
// //             app.UseSwaggerUI();

// //             app.UseHttpsRedirection();
// //             app.UseStaticFiles();

// //             app.UseRouting();

// //             // ✅ Apply dynamic CORS
// //             app.UseCors("CorsPolicy");

// //             app.UseAuthentication();
// //             app.UseAuthorization();

// //             app.UseEndpoints(endpoints =>
// //             {
// //                 endpoints.MapControllers();
// //             });

// //             Console.WriteLine($"✅ App started in {env.EnvironmentName} mode");
// //             Console.WriteLine($"Frontend URL: {Configuration["FRONTEND_URL"]}");
// //             Console.WriteLine($"Database: {Configuration.GetConnectionString("DefaultConnection")}");
// //         }
// //     }
// // }

// using System.Text;
// using System.IdentityModel.Tokens.Jwt;
// using API.Database;
// using API.EXception;
// using API.Services.Implementations;
// using API.Services.Interface;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
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

//             services.AddApplicationServices(Configuration);

//             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                 .AddJwtBearer(options =>
//                 {
//                     options.TokenValidationParameters = new TokenValidationParameters
//                     {
//                         ValidateIssuer = false,
//                         ValidateAudience = false,
//                         ValidateLifetime = true,
//                         ValidateIssuerSigningKey = true,
//                         IssuerSigningKey = new SymmetricSecurityKey(
//                             Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"]!)),
//                         ClockSkew = TimeSpan.Zero
//                     };
//                 });

//             services.AddAuthorization(options =>
//             {
//                 options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "admin"));
//             });

//             services.AddSwaggerGen();
//             services.AddSwaggerDocumentation();

//             // ===== CORS =====
//             services.AddCors(options =>
//             {
//                 options.AddPolicy("CorsPolicy", builder =>
//                 {
//                     builder
//                         .WithOrigins(
//                             "http://localhost:3000",        // React dev
//                             "http://localhost:3001",        // React dev alternative
//                             "https://your-production-frontend.com" // production frontend
//                         )
//                         .AllowAnyHeader()
//                         .AllowAnyMethod()
//                         .WithExposedHeaders(new string[] { "totalAmountOfRecords" });
//                 });
//             });
//         }

//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//         {
//             if (env.IsDevelopment())
//             {
//                 app.UseSwagger();
//                 app.UseSwaggerUI();
//             }

//             app.UseSwaggerDocumentation();
//             app.UseHttpsRedirection();
//             app.UseStaticFiles();
//             app.UseRouting();

//             app.UseCors("CorsPolicy"); // Must be before auth
//             app.UseAuthentication();
//             app.UseAuthorization();

//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllers();
//             });
//         }
//     }
// }



// using System.Text;
// using System.IdentityModel.Tokens.Jwt;
// using API.Database;
// using API.EXception;
// using API.Services.Implementations;
// using API.Services.Interface;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
// using API.Extensions;
// using Microsoft.EntityFrameworkCore;

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
//             // ✅ Database configuration (works on both local and Render)
//             services.AddDbContext<AppDbContext>(options =>
//                 options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

//             // ===== Application Services =====
//             services.AddScoped<IFileStorage, LocalFileStorage>();
//             services.AddHttpContextAccessor();
//             services.AddApplicationServices(Configuration);

//             // ===== Controllers & Filters =====
//             services.AddControllers(options =>
//             {
//                 options.Filters.Add(typeof(MyException));
//                 options.Filters.Add(typeof(ParseBadRequest));
//             }).ConfigureApiBehaviorOptions(BadRequestBehavior.Parse);

//             // ===== JWT Authentication =====
//             var key = Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"] ??
//                                              throw new InvalidOperationException("JWT Secret missing"));

//             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                 .AddJwtBearer(options =>
//                 {
//                     options.TokenValidationParameters = new TokenValidationParameters
//                     {
//                         ValidateIssuer = false,
//                         ValidateAudience = false,
//                         ValidateLifetime = true,
//                         ValidateIssuerSigningKey = true,
//                         IssuerSigningKey = new SymmetricSecurityKey(key),
//                         ClockSkew = TimeSpan.Zero
//                     };
//                 });

//             // ===== Authorization Policy =====
//             services.AddAuthorization(options =>
//             {
//                 options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "admin"));
//             });

//             // ===== Swagger =====
//             services.AddSwaggerGen();
//             services.AddSwaggerDocumentation();

//             // ===== CORS =====
//             services.AddCors(options =>
//             {
//                 options.AddPolicy("CorsPolicy", builder =>
//                 {
//                     builder
//                         .WithOrigins(
//                             "http://localhost:3000",                 // local React
//                             "https://movieapp-frontend.onrender.com" // production React
//                         )
//                         .AllowAnyHeader()
//                         .AllowAnyMethod()
//                         .AllowCredentials()
//                         .WithExposedHeaders("totalAmountOfRecords");
//                 });
//             });
//         }

//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//         {
//             // ===== Swagger =====
//             if (env.IsDevelopment())
//             {
//                 app.UseSwagger();
//                 app.UseSwaggerUI();
//             }

//             app.UseSwaggerDocumentation();
//             app.UseHttpsRedirection();
//             app.UseStaticFiles();
//             app.UseRouting();

//             // ===== CORS, Auth, Routing =====
//             app.UseCors("CorsPolicy");
//             app.UseAuthentication();
//             app.UseAuthorization();

//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllers();
//             });

//             // ===== Apply database migrations automatically (safe for Render) =====
//             using var scope = app.ApplicationServices.CreateScope();
//             var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//             db.Database.Migrate();
            
//         }
//     }
// }
