using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.DTO;

public class RaceDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public double Length { get; set; }

    public DateTime Date { get; set; }

    public string? ImageUrl { get; set; }

    public string Location { get; set; } = string.Empty;

    public int Occupancy { get; set; }

    // No file upload here
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    
    public bool ShowOnHomepage { get; set; } = false; // Show in the main home page for the Client UI , Default = hidden

}
