using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Text.Json.Serialization;

namespace CruiseShip.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [ValidateNever]
        public string VoyagerId { get; set; }

        public int? FacilityId { get; set; }

        public int? RoomId { get; set; }
        [Display(Name ="Booking Date")]

        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";//Pending, Booked, Cancelled, Rejected

        [ForeignKey("VoyagerId")]
        [ValidateNever]
        [JsonIgnore]
        public UserProfile Voyager { get; set; }

        [ForeignKey("FacilityId")]
        [ValidateNever]
        [JsonIgnore]
        public Facility Facility { get; set; }

        [ForeignKey("RoomId")]
        [ValidateNever]
        [JsonIgnore]
        public Room Room { get; set; }
    }
}
