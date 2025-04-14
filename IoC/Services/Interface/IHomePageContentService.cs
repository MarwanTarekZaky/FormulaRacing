using Infrastructure.DTO;

namespace IoC.Services.Interface;

// IoC/Services/Interface/IHomePageContentService.cs
public interface IHomePageContentService
{
    Task<IEnumerable<HomePageContentDTO>> GetAllAsync();
    Task<HomePageContentDTO> GetByIdAsync(int id);
    Task AddAsync(HomePageContentDTO dto);
    Task UpdateAsync(HomePageContentDTO dto);
    Task DeleteAsync(int id);
}
