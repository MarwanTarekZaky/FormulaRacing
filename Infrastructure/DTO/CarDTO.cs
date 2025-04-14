using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.DTO;

public class CarDTO
{
    public int Id { get; set; }
    public string BrandName { get; set; } = string.Empty;
    public int MaxSpeed { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public double Cost { get; set; }
    public string? ImageUrl { get; set; }
    [Display(Name = "Upload Image")]
    public IFormFile? ImageFile { get; set; }

}