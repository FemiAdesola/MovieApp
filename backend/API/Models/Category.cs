using System.ComponentModel.DataAnnotations;
using API.Helper;

namespace API.Models
{
    public class Category: BaseModel
    {
        [Required(ErrorMessage = "The field with name {0} is required")]
        [StringLength(50)]
        [FirstLetterUppercase]
        public string Name { get; set; } = null!;
    }
}