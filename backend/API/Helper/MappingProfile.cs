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
        }

    }
}