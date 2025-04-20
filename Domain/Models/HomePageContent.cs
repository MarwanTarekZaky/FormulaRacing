using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Domain.Models;

public class HomePageContent
{
    public int Id { get; set; }

    public required string BannerText { get; set; }
    
    [NotMapped]
    [Display(Name = "Upload Video Background")]
    public required IFormFile VideoFile { get; set; }

    public string? VideoPath { get; set; }
}