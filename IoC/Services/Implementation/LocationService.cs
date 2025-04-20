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

public class LocationService : ILocationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHost;
    public LocationService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHost)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _webHost = webHost;
    }

    public async Task<IEnumerable<LocationDTO>> GetAllAsync( Expression<Func<Location,bool>>? filter = null)
    {
        IEnumerable<Location> locations = null;
     
           locations  = await _unitOfWork.Locations.GetAllAsync(filter);
       
        return _mapper.Map<IEnumerable<LocationDTO>>(locations);
    }
    public  Task<SelectList> GetSelectListAsync(Expression<Func<Location,bool>>? filter = null)
    {
        var locationList =  GetAllAsync(filter).GetAwaiter().GetResult().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        return Task.FromResult(new SelectList(locationList, "Value", "Text"));
    }
    public async Task<LocationDTO?> GetByIdAsync(int id)
    {
        var location = await _unitOfWork.Locations.GetByIdAsync(id);
        return _mapper.Map<LocationDTO?>(location);
    }

    public async Task AddAsync(LocationDTO locationDto)
    {
        var location = _mapper.Map<Location>(locationDto);
        
        await _unitOfWork.Locations.AddAsync(location);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateAsync(LocationDTO locationDto)
    {
        var location = await _unitOfWork.Locations.GetByIdAsync(locationDto.Id);
        
        if (location == null) return;
         _mapper.Map(locationDto, location);
         
        _unitOfWork.Locations.Update(location);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var location = await _unitOfWork.Locations.GetByIdAsync(id);
        if (location != null)
        {
            _unitOfWork.Locations.Remove(location);
            await _unitOfWork.CompleteAsync();
        }
    }

 
}
