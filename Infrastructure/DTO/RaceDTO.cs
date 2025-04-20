using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Infrastructure.DTO;

public class RaceDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public double Length { get; set; }

    public DateTime Date { get; set; }

    public string? ImageUrl { get; set; }
    
    [Display(Name = "Race Location")]
    public int? LocationId { get; set; }

    public string? LocationName { get; set; } // Optional, for display only

    public int Occupancy { get; set; }

    // No file upload here
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    
    public bool ShowOnHomepage { get; set; } = false; // Show in the main home page for the Client UI , Default = hidden
    
    [Display(Name = "Visibile option")]
    public bool Visibility { get; set; } = false; // Show in the pages for the Client UI , Default = Not Visible
    
    [Display(Name = "Booked seats number")]
    public int NumberOfBookedSeats { get; set; } = 0;
    
    [Display(Name = "Select Cars")]
    public List<int> SelectedCarIds { get; set; } = new List<int>();

    public IEnumerable<SelectListItem>? CarList { get; set; }

    
}
