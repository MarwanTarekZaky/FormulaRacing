using System.Linq.Expressions;
using AutoMapper;
using Domain.Models;
using Infrastructure.DTO;
using Infrastructure.IRepository;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IoC.Services.Implementation;

public class RaceCarService : IRaceCarService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHost;
    public RaceCarService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHost)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _webHost = webHost;
    } 
    public async Task AddCarsToRaceAsync(int raceId, IEnumerable<int> carIds)
    {
        foreach (var carId in carIds)
        {
            var raceCar = new RaceCar
            {
                RaceId = raceId,
                CarId = carId
            };
            await _unitOfWork.RaceCars.AddAsync(raceCar);
        }

        await _unitOfWork.CompleteAsync();
    }
    
    public async Task UpdateCarsOfRaceAsync(int raceId, List<int> selectedCarIds)
    {
        var existing = await _unitOfWork.RaceCars.GetAllAsync(r => r.RaceId == raceId);

        _unitOfWork.RaceCars.RemoveRange(existing); // Remove old relations

        var newRelations = selectedCarIds.Select(carId => new RaceCar
        {
            RaceId = raceId,
            CarId = carId
        });

        await _unitOfWork.RaceCars.AddRangeAsync(newRelations);
        await _unitOfWork.CompleteAsync();
    }
    
    
    public async Task AddAsync(RaceCar raceCar)
    {
        await _unitOfWork.RaceCars.AddAsync(raceCar);
        await _unitOfWork.CompleteAsync();
    }
    public async Task<IEnumerable<RaceCar>> GetByRaceIdAsync(int raceId)
    {
        return await _unitOfWork.RaceCars.GetByRaceIdAsync(raceId);
    }

    public async Task<IEnumerable<RaceCar>> GetByCarIdAsync(int carId)
    {
        return await _unitOfWork.RaceCars.GetByCarIdAsync(carId);
    }
  
    public async Task RemoveAsync(int raceId, int carId)
    {
        await _unitOfWork.RaceCars.RemoveAsync(raceId, carId);
    }

    public async Task DeleteByRaceIdAsync(int raceId)
    {
        var raceCars = await _unitOfWork.RaceCars.GetAllAsync(rc => rc.RaceId == raceId);
        if (raceCars.Any())
        {
            _unitOfWork.RaceCars.RemoveRange(raceCars);
            await _unitOfWork.CompleteAsync();
        }
    }

    public async Task<IEnumerable<CarDTO>> GetCarsByRaceIdAsync(int raceId)
    {
        var cars = await _unitOfWork.RaceCars.GetCarsByRaceIdAsync(raceId);
        return _mapper.Map<IEnumerable<CarDTO>>(cars);
    }
    
    public async Task<IEnumerable<RaceDTO>> GetRacesWithCarsAsync()
    {
        var races = await _unitOfWork.RaceCars.GetAllWithCarsAsync();
        var filtered = races.Where(r => r.RaceCars != null && r.RaceCars.Any());
        return _mapper.Map<IEnumerable<RaceDTO>>(filtered);
    }


}
