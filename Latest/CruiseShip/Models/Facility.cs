using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

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
        
        [ValidateNever]
        public string ImageURL { get; set; }

        [DisplayName("Created By")]
        [ValidateNever]
        public string CreatedBy { get; set; }

        // Navigation property
        //[NotMapped]
        [ForeignKey("CreatedBy")]
        [ValidateNever]
        
        public UserProfile CreatedByUser { get; set; }

        //Navigation Property
        [ValidateNever]
        [JsonIgnore]
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
