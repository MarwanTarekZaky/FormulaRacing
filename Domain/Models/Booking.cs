using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Domain.Models;

public class Booking
{
    public int Id { get; set; }

    [Display(Name = "User Name")]
    [Required, MaxLength(100)]
    public string UserName { get; set; }

    [Display(Name = "User Email")]
    [Required, EmailAddress]
    public string UserEmail { get; set; }

    [Display(Name = "Confirmed")]
    public bool IsConfirmed { get; set; } = false;

    // One-to-One with Car
    [Display(Name = "Booked Car")]
    public int CarId { get; set; }
    [ValidateNever]
    public Car Car { get; set; }

    // One-to-One with Race
    [Display(Name = "Associated Race")]
    public int RaceId { get; set; }
    [ValidateNever]
    public Race Race { get; set; }
}
