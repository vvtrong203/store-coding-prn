using Microsoft.EntityFrameworkCore;
namespace Module2_ASP_NET_MVC_EF_CodeFirst.Models
{
    public class PRN222DBContext: DbContext
    {
        public PRN222DBContext(DbContextOptions<PRN222DBContext> options):base(options) {}

        // Khai báo 2 DBSet đại diện cho 2 collection kiểu: Category, Product
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Khởi tạo dữ liệu ban đầu cho 2 bảng: categories (2 records), product (3 records)
            base.OnModelCreating(modelBuilder);

            // Seed dữ liệu Category
            var category1 = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = "Điện thoại",
                Description = "Các sản phẩm điện thoại di động"
            };

            var category2 = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = "Laptop",
                Description = "Các sản phẩm laptop cao cấp"
            };

            modelBuilder.Entity<Category>().HasData(category1, category2);

            // Seed dữ liệu Product
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "IPhone 16 Pro",
                    Price = 25000000,
                    CategoryId = category1.CategoryId
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Samsung Galaxy S25 Ultra",
                    Price = 24000000,
                    CategoryId = category1.CategoryId
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Macbook Pro M4",
                    Price = 65000000,
                    CategoryId = category2.CategoryId
                }
            );
        }
    }
}
