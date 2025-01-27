using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CruiseShip.Data;
using CruiseShip.Models;
using Microsoft.AspNetCore.Authorization;
using CruiseShip.Data.Repository.IRepository;

namespace CruiseShip.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class BookingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public BookingsController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<IEnumerable<Booking>> GetBookings()
        {
            return await _unitOfWork.Booking.GetAll();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _unitOfWork.Booking.Get(b=>b.Id==id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Booking.Update(booking);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            await _unitOfWork.Booking.Add(booking);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _unitOfWork.Booking.Get(p=>p.Id==id);
            if (booking == null)
            {
                return NotFound();
            }

            await _unitOfWork.Booking.Remove(booking);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
        // GET: api/BookRequests/count/pending
        [HttpGet("count/pending")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetPendingRequestCount()
        {
           
            int pendingCount = await _context.Bookings
                .CountAsync(r => r.Status == "pending");

            return Ok(new
            {
                success = true,
                count = pendingCount
            });
        }
        private bool BookingExists(int id)
        {
            return _context.Bookings.Find(id) != null;
        }
    }
}
