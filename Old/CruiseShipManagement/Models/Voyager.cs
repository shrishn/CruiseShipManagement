using System.ComponentModel.DataAnnotations;

namespace CruiseShipManagement.Models
{
    public class Voyager
    {
        [Key] public int VoyagerId { get; set; }//Primary Key
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
