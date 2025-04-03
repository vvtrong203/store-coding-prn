using System.ComponentModel.DataAnnotations;

namespace Model2_ASP_NET_EF_CodeFirst.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; } = Guid.NewGuid(); 

        [Required]
        [MaxLength(100)] 
        public string CategoryName { get; set; } = string.Empty;

        [MaxLength(500)] 
        public string? Description { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}