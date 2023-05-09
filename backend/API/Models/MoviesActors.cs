using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class MoviesActors : BaseModel
    {
        public int ActorId { get; set; }
        public int MovieId { get; set; }
        
        [StringLength(maximumLength: 75)]
        public string? Character { get; set; }
        public int Order { get; set; }
        public Actor Actor { get; set; } = null!;
        public Movie Movie { get; set; } = null!;
    }
}