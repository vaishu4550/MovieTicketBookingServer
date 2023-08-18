using AutoMapper;
using MovieTicketBooking.Data.Models;
using MovieTicketBooking.Data.Models.Dto;

namespace MovieTicketBooking.Data.AutoMapperProfile
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<TheatreDto, Theatre>()
                .ForMember(dest => dest.TheatreName, opt => opt.MapFrom(src => src.TheatreName))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ReverseMap();

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.ContactNumber))
                .ReverseMap();

            CreateMap<MovieDto, Movie>()
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.MovieName))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages))
                .ReverseMap();

            CreateMap<TicketDto, Tickets>()
                .ForMember(dest => dest.TicketsCount, opt => opt.MapFrom(src => src.TicketsCount))
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
                .ReverseMap();
        }
    }
}
