using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace API.Models
{
    public class MovieCinema : BaseModel
    {
        [StringLength(maximumLength: 75)]
        public string Name { get; set; } = null!;
        // public Point Location { get; set; }
    }
}