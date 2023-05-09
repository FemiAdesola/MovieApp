namespace API.DTOs
{
    public class MoviePostGetDTO
    {
        public List<CategoryDTO>? Categories { get; set; }
        public List<MovieCinemaDTO>? MovieCinemas { get; set; }
    }
}