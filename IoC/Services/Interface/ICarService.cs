using System.Linq.Expressions;
using Domain.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IoC.Services.Interface;

public interface ICarService
{
    Task<IEnumerable<CarDTO>> GetAllAsync(Expression<Func<Car,bool>>? filter = null, Expression<Func<Car, object>>[]? includes = null);
    Task<CarDTO?> GetByIdAsync(int id);
    Task AddAsync(CarDTO car);
    Task UpdateAsync(CarDTO car);
    Task DeleteAsync(int id);
    Task<MultiSelectList> GetSelectListAsync();
}