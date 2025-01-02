using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CruiseShip.Models.ViewModels
{
    public class FacilityVM
    {
        public Facility Facility { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> AdminUsers { get; set; }
       
    }
}
