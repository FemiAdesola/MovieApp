namespace API.Models
{
    public class MoviesCategories : BaseModel
    {
        public int CategoryId { get; set; }
        public int MovieId { get; set; }
        public Category? Category { get; set; }
        public Movie? Movie { get; set; }
    }
}