using CruiseShip.Models;

namespace CruiseShip.Data.Repository.IRepository
{
    public interface IFacilityRepository : IRepository<Facility>
    {
        void Update(Facility obj);
        void Save();
    }
}
