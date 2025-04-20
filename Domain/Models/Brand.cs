using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Brand
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    // Navigation: one Brand to many Cars
    public ICollection<Car> Cars { get; set; } = new List<Car>();
}