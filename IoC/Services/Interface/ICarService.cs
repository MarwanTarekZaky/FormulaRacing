using Domain.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IoC.Services.Interface;

public interface ICarService
{
    Task<IEnumerable<CarDTO>> GetAllAsync();
    Task<CarDTO?> GetByIdAsync(int id);
    Task AddAsync(CarDTO car);
    Task UpdateAsync(CarDTO car);
    Task DeleteAsync(int id);
    Task<SelectList> GetSelectListAsync();
}