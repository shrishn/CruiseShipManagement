namespace CruiseShip.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IFacilityRepository Facility { get; }
        IRoomRepository Room { get; }
        IBookingRepository Booking { get; }
        IBillRepository Bill { get; }
        Task SaveAsync();
    }
}
