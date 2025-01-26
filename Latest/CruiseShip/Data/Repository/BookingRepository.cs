using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;
using Microsoft.EntityFrameworkCore;

namespace CruiseShip.Data.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Booking obj)
        {
            _db.Bookings.Update(obj);
        }
    }
}
