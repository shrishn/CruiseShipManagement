using CruiseShip.Models;

namespace CruiseShip.Data.Repository.IRepository
{
    public interface IBillRepository : IRepository<Bill>
    {
        void Update(Bill obj);
    }
}
