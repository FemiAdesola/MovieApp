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