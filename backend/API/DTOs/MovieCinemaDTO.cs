using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class MovieCinemaDTO : BaseDTO
    {
        [StringLength(maximumLength: 75)]
        public string Name { get; set; } = null!;
        public double Latitude { get; set; } 
         public double Longitude { get; set; } 

    }
}