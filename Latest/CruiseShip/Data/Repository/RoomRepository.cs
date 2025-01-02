using System.Linq.Expressions;
using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;

namespace CruiseShip.Data.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly ApplicationDbContext _db;
        public RoomRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
       

        public void Update(Room obj)
        {
            _db.Rooms.Update(obj);
        }
    }
}
