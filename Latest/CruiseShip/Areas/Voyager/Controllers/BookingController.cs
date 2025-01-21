using System.Net.NetworkInformation;
using CruiseShip.Data;
using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;
using CruiseShip.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;



namespace CruiseShip.Areas.Voyager.Controllers
{
    [Area("Voyager")]
    [Authorize(Roles = "Voyager")]

    public class BookingController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public BookingController(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<IActionResult> RequestFacility(int? id)
        {
            
            Booking booking = new Booking(); // Initialize the variable to null or a default instance
            booking.FacilityId = id;
            return View(booking);
        }
        [HttpPost]
        public async Task<IActionResult> RequestFacility(Booking obj)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userId = user.Id;
                obj.VoyagerId = userId; // Assign VoyagerId to the Booking object
            }
            if (ModelState.IsValid) {
                _context.Bookings.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Facility", "Home");
            }
            

            return View(obj);
            
        }
        public async Task<IActionResult> UserBookings()
        {
            var user = await _userManager.GetUserAsync(User) as UserProfile;
            var bookings = await _context.Bookings
                                         .Include(b => b.Facility)
                                         .Where(b => b.VoyagerId == user.Id)
                                         .ToListAsync();
            var viewModel = new UserBookingsViewModel()
            {
                UserName = user.Name,
                Facilities = bookings.Select(b => b.Facility).ToList()
            };

            return View(viewModel);
        }

    }
}
