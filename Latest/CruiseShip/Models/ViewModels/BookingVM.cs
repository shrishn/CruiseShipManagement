namespace CruiseShip.Models.ViewModels
{
    public class BookingVM
    {
        public string VoyagerId { get; set; }
        public int FacilityId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
    }
}
