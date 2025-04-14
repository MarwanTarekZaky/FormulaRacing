using Domain.Models;

namespace Infrastructure.DTO;

public class BookingDTO
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; }
    public int CarId { get; set; }
    public string CarName { get; set; } // Add CarName property
    public Car? Car { get; set; }
    
    public int RaceId { get; set; }
    public string RaceName { get; set; } // Add RaceName property
    public Race? Race { get; set; }
}
