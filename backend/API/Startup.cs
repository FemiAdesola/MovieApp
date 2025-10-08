// // using System.IdentityModel.Tokens.Jwt;
// // using System.Text;
// // using API.Database;
// // using API.EXception;
// // using API.Services.Implementations;
// // using Microsoft.AspNetCore.Authentication.JwtBearer;
// // using Microsoft.IdentityModel.Tokens;
// // using API.Services.Interface;
// // using API.Extensions;
// // using Microsoft.AspNetCore.Identity;

// // namespace API
// // {
// //     public class Startup
// //     {
// //         public Startup(IConfiguration configuration)
// //         {
// //             JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
// //             Configuration = configuration;

// //             // ✅ Debugging: print JWT secret
// //         Console.WriteLine($"🔑 JWT Secret: {Configuration["AppSettings:Secret"]}");
// //         }

// //         public IConfiguration Configuration { get; }
// //         public void ConfigureServices(IServiceCollection services)
// //         {
// //             services.AddDbContext<AppDbContext>();
// //             services.AddScoped<IFileStorage, LocalFileStorage>();
// //             services.AddHttpContextAccessor();

// //             services.AddControllers(options =>
// //             {

// //                 options.Filters.Add(typeof(MyException));
// //                 options.Filters.Add(typeof(ParseBadRequest));
// //             }).ConfigureApiBehaviorOptions(BadRequestBehavior.Parse);

// //           services.AddApplicationServices(Configuration);
// //             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
// //                .AddJwtBearer(options =>
// //                {
// //                    options.TokenValidationParameters = new TokenValidationParameters
// //                    {
// //                        ValidateIssuer = false,
// //                        ValidateAudience = false,
// //                        ValidateLifetime = true,
// //                        ValidateIssuerSigningKey = true,
// //                        IssuerSigningKey = new SymmetricSecurityKey(
// //                            Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"]!)),
// //                        ClockSkew = TimeSpan.Zero

// //                    };
// //                });

// //             services.AddAuthorization(options =>
// //             {
// //                 options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "admin"));
// //             });

// //             services.AddSwaggerGen();
// //             services.AddSwaggerDocumentation();

// //             services.AddCors(options =>
// //                 {
// //                     options.AddPolicy("CorsPolicy", builder =>
// //                         {
// //                             builder
// //                                .WithOrigins("http://localhost:3000")
// //                               .AllowAnyOrigin()
// //                               .AllowAnyHeader()
// //                               .AllowAnyMethod()
// //                               .WithExposedHeaders(new string[] { "totalAmountOfRecords" });
// //                         });
// //                 });
// //         }
// //         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
// //         {
// //             //Configure the HTTP request pipeline.
// //             if (env.IsDevelopment())
// //             {
// //                 app.UseSwagger();
// //                 app.UseSwaggerUI();
// //             }


// //             app.UseSwaggerDocumentation();
// //             app.UseHttpsRedirection();
// //             app.UseStaticFiles();
// //             app.UseRouting();
// //             app.UseCors("CorsPolicy");
// //             app.UseAuthentication();
// //             app.UseAuthorization();
// //             app.UseEndpoints(endpoints =>
// //             {
// //                 endpoints.MapControllers();
// //             });
// //             // ✅ Seed the admin user after all middleware is set up
// //             using var scope = app.ApplicationServices.CreateScope();
// //             var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
// //             SeedData.EnsureAdminAsync(userManager).GetAwaiter().GetResult();
// //                 }
// //     }
// // }




// using System.IdentityModel.Tokens.Jwt;
// using System.Text;
// using API.Database;
// using API.EXception;
// using API.Services.Implementations;
// using API.Services.Interface;
// using API.Extensions;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.IdentityModel.Tokens;

// namespace API
// {
//     public class Startup
//     {
//         public Startup(IConfiguration configuration)
//         {
//             Configuration = configuration;
//             JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

//             // ✅ Debug JWT secret on startup
//             Console.WriteLine($"🔑 JWT Secret: {Configuration["AppSettings:Secret"]}");
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

//             // ✅ Authentication + JWT
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

//             // ✅ Authorization policy for admin
//             services.AddAuthorization(options =>
//             {
//                 options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "admin"));
//             });

//             services.AddSwaggerGen();
//             services.AddSwaggerDocumentation();

//             // ✅ CORS
//             services.AddCors(options =>
//             {
//                 options.AddPolicy("CorsPolicy", builder =>
//                 {
//                     builder
//                         .AllowAnyOrigin()
//                         .AllowAnyHeader()
//                         .AllowAnyMethod()
//                         .WithExposedHeaders(new[] { "totalAmountOfRecords" });
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



using API.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Database
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        // Identity
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // JWT
        var secret = Configuration["AppSettings:Secret"] ?? throw new Exception("JWT secret missing");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                };
            });

        // Authorization
        services.AddAuthorization(options =>
        {
            options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "admin"));
        });

        // Controllers + JSON
        services.AddControllers().AddNewtonsoftJson();

        // Swagger setup
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(option =>
        {
            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            option.AddSecurityDefinition("Bearer", securitySchema);
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securitySchema, new[] { "Bearer" } }
            });
        });

        // CORS for frontend
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod());
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();
        app.UseCors("CorsPolicy");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
