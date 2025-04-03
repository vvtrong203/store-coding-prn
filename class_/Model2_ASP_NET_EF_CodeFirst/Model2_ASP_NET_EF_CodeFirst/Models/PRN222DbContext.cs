using Microsoft.EntityFrameworkCore;

namespace Model2_ASP_NET_EF_CodeFirst.Models
{
    public class PRN222DbContext(DbContextOptions<PRN222DbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // var category1 = new Category { CategoryId = Guid.NewGuid(), CategoryName = "Electronics" };
            // var category2 = new Category { CategoryId = Guid.NewGuid(), CategoryName = "Clothing" };
            // var category3 = new Category { CategoryId = Guid.NewGuid(), CategoryName = "Home & Kitchen" };
            //
            // modelBuilder.Entity<Category>().HasData(category1, category2, category3);
            //
            // modelBuilder.Entity<Product>().HasData(
            //     new Product { ProductId = Guid.NewGuid(), ProductName = "Laptop", Price = 1000, CategoryId = category1.CategoryId },
            //     new Product { ProductId = Guid.NewGuid(), ProductName = "T-Shirt", Price = 20, CategoryId = category2.CategoryId },
            //     new Product { ProductId = Guid.NewGuid(), ProductName = "Blender", Price = 50, CategoryId = category3.CategoryId }
            // );
        }
        
        
    }
}