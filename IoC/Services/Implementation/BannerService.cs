using AutoMapper;
using Domain.Models;
using Infrastructure.DTO;
using Infrastructure.IRepository;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Hosting;

namespace IoC.Services.Implementation;

// IoC/Services/Implementation/BannerService.cs
public class BannerService : IBannerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHost;

    public BannerService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHost)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _webHost = webHost;
    }

    public async Task<IEnumerable<BannerDTO>> GetAllAsync()
    {
        var banners = await _unitOfWork.Banners.GetAllAsync();
        return _mapper.Map<IEnumerable<BannerDTO>>(banners);
    }

    public async Task<BannerDTO> GetByIdAsync(int id)
    {
        var banner = await _unitOfWork.Banners.GetByIdAsync(id);
        return _mapper.Map<BannerDTO>(banner);
    }

    public async Task AddAsync(BannerDTO dto)
    {
        var banner = _mapper.Map<Banner>(dto);
        await SaveImage(banner);
        await _unitOfWork.Banners.AddAsync(banner);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateAsync(BannerDTO dto)
    {
        var banner = await _unitOfWork.Banners.GetByIdAsync(dto.Id);
        if (banner == null) return;

        _mapper.Map(dto, banner);

        if (dto.ImageFile != null)
        {
            DeleteImage(banner.ImageUrl);
            await SaveImage(banner);
        }

        _unitOfWork.Banners.Update(banner);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var banner = await _unitOfWork.Banners.GetByIdAsync(id);
        if (banner != null)
        {
            DeleteImage(banner.ImageUrl);
            _unitOfWork.Banners.Remove(banner);
            await _unitOfWork.CompleteAsync();
        }
    }

    private async Task SaveImage(Banner banner)
    {
        if (banner.ImageFile != null)
        {
            var uploadDir = Path.Combine(_webHost.WebRootPath, "bannerImages");
            Directory.CreateDirectory(uploadDir);

            var fileName = Guid.NewGuid() + Path.GetExtension(banner.ImageFile.FileName);
            var filePath = Path.Combine(uploadDir, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await banner.ImageFile.CopyToAsync(stream);

            banner.ImageUrl = $"/bannerImages/{fileName}";
        }
    }

    private void DeleteImage(string? imageUrl)
    {
        if (!string.IsNullOrEmpty(imageUrl))
        {
            var path = Path.Combine(_webHost.WebRootPath, imageUrl.TrimStart('/'));
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}

