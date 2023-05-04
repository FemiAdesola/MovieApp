using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Actor : BaseModel
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}