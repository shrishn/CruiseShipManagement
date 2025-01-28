using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace CruiseShip.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }

        public string VoyagerId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }//Generated Paid

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int BookingId { get; set; }
        [ForeignKey("BookingId")]
        [ValidateNever]
        
        public Booking Booking { get; set; }

        [ForeignKey("VoyagerId")]
        [ValidateNever]
        [JsonIgnore]
        public IdentityUser Voyager { get; set; }
    }
}
