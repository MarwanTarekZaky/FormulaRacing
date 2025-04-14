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

public class RaceService : IRaceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHost;
    public RaceService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHost)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _webHost = webHost;
    }

    public async Task<IEnumerable<RaceDTO>> GetAllAsync( Expression<Func<Race,bool>>? filter = null)
    {
        IEnumerable<Race> races = null;
     
           races  = await _unitOfWork.Races.GetAllAsync(filter);
       
        return _mapper.Map<IEnumerable<RaceDTO>>(races);
    }
    public  Task<SelectList> GetSelectListAsync()
    {
        var raceList =  GetAllAsync().GetAwaiter().GetResult().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        return Task.FromResult(new SelectList(raceList, "Value", "Text"));
    }
    public async Task<RaceDTO?> GetByIdAsync(int id)
    {
        var race = await _unitOfWork.Races.GetByIdAsync(id);
        return _mapper.Map<RaceDTO?>(race);
    }

    public async Task AddAsync(RaceDTO raceDto)
    {
        var race = _mapper.Map<Race>(raceDto);

        if (raceDto.ImageFile != null)
        {
            // Handle image upload here
            race.ImageUrl = await SaveImageAsync(raceDto.ImageFile);
        }

        await _unitOfWork.Races.AddAsync(race);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateAsync(RaceDTO raceDto)
    {
        var race = await _unitOfWork.Races.GetByIdAsync(raceDto.Id);
        
        if (race == null) return;
         _mapper.Map(raceDto, race);
  

        if (raceDto.ImageFile != null)
        {
            // Handle image update
           race.ImageUrl =  await SaveImageAsync(raceDto.ImageFile);
        }

        _unitOfWork.Races.Update(race);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var race = await _unitOfWork.Races.GetByIdAsync(id);
        if (race != null)
        {
            _unitOfWork.Races.Remove(race);
            await _unitOfWork.CompleteAsync();
        }
    }

    private async Task<string> SaveImageAsync(IFormFile imageFile)
    {
        var uploadDir = Path.Combine(_webHost.WebRootPath, "images", "races");
        Directory.CreateDirectory(uploadDir);
        // Save the image file and return the URL or path
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
        var filePath = Path.Combine(uploadDir, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return "/images/races/" + fileName;
    }
}
