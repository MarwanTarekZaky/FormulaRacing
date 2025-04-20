using Domain.Models;
using Infrastructure.IRepository;
using Infrastructure.MainContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class RaceCarRepository : Repository<RaceCar>, IRaceCarRepository
{
    public RaceCarRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<RaceCar>> GetByRaceIdAsync(int raceId)
        {
            return await _context.RaceCars
                .Where(rc => rc.RaceId == raceId)
                .Include(rc => rc.Car).ThenInclude(rcc => rcc.CarImages)
                .ToListAsync();
        }

        public async Task<IEnumerable<RaceCar>> GetByCarIdAsync(int carId)
        {
            return await _context.RaceCars
                .Where(rc => rc.CarId == carId)
                .Include(rc => rc.Race)
                .ToListAsync();
        }

        public async Task RemoveAsync(int raceId, int carId)
        {
            var entity = await _context.RaceCars.FindAsync(raceId, carId);
            if (entity != null)
            {
                _context.RaceCars.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        
        
        public async Task<IEnumerable<Car>> GetCarsByRaceIdAsync(int raceId)
        {
            return await _context.RaceCars
                .Where(rc => rc.RaceId == raceId)
                .Select(rc => rc.Car)
                .Distinct()
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Race>> GetAllWithCarsAsync()
        {
            return await _context.Races
                .Include(r => r.RaceCars)
                .ThenInclude(rc => rc.Car)
                .ToListAsync();
        }

}
