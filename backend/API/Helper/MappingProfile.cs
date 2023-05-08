using API.DTOs;
using API.Models;
using AutoMapper;
using NetTopologySuite.Geometries;

namespace API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile(GeometryFactory geometryFactory)
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<CreateCategoryDTO, Category>();

            CreateMap<ActorDTO, Actor>().ReverseMap();
            CreateMap<CreateActorDTO, Actor>()
                .ForMember(x=>x.Image, options=>options.Ignore());

            CreateMap<MovieCinema,  MovieCinemaDTO>()
               .ForMember(x => x.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
               .ForMember(x => x.Longitude, dto => dto.MapFrom(prop => prop.Location.X));

            // CreateMap<MovieTheaterCreationDTO, MovieCinema>()
            //     .ForMember(x => x.Location, x => x.MapFrom(dto =>
            //     geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));
        }

    }
}