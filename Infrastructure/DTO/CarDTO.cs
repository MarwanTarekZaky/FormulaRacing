using System.ComponentModel.DataAnnotations;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Infrastructure.DTO;

public class CarDTO
{
    public int Id { get; set; }
    
    [Display(Name = "Brand")]
    [Required]
    public int BrandId { get; set; } // FK
    
    public string? BrandName { get; set; }
    public int MaxSpeed { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public double Cost { get; set; }
    public string? ImageUrl { get; set; }
   

    //optional List of image Urls
    public ICollection<CarImage>? CarImages { get; set; } 
    
    
    public List<string>? ImagesUrl { get; set; }= new List<string>();
    
    
    
    [Display(Name = "Upload Images")]
    public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();


    [Display(Name = "Visibility option")]
    public bool Visibility { get; set; } = true; // Show in the pages for the Client UI , Default = Visible
    
    [Display(Name = "Select Races")]
    public List<int> SelectedRaceIds { get; set; } = new List<int>();

    public IEnumerable<SelectListItem>? RaceList { get; set; }
}