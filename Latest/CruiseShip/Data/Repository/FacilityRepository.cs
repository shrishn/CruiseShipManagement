using System.Linq.Expressions;
using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models;

namespace CruiseShip.Data.Repository
{
    public class FacilityRepository : Repository<Facility>, IFacilityRepository
    {
        private readonly ApplicationDbContext _db;
        public FacilityRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
       

        public void Update(Facility obj)
        {
            _db.Facilities.Update(obj);
        }
    }
}
