using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CruiseShip.Models
{
    public class UserProfile:IdentityUser
    {
        [Required]
        public string? Name {  get; set; }
        public string? StreetAddress {  get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PINCode { get; set; }
    }
}
