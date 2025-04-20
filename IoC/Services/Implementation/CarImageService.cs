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

public class CarImageService : ICarImageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHost;
    public CarImageService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHost)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _webHost = webHost;
    }

    public async Task<IEnumerable<CarImage>> GetAllAsync(Expression<Func<CarImage, bool>>? filter = null)
    {
        IEnumerable<CarImage> carImages = null;

        return carImages = await _unitOfWork.CarImages.GetAllAsync(filter);

    }

    public async Task<CarImage?> GetByIdAsync(int id)
    {
       return (await _unitOfWork.CarImages.GetByIdAsync(id));
    }

    public async Task AddAsync(CarImage carImage)
    {
        carImage.ImageUrl = await SaveImage(carImage.ImageFile);
        await _unitOfWork.CarImages.AddAsync(carImage);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateAsync(CarImage carImage)
    {
        if (carImage == null) return;
        DeleteImage(carImage.ImageUrl);
        carImage.ImageUrl = await SaveImage(carImage.ImageFile);
        _unitOfWork.CarImages.Update(carImage);
        await _unitOfWork.CompleteAsync();
    }
    
    private async Task<string> SaveImage(IFormFile? file)
    {
        string imagePath = "";
        if (file != null)
        {
            var uploadDir = Path.Combine(_webHost.WebRootPath, "images", "cars");
            Directory.CreateDirectory(uploadDir);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadDir, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return $"/images/cars/{fileName}";
        }
        return imagePath;
    }
    public async Task DeleteAsync(int id)
    {
        var carImage = await _unitOfWork.CarImages.GetByIdAsync(id);
        if (carImage != null)
        {
            DeleteImage(carImage.ImageUrl);
            _unitOfWork.CarImages.Remove(carImage);
            await _unitOfWork.CompleteAsync();
        }
    }
    
    private void DeleteImage(string? imageUrl)
    {
        if (!string.IsNullOrEmpty(imageUrl))
        {
            var path = Path.Combine(_webHost.WebRootPath, imageUrl.TrimStart('/'));
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }

 
}
