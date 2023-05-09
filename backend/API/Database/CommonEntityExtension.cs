using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public static class CommonEntityExtension
    {
        public static void AddICommonConfig(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesActors>()
                .HasKey(x => new { x.ActorId, x.MovieId });

            modelBuilder.Entity<MoviesCategories>()
                .HasKey(x => new { x.CategoryId, x.MovieId });

            modelBuilder.Entity<MovieCinemasMovies>()
                .HasKey(x => new { x.MovieCinemaId, x.MovieId });
        }
    }
}