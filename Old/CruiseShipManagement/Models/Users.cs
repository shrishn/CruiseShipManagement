using System.ComponentModel.DataAnnotations;

namespace CruiseShipManagement.Models
{
    public class Users
    {
        [Key] public int UserId { get; set; }
        [Required] public string Username { get; set; }
        public string Role { get; set; }
        [Required] public string Email { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }

}
