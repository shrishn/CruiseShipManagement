using System.ComponentModel;
using CruiseShip.Data.Repository.IRepository;

namespace CruiseShip.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IFacilityRepository Facility { get; private set; }
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Facility = new FacilityRepository(_db);
        }
        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
