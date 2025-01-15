using Microsoft.AspNetCore.Mvc;

namespace CruiseShip.Areas.Voyager.Controllers
{
    [Area("Voyager")]   
    public class BookingController : Controller
    {
        public IActionResult RequestFacility()
        {
            return View();
        }
    }
}
