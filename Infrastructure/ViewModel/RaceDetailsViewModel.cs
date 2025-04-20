using Infrastructure.DTO;

namespace Infrastructure.ViewModel;

public class RaceDetailsViewModel
{
    
    public RaceDTO Race { get; set; } = new RaceDTO(); // Details of the selected race
    public List<CarDTO> Cars { get; set; } = new List<CarDTO>(); // Cars participating in that race
}