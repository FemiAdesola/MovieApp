using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateActorDTO
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? Biography { get; set; }
        public IFormFile? Image { get; set; } 
    }
}