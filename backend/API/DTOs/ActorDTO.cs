namespace API.DTOs
{
    public class ActorDTO : BaseDTO
    {
        public string Name { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}