using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public class HomeContentDescription
{
 
    public int Id { get; set; }
    public string? RacesDescription { get; set; }  = string.Empty;
    public string? TracksDescription { get; set; } =  string.Empty;
}