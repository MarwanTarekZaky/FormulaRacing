using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.DTO;

public class HomePageContentDTO
{
    public int Id { get; set; }
    public required string RaceParagraph { get; set; }
    public required string BannerText { get; set; }
    public required string ImageParagraph { get; set; }
    
    [NotMapped]
    [Display(Name = "Upload Video Background")]
    public required IFormFile VideoFile { get; set; }

    public string? VideoPath { get; set; }
}