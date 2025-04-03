using System.ComponentModel.DataAnnotations;

namespace Module2_ASP_NET_MVC_EF_CodeFirst.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipAddress { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

