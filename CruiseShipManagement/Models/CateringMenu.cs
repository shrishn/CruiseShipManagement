using System.ComponentModel.DataAnnotations;

namespace CruiseShipManagement.Models
{
    public class CateringMenu
    {
        [Key] public int MenuItemId { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        [Required] public decimal Price { get; set; }
        public bool Availability { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
