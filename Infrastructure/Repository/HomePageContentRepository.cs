using Domain.Models;
using Infrastructure.IRepository;
using Infrastructure.MainContext;

namespace Infrastructure.Repository;

public class HomePageContentRepository : Repository<HomePageContent>, IHomePageContentRepository
{
    public HomePageContentRepository(ApplicationDbContext context) : base(context) { }
}