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

    [Display(Name = "Race Location")]
    public int? LocationId { get; set; } // Foreign Key
    [ForeignKey("LocationId")]
    [Display(Name = "Race Location")]
    public Location? Location { get; set; }
    [MaxLength(100)]
    public string? LocationName { get; set; } // Optional, for display only

    [Range(1, 10000)]
    public int Occupancy { get; set; } // Max number of participants or teams
    
    [Display(Name = "Show on Homepage ")]
    public bool ShowOnHomepage { get; set; } = false; // Show in the main home page for the Client UI , Default = hidden
    
    public bool Visibility { get; set; } = false; // Show in the pages for the Client UI , Default = Not Visible
    
    [Display(Name = "Booked seats number")]
    public int NumberOfBookedSeats { get; set; } = 0;
    
    // Navigation property for the many-to-many relation
    public ICollection<RaceCar> RaceCars { get; set; } = new List<RaceCar>();

}
