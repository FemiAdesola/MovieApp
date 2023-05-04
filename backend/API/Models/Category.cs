using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Category: BaseModel
    {
        [Required(ErrorMessage = "The field with name {0} is required")]
        [StringLength(50)]
        public string Name { get; set; } = null!;
    }
}