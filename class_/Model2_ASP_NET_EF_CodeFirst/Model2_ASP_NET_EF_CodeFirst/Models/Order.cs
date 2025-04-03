using System.ComponentModel.DataAnnotations;

namespace Model2_ASP_NET_EF_CodeFirst.Models;

public class Order
{
    [Key]
    public Guid OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShipAddress { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; }
}