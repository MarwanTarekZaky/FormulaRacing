using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Domain.Models
{
    public class CarImage
    {
        public int Id { get; set; }

        
        public string? ImageUrl { get; set; }

        // Foreign key to the Car entity
        public int CarId { get; set; }

        // Navigation property
        public Car? Car { get; set; }
        
        public bool Hidden { get; set;  }
        [NotMapped]
        public IFormFile? ImageFile { get; set; } =  null;

        public int DisplayOrder { get; set; } = 0;
    }
}