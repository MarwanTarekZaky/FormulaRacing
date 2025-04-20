namespace Infrastructure.DTO;

public class HomeContentDescriptionDTO
{
    public int Id { get; set; }
    public string? RacesDescription { get; set; }  = string.Empty;
    public string? TracksDescription { get; set; } =  string.Empty;
}