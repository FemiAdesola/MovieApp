using API.Helper;
using Microsoft.AspNetCore.Mvc;

namespace API.DTOs
{
    public class CreateMovieDTO
    {
        public string? Title { get; set; } 
        public string? Summary { get; set; }
        public string? Trailer { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType =typeof(TypeBinder<List<int>>))]
        public List<int> CategoryIds { get; set; }

         [ModelBinder(BinderType =typeof(TypeBinder<List<int>>))]
        public List<int> MovieCinemasIds { get; set; }

        [ModelBinder(BinderType =typeof(TypeBinder<List<CreateMovieActorsDTO>>))]
        public List<CreateMovieActorsDTO> Actors { get; set; }
    }
}