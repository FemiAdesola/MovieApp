namespace API.Models
{
    public class MoviesCategories
    {
        public int CategoryId { get; set; }
        public int MovieId { get; set; }
        public Category? Category { get; set; }
        public Movie? Movie { get; set; }
    }
}