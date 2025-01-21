using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CruiseShip.Models.ViewModels
{
    public class BookingVM
    {
        public Booking Booking { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Facilities { get; set; }
    }
}
