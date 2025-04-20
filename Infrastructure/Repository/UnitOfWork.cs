using Infrastructure.IRepository;
using Infrastructure.MainContext;

namespace Infrastructure.Repository;

public class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public ICarRepository Cars { get; }
    public IRaceRepository Races { get; private set; }
    public IEventRepository Events { get; }
    public IBookingRepository Bookings { get; }
    public IBannerRepository Banners { get; }
    public IHomePageContentRepository HomePageContents { get; }

    public IBrandRepository Brands { get; }
    
    public ILocationRepository Locations { get; }
    public ICarImageRepository CarImages { get; }
    public IRaceCarRepository RaceCars { get; }
    public IHomeContentDescriptionRepository HomeContentDescriptions { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Cars = new CarRepository(_context);
        Races = new RaceRepository(_context);
        Events = new EventRepository(_context);
        Bookings = new BookingRepository(_context);
        Banners = new BannerRepository(_context);
        HomePageContents = new HomePageContentRepository(_context);
        Brands = new BrandRepository(_context);
        Locations = new LocationRepository(_context);
        CarImages = new CarImageRepository(_context);
        RaceCars = new RaceCarRepository(_context);
        HomeContentDescriptions = new HomeContentDescriptionRepository(_context);
        
    }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}