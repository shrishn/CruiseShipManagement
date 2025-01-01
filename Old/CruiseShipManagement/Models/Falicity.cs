using System.ComponentModel.DataAnnotations;

namespace CruiseShipManagement.Models
{
    public class Facility
    {
        [Key] public int FacilityId { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Availability { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }

}
