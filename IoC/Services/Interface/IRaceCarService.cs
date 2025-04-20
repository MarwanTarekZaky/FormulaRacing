using System.Linq.Expressions;
using Domain.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IoC.Services.Interface;

public interface IRaceCarService
{
    Task AddAsync(RaceCar RaceCar);
    Task<IEnumerable<RaceCar>> GetByRaceIdAsync(int raceId);
    Task<IEnumerable<RaceCar>> GetByCarIdAsync(int carId);
    Task RemoveAsync(int raceId, int carId);
    
    Task AddCarsToRaceAsync(int raceId, IEnumerable<int> carIds);
    Task UpdateCarsOfRaceAsync(int raceId, List<int> selectedCarIds);
    Task DeleteByRaceIdAsync(int raceId);
    
    Task<IEnumerable<CarDTO>> GetCarsByRaceIdAsync(int raceId);
    Task<IEnumerable<RaceDTO>> GetRacesWithCarsAsync();
    // void RemoveRange(IEnumerable<RaceCar> entities); 
    // Task AddRangeAsync(IEnumerable<RaceCar> entities);

}
