using System.Net.NetworkInformation;
using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;
using CruiseShip.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace CruiseShip.Areas.Voyager.Controllers
{
    [Area("Voyager")]   

    public class BookingController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public BookingController(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> RequestFacility()
        {
            var user = await _userManager.GetUserAsync(User);
            BookingVM booking = null; // Initialize the variable to null or a default instance

            if (user != null)
            {
                var userId = user.Id;
                booking = new BookingVM()
                {
                    Booking = new Booking() // Initialize the Booking object
                    {
                        VoyagerId = userId // Assign VoyagerId to the Booking object
                    }
                };
            }
            List<string> facilityNames = _unitOfWork.Facility
                .GetAll(includeProperties: "CreatedByUser")
                .Select(f => f.Name) // Assuming "Name" is the property for the facility name
                .ToList();
            booking.Facilities = facilityNames.Select(f => new SelectListItem
            {
                Text = f,
                Value = f // Adjust the Value as needed (it could be an ID or something else)
            }).ToList();
            return View(booking);
        }
        [HttpPost]
        public async Task<IActionResult> RequestFacility(BookingVM obj)
        {
            return RedirectToAction("Index");
        }

    }
}
