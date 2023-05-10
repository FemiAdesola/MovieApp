namespace API.DTOs
{
    public class ActorsMovieDTO : BaseDTO
    {
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string? Character { get; set; }
        public int Order { get; set; }
    }
}