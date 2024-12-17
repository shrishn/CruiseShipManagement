using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CruiseShipManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Custom properties like Role, Orders, etc.
        public string Role { get; set; }
        
    }
}
