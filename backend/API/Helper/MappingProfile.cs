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
        
            CreateMap<Movie, MovieDTO>()
               .ForMember(x => x.Categories, options => options.MapFrom(MapMoviesCategories))
               .ForMember(x => x.MovieCinemas, options => options.MapFrom(MapMMovieCinemasMovies))
               .ForMember(x => x.Actors, options => options.MapFrom(MapMoviesActors));
        
        
        }

         private List<MoviesCategories> MapMoviesCategories(CreateMovieDTO createMovieDTO, Movie movie)
        {
            var result = new List<MoviesCategories>();

            if (createMovieDTO.CategoryIds == null) { return result; }

            foreach (var Id in createMovieDTO.CategoryIds)
            {
                result.Add(new MoviesCategories() { CategoryId = Id });
            }

            return result;
        }

        private List<MovieCinemasMovies> MapMMovieCinemasMovies(CreateMovieDTO createMovieDTO,
            Movie movie)
        {
            var result = new List<MovieCinemasMovies>();

            if (createMovieDTO.MovieCinemaIds == null) { return result; }

            foreach (var Id in createMovieDTO.MovieCinemaIds)
            {
                result.Add(new MovieCinemasMovies() { MovieCinemaId = Id });
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
        private List<CategoryDTO> MapMoviesCategories(Movie movie, MovieDTO moviedto)
        {
            var result = new List<CategoryDTO>();

            if (movie.MoviesCategories != null)
            {
                foreach (var category in movie.MoviesCategories)
                {
                    result.Add(new CategoryDTO() { Id = category.CategoryId, Name = category.Category!.Name});
                }
            }

            return result;
        }

        private List<ActorsMovieDTO> MapMoviesActors(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<ActorsMovieDTO>();

            if (movie.MoviesActors != null)
            {
                foreach (var moviesActors in movie.MoviesActors)
                {
                    result.Add(new ActorsMovieDTO()
                    {
                        Id = moviesActors.ActorId,
                        Name = moviesActors.Actor.Name,
                        Character = moviesActors.Character,
                        Image= moviesActors.Actor.Image,
                        Order = moviesActors.Order
                    });
                }
            }

            return result;
        }

        private List<MovieCinemaDTO> MapMMovieCinemasMovies(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<MovieCinemaDTO>();

            if (movie.MovieCinemasMovies != null)
            {
                foreach (var movieCinemaMovies in movie.MovieCinemasMovies)
                {
                    result.Add(new MovieCinemaDTO()
                    {
                        Id = movieCinemaMovies.MovieCinemaId,
                        Name = movieCinemaMovies.MovieCinema!.Name,
                        Latitude = movieCinemaMovies.MovieCinema.Location.Y,
                        Longitude = movieCinemaMovies.MovieCinema.Location.X
                    });
                }
            }

            return result;
        }

    }
}