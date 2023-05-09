using System.Reflection;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class AppDbContext : DbContext 
    {
        static AppDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        private readonly IConfiguration _config;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options) => _config = config;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connString = _config.GetConnectionString("DefaultConnection");
            optionsBuilder
                .UseNpgsql(
                    connString, 
                    sqlOptions  => sqlOptions.UseNetTopologySuite())
                
               
                // .AddInterceptors(new AppDbContextSaveChangesInterceptor())
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
    }
}