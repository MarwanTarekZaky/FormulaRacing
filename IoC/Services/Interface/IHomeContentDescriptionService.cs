using System.Linq.Expressions;
using Domain.Models;
using Infrastructure.DTO;

namespace IoC.Services.Interface;

// IoC/Services/Interface/IHomeContentDescriptionService.cs
public interface IHomeContentDescriptionService
{
    Task UpdatePartialAsync(HomeContentDescriptionDTO dto);
    Task<HomeContentDescriptionDTO?> GetLastAsync(Expression<Func<HomeContentDescription, bool>>? predicate = null,
        Expression<Func<HomeContentDescription, object>>? orderBy = null,params Expression<Func<HomeContentDescription, object>>[]? includes);
    Task AddAsync(HomeContentDescriptionDTO dto);
    
}
