using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model2_ASP_NET_EF_CodeFirst.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public String ProductName { get; set; }
    
        public int Price { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
