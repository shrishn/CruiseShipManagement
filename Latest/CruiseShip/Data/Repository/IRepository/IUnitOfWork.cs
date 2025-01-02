namespace CruiseShip.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IFacilityRepository Facility { get; }
        void Save();
    }
}
