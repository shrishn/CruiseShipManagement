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
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7061/api/Bookings";
        public BookingController(UserManager<IdentityUser> userManager,  ApplicationDbContext context)
        {
            _userManager = userManager;
            _httpClient = new HttpClient();
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
            if (ModelState.IsValid) 
            {
                var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(obj), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_apiBaseUrl, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Booking Created Successfully";
                    return RedirectToAction("Facility", "Home");
                }
                else
                {
                    TempData["error"] = "Failed to create the booking.";
                    return View(obj);
                }              
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
            if (bookings == null)
            {
                // Handle the case where bookings is null
                throw new Exception("Bookings not found");
            }

            var viewModel = new UserBookingsViewModel()
            {
                UserName = user.Name,
                Bookings = bookings // Ensure bookings is not null
            };

            return View(viewModel);
        }

    }
}
