using System.ComponentModel.DataAnnotations;

namespace CruiseAPI.Models.ViewModels
{
    public class BookingVM
    {
        public int Id { get; set; }
        public string VoyagerId { get; set; }
        public int? FacilityId { get; set; }
        public int? RoomId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = "Pending";//Pending, Booked, Cancelled, Rejected
    }
}
