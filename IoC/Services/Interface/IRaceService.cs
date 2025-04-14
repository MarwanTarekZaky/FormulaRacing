using System.Linq.Expressions;
using Domain.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IoC.Services.Interface;

public interface IRaceService
{
    Task<IEnumerable<RaceDTO>> GetAllAsync( Expression<Func<Race,bool>>? filter = null);
    Task<RaceDTO?> GetByIdAsync(int id);
    Task AddAsync(RaceDTO raceDto);
    Task UpdateAsync(RaceDTO raceDto);
    Task DeleteAsync(int id);
    Task<SelectList> GetSelectListAsync();
}
