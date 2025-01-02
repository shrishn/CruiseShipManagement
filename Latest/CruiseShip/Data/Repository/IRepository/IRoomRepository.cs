using CruiseShip.Models;

namespace CruiseShip.Data.Repository.IRepository
{
    public interface IRoomRepository : IRepository<Room>
    {
        void Update(Room obj);
    }
}
