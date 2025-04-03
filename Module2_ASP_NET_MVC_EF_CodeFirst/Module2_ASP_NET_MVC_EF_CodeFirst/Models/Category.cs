using System.ComponentModel.DataAnnotations;

namespace Module2_ASP_NET_MVC_EF_CodeFirst.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
