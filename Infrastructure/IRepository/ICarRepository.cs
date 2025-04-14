using Domain.Models;

namespace Infrastructure.IRepository;

public interface ICarRepository: IRepository<Car>
{
    // Additional methods for Car if needed
}