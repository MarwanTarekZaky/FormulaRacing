using Microsoft.AspNetCore.Http;

namespace Infrastructure.DTO;

// Infrastructure/DTO/BannerDTO.cs
public class BannerDTO
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public IFormFile? ImageFile { get; set; }

    public string? ImageUrl { get; set; }
}

