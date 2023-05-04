using API.Extensions;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerDocumentation();
        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        builder.Services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy", builder =>
                        {
                            builder
                               .WithOrigins("http://localhost:3003")
                              .AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                          ;
                      });
                  });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSwaggerDocumentation();
        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseRouting();
        app.MapControllers();

        app.Run();
    }
}