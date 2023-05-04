namespace API.Models
{
    public class MovieCinemasMovies : BaseModel
    {
        public int MovieCinemaId { get; set; }
        public int MovieId { get; set; }
        public MovieCinema? MovieCinema { get; set; }
        public Movie? Movie { get; set; }
    }
}