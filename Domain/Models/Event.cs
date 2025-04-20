using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

public class Event
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Url]
        [Display(Name = "YouTube Video Link")]
        public string? VideoUrl { get; set; }
        
        [Display(Name = "Show on Homepage ")]
        public bool ShowOnHomepage { get; set; } = false; // Show in the main home page for the Client UI , Default = hidden
        
        public bool Visibility { get; set; } = true; // Show in the pages for the Client UI , Default = Visible
    }

