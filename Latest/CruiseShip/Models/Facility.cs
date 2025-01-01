using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace CruiseShip.Models
{
    public class Facility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name field is required.")]
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public decimal Fee { get; set; }
        [DisplayName("Available Slots")]
        [Required, Range(1, 100)]
        public int AvailableSlots { get; set; }

        [DisplayName("Created By")]
        [ForeignKey("CreatedBy")]
        public string? CreatedBy { get; set; }

        // Navigation property
        //[NotMapped]
        //public IdentityUser CreatedByUser { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
