namespace CruiseShip.Models.ViewModels
{
    public class UserBookingsViewModel
    {
        public string UserName { get; set; }
        //public IEnumerable<Facility> Facilities { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }

    }
}
