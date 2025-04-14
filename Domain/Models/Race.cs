using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Domain.Models;

public class Race
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 10000)]
    public double Length { get; set; } // Length in km or miles

    [DataType(DataType.Date)]
    [Display(Name = "Race Date")]
    public DateTime Date { get; set; }

    // This will store the URL of the image after it’s uploaded
    [Display(Name = "Race Image")]
    public string? ImageUrl { get; set; }

    // This is used for the image upload
    [NotMapped]
    [Display(Name = "Upload Image")]
    public IFormFile? ImageFile { get; set; }

    [Required]
    public string Location { get; set; } = string.Empty;

    [Range(1, 10000)]
    public int Occupancy { get; set; } // Max number of participants or teams
    
    [Display(Name = "Show on Homepage ")]
    public bool ShowOnHomepage { get; set; } = false; // Show in the main home page for the Client UI , Default = hidden
}
