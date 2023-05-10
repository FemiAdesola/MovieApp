namespace API.DTOs
{
    public class MovieDTO : BaseDTO
    {
        public string? Title { get; set; } 
        public string? Summary { get; set; }
        public string? Trailer { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Poster { get; set; }
        public List<CategoryDTO>? Categories { get; set; }
        public List<MovieCinemaDTO>? MovieCinemas { get; set; }
        public List<ActorsMovieDTO>? Actors { get; set; }
    }
}