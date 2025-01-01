using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CruiseShip.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public string VoyagerId { get; set; }

        public int? FacilityId { get; set; }

        public int? RoomId { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [ForeignKey("VoyagerId")]
        public IdentityUser Voyager { get; set; }

        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }
    }
}
