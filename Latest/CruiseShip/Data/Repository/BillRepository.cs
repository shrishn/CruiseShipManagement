using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;
namespace CruiseShip.Data.Repository
{
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        private readonly ApplicationDbContext _db;
        public BillRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public void Update(Bill obj)
        {
            _db.Bills.Update(obj);
        }
    }
}
