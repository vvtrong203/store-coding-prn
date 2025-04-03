using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module2_ASP_NET_MVC_EF_CodeFirst.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId {  get; set; }
        public Category Category { get; set; }
    }
}
