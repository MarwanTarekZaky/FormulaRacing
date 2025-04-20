using Domain.Models;

namespace Infrastructure.IRepository;

public interface IRaceCarRepository : IRepository<RaceCar>
{
    // Custom queries for RaceCar can be added here in the future
    Task<IEnumerable<RaceCar>> GetByRaceIdAsync(int raceId);
    Task<IEnumerable<RaceCar>> GetByCarIdAsync(int carId);
    Task RemoveAsync(int raceId, int carId);
    
    Task<IEnumerable<Car>> GetCarsByRaceIdAsync(int raceId);
    Task<IEnumerable<Race>> GetAllWithCarsAsync();

}
