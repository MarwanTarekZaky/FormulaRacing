using System.Linq.Expressions;
using Domain.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IoC.Services.Interface;

public interface IRaceService
{
    Task<IEnumerable<RaceDTO>> GetAllAsync( Expression<Func<Race,bool>>? filter = null);
    Task<RaceDTO?> GetByIdAsync(int id);
    Task<int> AddAsync(RaceDTO raceDto);
    Task<int> UpdateAsync(RaceDTO raceDto);
    Task DeleteAsync(int id);
    Task<SelectList> GetSelectListAsync(Expression<Func<Race,bool>>? filter = null);
}
