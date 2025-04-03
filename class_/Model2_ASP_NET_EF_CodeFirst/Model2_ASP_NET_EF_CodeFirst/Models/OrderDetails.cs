using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model2_ASP_NET_EF_CodeFirst.Models;

public class OrderDetails
{
    [Key]
    public Guid OrderDetailId { get; set; }
    [ForeignKey("Order")]
    public Guid OrderId { get; set; }
    
    public Order Order { get; set; }
    
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
    
    public int UnitPrice { get; set; }
    
    public int Quantity { get; set; }
    
    public double Discount { get; set; }
}