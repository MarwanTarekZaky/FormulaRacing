namespace Infrastructure.IRepository;

public interface IUnitOfWork
{
    ICarRepository Cars { get; }
    IRaceRepository Races { get; }
    IEventRepository Events { get; }
    IBookingRepository Bookings { get; }
    IBannerRepository Banners { get; }
    IHomePageContentRepository HomePageContents { get; }
    IBrandRepository Brands { get; }
    ILocationRepository Locations { get; }
    ICarImageRepository CarImages { get; }
    IRaceCarRepository RaceCars { get; }
    IHomeContentDescriptionRepository HomeContentDescriptions { get; }
    Task<int> CompleteAsync();
}