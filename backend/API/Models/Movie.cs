using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Movie : BaseModel
    {
        [StringLength(maximumLength: 75)]
        [Required]
        public string? Title { get; set; } 
        public string? Summary { get; set; }
        public string? Trailer { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Poster { get; set; }
        public List<MoviesCategories> MoviesCategories { get; set; } = null!;
        public List<MovieCinemasMovies> MovieCinemasMovies { get; set; } = null!;
        public List<MoviesActors> MoviesActors { get; set; } = null!;
    }
}