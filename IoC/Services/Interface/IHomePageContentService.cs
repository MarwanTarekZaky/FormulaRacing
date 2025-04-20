using Infrastructure.DTO;

namespace IoC.Services.Interface;

// IoC/Services/Interface/IHomePageContentService.cs
public interface IHomePageContentService
{
    Task<HomePageContentDTO> Get();
    Task UpdateAsync(HomePageContentDTO dto);
    
}
