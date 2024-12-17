using System.ComponentModel.DataAnnotations;

namespace CruiseShipManagement.Models
{
    public class CateringOrder
    {
        [Key] public int OrderId { get; set; } // Primary Key
        public int VoyagerId { get; set; } //Foreign Key
        public string Item { get; set; }
        public int Quantity { get; set; }
        public string OrderStatus { get; set; }

        public Voyager Voyager { get; set; }
    }
}
