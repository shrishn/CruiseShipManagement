using CruiseShip.Data;
using CruiseShip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruiseShip.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookingController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult ViewFacilityBookings()
        {
            List<Booking> bookingList = _db.Bookings.Where(b => b.RoomId == null).ToList();
            return View(bookingList);
         
        }
    }
}
