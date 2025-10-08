using API;
using DotNetEnv;

namespace MoviesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (Environment.GetEnvironmentVariable("RENDER") == null)
            {
                Env.Load();
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

