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
// using DotNetEnv;

// namespace MoviesAPI
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             // Load .env only in local development
//             var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//             if (env == "Development")
//             {
//                 Env.Load();
//                 Console.WriteLine("✅ Loaded .env for local development");
//             }
//             else
//             {
//                 Console.WriteLine("🚀 Running in production - using environment variables from Render");
//             }

//             CreateHostBuilder(args).Build().Run();
//         }

//         public static IHostBuilder CreateHostBuilder(string[] args) =>
//             Host.CreateDefaultBuilder(args)
//                 .ConfigureWebHostDefaults(webBuilder =>
//                 {
//                     webBuilder.UseStartup<Startup>();
//                 });
//     }
// }


using API;
using DotNetEnv;

namespace MoviesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Detect environment
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (env == "Development")
            {
                Env.Load();
                Console.WriteLine("✅ Loaded .env for local development");
            }
            else
            {
                Console.WriteLine("🚀 Running in production - using Render environment variables");
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
