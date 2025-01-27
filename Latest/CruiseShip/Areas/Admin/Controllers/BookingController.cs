using CruiseShip.Data;
using CruiseShip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CruiseShip.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7061/api/Bookings";

        public BookingController(ApplicationDbContext db)
        {
            _db = db;
            _httpClient = new HttpClient();
        }

        public IActionResult ViewFacilityBookings()
        {
            List<Booking> bookingList = _db.Bookings.Include(p=>p.Voyager).Include(p=>p.Facility).Where(b => b.RoomId == null).ToList();
            return View(bookingList);
         
        }
        [HttpGet]
        public async Task <IActionResult> ApproveRequest(int? id) 
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "/" + id);
            Booking booking = new Booking();
           

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                booking = JsonConvert.DeserializeObject<Booking>(jsonData);

            }
            else
            {
                return NotFound();
            }
            booking.Status = "Approved";

            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(booking), System.Text.Encoding.UTF8, "application/json");
            response = await _httpClient.PutAsync(_apiBaseUrl + "/" + booking.Id, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Booking Approved";
                return Json(new { success = true, message = "Booking Approved" });

            }
            else
            {
                TempData["error"] = "Failed to update the facility.";
                return Json(new { success = false, message = "Failed to approved" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> RejectRequest(int? id)
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "/" + id);
            Booking booking = new Booking();


            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                booking = JsonConvert.DeserializeObject<Booking>(jsonData);

            }
            else
            {
                return NotFound();
            }
            booking.Status = "Rejected";

            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(booking), System.Text.Encoding.UTF8, "application/json");
            response = await _httpClient.PutAsync(_apiBaseUrl + "/" + booking.Id, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Booking Rejected";
                return Json(new { success = true, message = "Booking Rejected" });

            }
            else
            {
                TempData["error"] = "Failed to update booking";
                return Json(new { success = false, message = "Failed to approved" });
            }
        }
    }
}
