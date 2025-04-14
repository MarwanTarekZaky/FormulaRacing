using Domain.Models;
using Infrastructure.IRepository;
using Infrastructure.MainContext;

namespace Infrastructure.Repository;

public class CarRepository : Repository<Car>, ICarRepository
{
    public CarRepository(ApplicationDbContext context) : base(context) { }
}