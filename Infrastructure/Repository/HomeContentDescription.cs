using Domain.Models;
using Infrastructure.IRepository;
using Infrastructure.MainContext;

namespace Infrastructure.Repository;

public class HomeContentDescriptionRepository : Repository<HomeContentDescription>, IHomeContentDescriptionRepository
{
    public HomeContentDescriptionRepository(ApplicationDbContext context) : base(context) { }
}