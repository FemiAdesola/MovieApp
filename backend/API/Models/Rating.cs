using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class Rating : BaseModel
    {
        [Range(1, 5)]
        public int Rate { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public string? UserId { get; set; }
        public IdentityUser User { get; set; } = null!;
    }
}