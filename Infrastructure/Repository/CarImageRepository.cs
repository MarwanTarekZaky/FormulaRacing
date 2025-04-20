using Domain.Models;
using Infrastructure.IRepository;
using Infrastructure.MainContext;

namespace Infrastructure.Repository;

public class CarImageRepository : Repository<CarImage>, ICarImageRepository
{
    public CarImageRepository(ApplicationDbContext context) : base(context)
    {
    }
}
