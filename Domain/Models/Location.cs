using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Location
{
    public int Id { get; set; }
    [MaxLength(100)] public required string Name { get; set; } = string.Empty;
    public ICollection<Race> Races { get; set; } =  new List<Race>(); //Navigation property
} 