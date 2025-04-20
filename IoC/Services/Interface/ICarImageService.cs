using System.Linq.Expressions;
using Domain.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IoC.Services.Interface;

public interface ICarImageService
{
    Task<IEnumerable<CarImage>> GetAllAsync( Expression<Func<CarImage,bool>>? filter = null);
    Task<CarImage?> GetByIdAsync(int id);
    Task AddAsync(CarImage carImage);   
    Task UpdateAsync(CarImage carImage);
    Task DeleteAsync(int id);

}
