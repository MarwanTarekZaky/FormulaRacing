using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTO;

public class LocationDTO
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}