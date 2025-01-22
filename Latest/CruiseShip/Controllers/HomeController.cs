using Microsoft.AspNetCore.Mvc;

namespace CruiseShip.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
