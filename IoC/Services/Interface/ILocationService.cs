using System.Linq.Expressions;
using Domain.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IoC.Services.Interface;

public interface ILocationService
{
    Task<IEnumerable<LocationDTO>> GetAllAsync( Expression<Func<Location,bool>>? filter = null);
    Task<LocationDTO?> GetByIdAsync(int id);
    Task AddAsync(LocationDTO locationDto);
    Task UpdateAsync(LocationDTO locationDto);
    Task DeleteAsync(int id);
    Task<SelectList> GetSelectListAsync(Expression<Func<Location,bool>>? filter = null);
}
