using System.ComponentModel.DataAnnotations;

namespace CruiseShipManagement.Models
{
    public class OrderItem
    {
        [Key] public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        [Required] public int Quantity { get; set; }
        public Order Order { get; set; }
        public CateringMenu MenuItem { get; set; }
    }

}
