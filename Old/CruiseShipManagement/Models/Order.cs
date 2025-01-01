using System.ComponentModel.DataAnnotations;

namespace CruiseShipManagement.Models
{
    public class Order
    {
        [Key] public int OrderId { get; set; }
        public int UserId { get; set; }
        [Required] public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public Users User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
