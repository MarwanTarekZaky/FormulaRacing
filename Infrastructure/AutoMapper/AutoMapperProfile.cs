using System.Security.Cryptography;
using AutoMapper;
using Domain.Models;
using Infrastructure.DTO;

namespace Infrastructure.AutoMapper;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Car, Car>().ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id if you're mapping to a new object
        CreateMap<CarDTO, Car>();
        CreateMap<Car, CarDTO>()
            .ForMember(dest => dest.ImageUrl,
                opt => opt.MapFrom(src => src.CarImages.Select(e => e.ImageUrl).FirstOrDefault()))

            .ForMember(dest => dest.ImagesUrl,
                opt => opt.MapFrom(src => src.CarImages.Select(e => e.ImageUrl!).ToList()))


            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : null))
            ;
        
        CreateMap<Race, RaceDTO>().ReverseMap();
        CreateMap<EventDTO, Event>().ReverseMap();
        CreateMap<Banner, BannerDTO>().ReverseMap();
        CreateMap<BookingDTO, Booking>().ReverseMap();
        CreateMap<HomePageContent, HomePageContentDTO>().ReverseMap();
        CreateMap<BrandDTO, Brand>().ReverseMap();
        CreateMap<Location, LocationDTO>().ReverseMap();
        CreateMap<HomeContentDescription, HomeContentDescriptionDTO>().ReverseMap();
        // CreateMap<HomeContentDescriptionDTO, HomeContentDescription>().ForMember(dest => dest.TracksDescription, opt => opt.MapFrom(src => src.TracksDescription != null ? src.TracksDescription : opt.DestinationMember.));

    }
}
