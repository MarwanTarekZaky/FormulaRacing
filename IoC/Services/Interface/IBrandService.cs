using System.Linq.Expressions;
using Domain.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IoC.Services.Interface;

public interface IBrandService
{
    Task<IEnumerable<BrandDTO>> GetAllAsync( Expression<Func<Brand,bool>>? filter = null);
    Task<BrandDTO?> GetByIdAsync(int id);
    Task AddAsync(BrandDTO brandDto);
    Task UpdateAsync(BrandDTO brandDto);
    Task DeleteAsync(int id);
    Task<SelectList> GetSelectListAsync(Expression<Func<Brand,bool>>? filter = null);
}
