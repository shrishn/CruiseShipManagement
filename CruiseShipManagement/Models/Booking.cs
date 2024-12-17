using System.ComponentModel.DataAnnotations;

namespace CruiseShipManagement.Models
{
    public class Booking
    {
        [Key] public int BookingId { get; set; }
        public int UserId { get; set; }
        public int FacilityId { get; set; }
        [Required] public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public Users User { get; set; }
        public Facility Facility { get; set; }
    }

}
