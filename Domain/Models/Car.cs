using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace Domain.Models;

public class Car
{
    public int Id { get; set; }
    public int? BrandId { get; set; }

    [ForeignKey("BrandId")]
    public Brand? Brand { get; set; } = null!;
    public required int MaxSpeed { get; set; }
    public required string Color { get; set; }
   public required string Model { get; set; }
    [Display(Name = "Cost per ride")]
    [Range(10, 10000)]  
    public required double Cost { get; set; }
    
    // Navigation property for multiple images
    public ICollection<CarImage> CarImages { get; set; } = new List<CarImage>();
    
    public bool Visibility { get; set; } = true; // Show in the pages for the Client UI , Default = Visible
    
    // Navigation property for the many-to-many relation
    public ICollection<RaceCar> RaceCars { get; set; } = new List<RaceCar>();
    
}