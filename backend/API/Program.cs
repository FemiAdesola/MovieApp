// // using API;
// // using DotNetEnv;

// // namespace MoviesAPI
// // {
// //     public class Program
// //     {
// //         public static void Main(string[] args)
// //         {
// //             if (Environment.GetEnvironmentVariable("RENDER") == null)
// //             {
// //                 Env.Load();
// //             }

// //             CreateHostBuilder(args).Build().Run();
// //         }

// //         public static IHostBuilder CreateHostBuilder(string[] args) =>
// //             Host.CreateDefaultBuilder(args)
// //                 .ConfigureWebHostDefaults(webBuilder =>
// //                 {
// //                     webBuilder.UseStartup<Startup>();
// //                 });
// //     }
// // }

// using API;
// using API.Database;
// using DotNetEnv;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;

// namespace MoviesAPI
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             // Load .env locally, but not on Render
//             if (Environment.GetEnvironmentVariable("RENDER") == null)
//             {
//                 Env.Load();
//             }

//             var host = CreateHostBuilder(args).Build();

//             // ✅ Apply migrations and seed admin on startup
//             using (var scope = host.Services.CreateScope())
//             {
//                 var services = scope.ServiceProvider;
//                 try
//                 {
//                     var context = services.GetRequiredService<AppDbContext>();
//                     context.Database.Migrate();

//                     var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
//                     SeedData.EnsureAdminAsync(userManager).GetAwaiter().GetResult();

//                     Console.WriteLine("✅ Database migrations applied and admin seeding done.");
//                 }
//                 catch (Exception ex)
//                 {
//                     Console.WriteLine($"❌ Error applying migrations or seeding admin: {ex.Message}");
//                     throw;
//                 }
//             }

//             host.Run();
//         }

//         public static IHostBuilder CreateHostBuilder(string[] args) =>
//             Host.CreateDefaultBuilder(args)
//                 .ConfigureWebHostDefaults(webBuilder =>
//                 {
//                     webBuilder.UseStartup<Startup>();
//                 });
//     }
// }

using API.Database;
using API.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

public class Program
{
    public static void Main(string[] args)
    {
        // Load .env locally, Render provides environment variables automatically
        if (Environment.GetEnvironmentVariable("RENDER") == null)
        {
            Env.Load();
        }

        var host = CreateHostBuilder(args).Build();

        // Apply migrations & seed admin user automatically
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                context.Database.Migrate();

                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                SeedData.EnsureAdminAsync(userManager).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Migration or seeding failed: {ex.Message}");
            }
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
