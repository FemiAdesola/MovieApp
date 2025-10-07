using System.Reflection;
using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    // public class AppDbContext : DbContext 
       public class AppDbContext : IdentityDbContext
    {
        static AppDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        private readonly IConfiguration _config;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options) => _config = config;

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     base.OnConfiguring(optionsBuilder);

        //     var connString = _config.GetConnectionString("DefaultConnection");
        //     optionsBuilder
        //         .UseNpgsql(
        //             connString, 
        //             sqlOptions  => sqlOptions.UseNetTopologySuite())
        //         // .AddInterceptors(new AppDbContextSaveChangesInterceptor())
        //         .UseSnakeCaseNamingConvention();
        // }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    base.OnConfiguring(optionsBuilder);

    // Prefer DATABASE_URL from Render
    var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

    string connString;
    if (!string.IsNullOrEmpty(databaseUrl))
    {
        var npgsqlBuilder = new Npgsql.NpgsqlConnectionStringBuilder(databaseUrl)
        {
            SslMode = Npgsql.SslMode.Require
        };
        connString = npgsqlBuilder.ConnectionString;
    }
    else
    {
        connString = _config.GetConnectionString("DefaultConnection")!;
    }

    optionsBuilder
        .UseNpgsql(connString, sqlOptions => sqlOptions.UseNetTopologySuite())
        .UseSnakeCaseNamingConvention();
}

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddICommonConfig();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Actor> Actors { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<MovieCinema> MovieCinemas { get; set; } = null!;
        public DbSet<MoviesActors> MoviesActors { get; set; } = null!;
        public DbSet<MovieCinemasMovies> MovieCinemasMovies { get; set; } = null!;
        public DbSet<MoviesCategories> MoviesCategories { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
    }
}