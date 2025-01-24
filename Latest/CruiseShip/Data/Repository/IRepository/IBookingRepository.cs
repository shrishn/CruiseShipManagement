using CruiseShip.Models;

namespace CruiseShip.Data.Repository.IRepository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void Update(Booking obj);
    }
}
