using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace Domain.Models;

public class Car
{
    public int Id { get; set; }
    [MaxLength(50)]
    public required string BrandName { get; set; }
    public required int MaxSpeed { get; set; }
    public required string Color { get; set; }
   public required string Model { get; set; }
    [Display(Name = "Cost per ride")]
    [Range(10, 10000)]
    public required double Cost { get; set; }
    [NotMapped]
    [Display(Name = "Upload Image")]
    public IFormFile ImageFile { get; set; }
    [Display(Name = "Image Url")]
    public string? ImageUrl { get; set; }
    
}