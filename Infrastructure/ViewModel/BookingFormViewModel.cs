using Domain.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Infrastructure.ViewModel;

public class BookingFormViewModel
{
    public Booking? Booking { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem>? Races { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem>? Cars { get; set; }
}
