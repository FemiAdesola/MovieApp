namespace API.DTOs
{
    public class MoviePutGetDTO
    {
        public MovieDTO? Movie { get; set; }
        public List<CategoryDTO>? SelectedCategories { get; set; }
        public List<CategoryDTO>? NonSelectedCategories { get; set; }
        public List<MovieCinemaDTO>? SelectedMovieCinemas { get; set; }
        public List<MovieCinemaDTO>? NonSelectedMovieCinemas { get; set; }
        public List<ActorsMovieDTO>? Actors { get; set; }
    }
}