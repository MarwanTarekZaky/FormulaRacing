using Infrastructure.DTO;

namespace IoC.Services.Interface;

// IoC/Services/Interface/IBannerService.cs
public interface IBannerService
{
    Task<IEnumerable<BannerDTO>> GetAllAsync();
    Task<BannerDTO> GetByIdAsync(int id);
    Task AddAsync(BannerDTO dto);
    Task UpdateAsync(BannerDTO dto);
    Task DeleteAsync(int id);
}
