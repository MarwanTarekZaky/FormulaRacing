using System.Linq.Expressions;
using Domain.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Http;

namespace IoC.Services.Interface;

public interface IEventService
{
    Task<IEnumerable<EventDTO>> GetAllAsync(Expression<Func<Event,bool>>? filter = null);
    Task<EventDTO> GetByIdAsync(int id);
    Task AddAsync(EventDTO eventDto);
    Task UpdateAsync(EventDTO eventDto);
    Task DeleteAsync(int id);
}