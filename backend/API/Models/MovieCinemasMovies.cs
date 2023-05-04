namespace API.Models
{
    public class MovieCinemasMovies
    {
        public int MovieTheaterId { get; set; }
        public int MovieId { get; set; }
        public MovieCinema? MovieCinema { get; set; }
        public Movie? Movie { get; set; }
    }
}