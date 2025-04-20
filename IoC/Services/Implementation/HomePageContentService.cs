using AutoMapper;
using Domain.Models;
using Infrastructure.DTO;
using Infrastructure.IRepository;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Hosting;

namespace IoC.Services.Implementation;

// IoC/Services/Implementation/HomePageContentService.cs
public class HomePageContentService : IHomePageContentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHost;

    public HomePageContentService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHost)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _webHost = webHost;
    }

    public async Task<HomePageContentDTO> Get()
    {
        var homePageContents = await _unitOfWork.HomePageContents.GetByIdAsync(1);
        return _mapper.Map<HomePageContentDTO>(homePageContents);
    }


    public async Task UpdateAsync(HomePageContentDTO dto)
    {
        var homePageContent = await _unitOfWork.HomePageContents.GetByIdAsync(dto.Id);
        if (homePageContent == null) return;
        DeleteVideo(homePageContent.VideoPath);
        _mapper.Map(dto, homePageContent);

        if (dto.VideoFile != null)
        {
            await SaveVideo(homePageContent);
        }

        _unitOfWork.HomePageContents.Update(homePageContent);
        await _unitOfWork.CompleteAsync();
    }

  
    private async Task SaveVideo(HomePageContent homePageContent)
    {
        if (homePageContent.VideoFile != null)
        {
            var uploadDir = Path.Combine(_webHost.WebRootPath, "homePageVideo");
            Directory.CreateDirectory(uploadDir);

            var fileName = Guid.NewGuid() + Path.GetExtension(homePageContent.VideoFile.FileName);
            var filePath = Path.Combine(uploadDir, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await homePageContent.VideoFile.CopyToAsync(stream);

            homePageContent.VideoPath = $"/homePageVideo/{fileName}";
        }
    }

    private void DeleteVideo(string? videoUrl)
    {
        if (!string.IsNullOrEmpty(videoUrl))
        {
            var path = Path.Combine(_webHost.WebRootPath, videoUrl.TrimStart('/'));
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}

