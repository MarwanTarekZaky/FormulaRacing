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

public class CarService : ICarService
{
   private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;
        private readonly IMapper _mapper;

        public CarService(IUnitOfWork unitOfWork, IWebHostEnvironment webHost, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _webHost = webHost;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarDTO>> GetAllAsync(Expression<Func<Car,bool>>? filter = null, Expression<Func<Car, object>>[]? includes = null )
        {
            var cars = await _unitOfWork.Cars.GetAllAsync(filter, c => c.CarImages, c => c.Brand);
            var carDtos =  _mapper.Map<IEnumerable<CarDTO>>(cars); // Mapping to DTO
            return carDtos;
            
        }
    
        public Task<MultiSelectList> GetSelectListAsync()
        {
           var carList =  GetAllAsync().GetAwaiter().GetResult().Select(u => new 
            {
                Text = u.Model,
                Value = u.Id.ToString()
            }).ToList();
           return Task.FromResult(new MultiSelectList(carList, "Value", "Text"));
        }

        public async Task<CarDTO?> GetByIdAsync(int id )
        {
            var car = await _unitOfWork.Cars.GetByIdAsync(id, c => c.CarImages, c => c.Brand);
            return car == null ? null : _mapper.Map<CarDTO>(car); // Mapping to DTO
        }   

        public async Task AddAsync(CarDTO carDto)
        {
            var car = _mapper.Map<Car>(carDto); // Mapping DTO to Car entity
            foreach (var image in carDto.ImageFiles)
            {
                car.CarImages.Add(new CarImage{ImageUrl = await SaveImage(image)});
            }

            car.Brand = null;
            await _unitOfWork.Cars.AddAsync(car);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(CarDTO carDto)
        {
            var existingCar = await _unitOfWork.Cars.GetByIdAsync(carDto.Id);
            if (existingCar == null) return;

            _mapper.Map(carDto, existingCar); // Mapping DTO to Car entity

            // Handle delete image logic
            foreach (var imageUrl in existingCar.CarImages.Select(ci => ci.ImageUrl).ToList())
            {
                DeleteImage(imageUrl);
            }

            //Handle adding car images
            foreach (var image in carDto.ImageFiles)
            {
                existingCar.CarImages.Add(new CarImage{ImageUrl = await SaveImage(image)});
            }
            
            _unitOfWork.Cars.Update(existingCar);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var car = await _unitOfWork.Cars.GetByIdAsync(id);
            if (car == null) return;

            foreach (var imageUrl in car.CarImages.Select(ci => ci.ImageUrl).ToList())
            {
                DeleteImage(imageUrl);
            }
            _unitOfWork.Cars.Remove(car);
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