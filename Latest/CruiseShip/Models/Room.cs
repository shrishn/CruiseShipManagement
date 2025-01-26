using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace CruiseShip.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoomNumber { get; set; }

        [MaxLength(100)]
        public string Type { get; set; }

        [Required]
        public decimal Fee { get; set; }

        [Required]
        public int AvailableSlots { get; set; }
        
        public string CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        [ValidateNever]
        [JsonIgnore]
        public IdentityUser? CreatedByUser { get; set; }

        // Navigation property
        [ValidateNever]
        [JsonIgnore]
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}