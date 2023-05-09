using API.DTOs;
using API.Models;
using AutoMapper;
using NetTopologySuite.Geometries;

namespace API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile(GeometryFactory geometryFactor)
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<CreateCategoryDTO, Category>();

            CreateMap<ActorDTO, Actor>().ReverseMap();
            CreateMap<CreateActorDTO, Actor>()
                .ForMember(x=>x.Image, options=>options.Ignore());

            CreateMap<MovieCinema,  MovieCinemaDTO>()
               .ForMember(x => x.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
               .ForMember(x => x.Longitude, dto => dto.MapFrom(prop => prop.Location.X));

            CreateMap<CreateMovieCinemaDTO, MovieCinema>()
                .ForMember(x => x.Location, x => x.MapFrom(dto =>
                geometryFactor.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));
        
            CreateMap<CreateMovieDTO, Movie>()
               .ForMember(x => x.Poster, options => options.Ignore())
               .ForMember(x => x.MoviesCategories, options => options.MapFrom(MapMoviesCategories))
               .ForMember(x => x.MovieCinemasMovies, options => options.MapFrom(MapMMovieCinemasMovies))
               .ForMember(x => x.MoviesActors, options => options.MapFrom(MapMoviesActors));
        }

         private List<MoviesCategories> MapMoviesCategories(CreateMovieDTO createMovieDTO, Movie movie)
        {
            var result = new List<MoviesCategories>();

            if (createMovieDTO.CategoryIds == null) { return result; }

            foreach (var id in createMovieDTO.CategoryIds)
            {
                result.Add(new MoviesCategories() { CategoryId = id });
            }

            return result;
        }

        private List<MovieCinemasMovies> MapMMovieCinemasMovies(CreateMovieDTO createMovieDTO,
            Movie movie)
        {
            var result = new List<MovieCinemasMovies>();

            if (createMovieDTO.MovieCinemasIds == null) { return result; }

            foreach (var id in createMovieDTO.MovieCinemasIds)
            {
                result.Add(new MovieCinemasMovies() { MovieCinemaId = id });
            }

            return result;
        }

        private List<MoviesActors> MapMoviesActors(CreateMovieDTO createMovieDTO, Movie movie)
        {
            var result = new List<MoviesActors>();

            if (createMovieDTO.Actors == null) { return result; }

            foreach (var actor in createMovieDTO.Actors)
            {
                result.Add(new MoviesActors() { ActorId = actor.Id, Character = actor.Character });
            }

            return result;
        }

    }
}