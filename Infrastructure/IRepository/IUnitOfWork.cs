namespace Infrastructure.IRepository;

public interface IUnitOfWork
{
    ICarRepository Cars { get; }
    IRaceRepository Races { get; }
    IEventRepository Events { get; }
    IBookingRepository Bookings { get; }
    IBannerRepository Banners { get; }
    IHomePageContentRepository HomePageContents { get; }
    Task<int> CompleteAsync();
}