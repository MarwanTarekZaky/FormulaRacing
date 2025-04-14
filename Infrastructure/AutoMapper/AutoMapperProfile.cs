using AutoMapper;
using Domain.Models;
using Infrastructure.DTO;

namespace Infrastructure.AutoMapper;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Car, Car>().ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id if you're mapping to a new object
        CreateMap<CarDTO, Car>().ReverseMap();
        CreateMap<Race, RaceDTO>().ReverseMap();
        CreateMap<EventDTO, Event>().ReverseMap();
        CreateMap<Banner, BannerDTO>().ReverseMap();
        CreateMap<BookingDTO, Booking>().ReverseMap();
        CreateMap<HomePageContent, HomePageContentDTO>().ReverseMap();

    }
}