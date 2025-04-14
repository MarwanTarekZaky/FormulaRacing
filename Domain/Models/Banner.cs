using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Domain.Models;

// Domain/Models/Banner.cs
public class Banner
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [NotMapped]
    [Display(Name = "Upload Image")]
    public IFormFile ImageFile { get; set; }

    public string? ImageUrl { get; set; }
}
