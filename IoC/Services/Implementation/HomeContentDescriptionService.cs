using System.Linq.Expressions;
using AutoMapper;
using Domain.Models;
using Infrastructure.DTO;
using Infrastructure.IRepository;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Hosting;

namespace IoC.Services.Implementation;

// IoC/Services/Implementation/HomeContentDescriptionService.cs
public class HomeContentDescriptionService : IHomeContentDescriptionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHost;

    public HomeContentDescriptionService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHost)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _webHost = webHost;
    }


    public async Task<HomeContentDescriptionDTO?> GetLastAsync(Expression<Func<HomeContentDescription, bool>>? predicate = null,
        Expression<Func<HomeContentDescription, object>>? orderBy = null, params Expression<Func<HomeContentDescription, object>>[]? includes)
    {
        var homeContentDescription = await _unitOfWork.HomeContentDescriptions.GetLastAsync(predicate, orderBy: x => x.Id);
        HomeContentDescriptionDTO? dto = _mapper.Map<HomeContentDescriptionDTO>(homeContentDescription);
        return dto;
    }

    public async Task AddAsync(HomeContentDescriptionDTO dto)
    {
        var homeContentDescription = _mapper.Map<HomeContentDescription>(dto);
        
        await _unitOfWork.HomeContentDescriptions.AddAsync(homeContentDescription);
        await _unitOfWork.CompleteAsync();
    }


    public async Task UpdatePartialAsync(HomeContentDescriptionDTO dto)
    {
        var homeContentDescription = await _unitOfWork.HomeContentDescriptions.GetLastAsync( orderBy: x => x.Id);
        if (homeContentDescription == null)
        {
            _mapper.Map(dto, homeContentDescription);
            _unitOfWork.HomeContentDescriptions.Update(homeContentDescription!);
        }

        if (!string.IsNullOrEmpty(dto.RacesDescription))
        {
            homeContentDescription!.RacesDescription = dto.RacesDescription;
        }

        if (!string.IsNullOrEmpty(dto.TracksDescription))
        {
            homeContentDescription!.TracksDescription = dto.TracksDescription;
        }
        
        _unitOfWork.HomeContentDescriptions.Update(homeContentDescription!);
        await _unitOfWork.CompleteAsync();
    }

  

}

