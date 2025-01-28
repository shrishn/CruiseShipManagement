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
        private readonly string _apiBookings = "https://localhost:7061/api/Bookings";
        private readonly string _apiBills = "https://localhost:7061/api/Bills";

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
        public async Task<IActionResult> ApproveRequest(int? id)
        {
            if (id == null)
            {
                return BadRequest("Invalid booking ID.");
            }

            var response = await _httpClient.GetAsync(_apiBookings + "/" + id);
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
            response = await _httpClient.PutAsync(_apiBookings + "/" + booking.Id, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Booking Approved";
                Bill bill = new Bill()
                {
                    VoyagerId = booking.VoyagerId,
                    Amount = booking.Facility.Fee,
                    Status = "Generated",
                    BookingId = booking.Id
                };
                jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(bill), System.Text.Encoding.UTF8, "application/json");
                response = await _httpClient.PostAsync(_apiBills, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Booking Approved" });
                }
                else
                {
                    return Json(new { success = true, message = "Bill Generation Error" });
                }
            }
            else
            {
                TempData["error"] = "Failed to update the facility.";
                return Json(new { success = false, message = "Failed to approve" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> RejectRequest(int? id)
        {
            var response = await _httpClient.GetAsync(_apiBookings + "/" + id);
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
            response = await _httpClient.PutAsync(_apiBookings + "/" + booking.Id, jsonContent);

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
