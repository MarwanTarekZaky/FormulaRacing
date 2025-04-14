using AutoMapper;
using Domain.Models;
using Infrastructure.DTO;
using Infrastructure.IRepository;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Hosting;
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

        public async Task<IEnumerable<CarDTO>> GetAllAsync()
        {
            var cars = await _unitOfWork.Cars.GetAllAsync();
            return _mapper.Map<IEnumerable<CarDTO>>(cars); // Mapping to DTO
        }

        public Task<SelectList> GetSelectListAsync()
        {


           var carList =  GetAllAsync().GetAwaiter().GetResult().Select(u => new SelectListItem
            {
                Text = u.Model,
                Value = u.Id.ToString()
            });
           return Task.FromResult(new SelectList(carList, "Value", "Text"));
        }

        public async Task<CarDTO?> GetByIdAsync(int id)
        {
            var car = await _unitOfWork.Cars.GetByIdAsync(id);
            return car == null ? null : _mapper.Map<CarDTO>(car); // Mapping to DTO
        }

        public async Task AddAsync(CarDTO carDto)
        {
            var car = _mapper.Map<Car>(carDto); // Mapping DTO to Car entity
            await SaveImage(car);
            await _unitOfWork.Cars.AddAsync(car);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(CarDTO carDto)
        {
            var existingCar = await _unitOfWork.Cars.GetByIdAsync(carDto.Id);
            if (existingCar == null) return;

            _mapper.Map(carDto, existingCar); // Mapping DTO to Car entity

            // Handle image logic
            if (carDto.ImageUrl != null)
            {
                DeleteImage(existingCar.ImageUrl);
                await SaveImage(existingCar);
                existingCar.ImageUrl = carDto.ImageUrl;
            }

            _unitOfWork.Cars.Update(existingCar);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var car = await _unitOfWork.Cars.GetByIdAsync(id);
            if (car == null) return;

            DeleteImage(car.ImageUrl);
            _unitOfWork.Cars.Remove(car);
            await _unitOfWork.CompleteAsync();
        }

        private async Task SaveImage(Car car)
        {
            if (car.ImageFile != null)
            {
                var uploadDir = Path.Combine(_webHost.WebRootPath, "images", "cars");
                Directory.CreateDirectory(uploadDir);

                var fileName = Guid.NewGuid() + Path.GetExtension(car.ImageFile.FileName);
                var filePath = Path.Combine(uploadDir, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await car.ImageFile.CopyToAsync(fileStream);

                car.ImageUrl = $"/images/cars/{fileName}";
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