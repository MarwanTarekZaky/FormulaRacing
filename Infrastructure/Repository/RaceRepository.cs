using Domain.Models;
using Infrastructure.IRepository;
using Infrastructure.MainContext;

namespace Infrastructure.Repository;

public class RaceRepository : Repository<Race>, IRaceRepository
{
    public RaceRepository(ApplicationDbContext context) : base(context)
    {
    }
}
