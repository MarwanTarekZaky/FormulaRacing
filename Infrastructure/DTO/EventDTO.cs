using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace Infrastructure.DTO;

    public class EventDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        
        [Display(Name = "YouTube Video Link")]
        public string? VideoUrl { get; set; }
        
        public bool ShowOnHomepage { get; set; } = false; // Show in the main home page for the Client UI , Default = hidden
    }

