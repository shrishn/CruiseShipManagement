using System.ComponentModel;
using CruiseShip.Data.Repository.IRepository;

namespace CruiseShip.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IFacilityRepository Facility { get; private set; }
        public IRoomRepository Room { get; private set; }
        public IBookingRepository Booking { get; private set; }
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Facility = new FacilityRepository(_db);
            Room = new RoomRepository(_db);
            Booking = new BookingRepository(_db);
        }


        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
