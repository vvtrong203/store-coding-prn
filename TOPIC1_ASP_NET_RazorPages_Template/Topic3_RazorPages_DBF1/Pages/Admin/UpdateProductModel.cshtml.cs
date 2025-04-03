using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topic3_RazorPages_DBF1.Modelss;

namespace Topic3_RazorPages_DBF1.Pages.Admin
{
    public class UpdateProductModel : PageModel
    {
        private readonly Prn222dbFirstContext _context;

        public UpdateProductModel(Prn222dbFirstContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = new Product();

        public List<Category> Categories { get; set; } = new List<Category>();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Product = await _context.Products.FirstOrDefaultAsync(p =>p.ProductId == id);
            if (Product == null)
            {
                return NotFound();
            }
            Categories = await _context.Categories.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Product.Category");
            if (!ModelState.IsValid)
            {
                // Duyệt qua các lỗi trong ModelState và ghi ra console
                foreach (var modelState in ModelState)
                {
                    var propertyName = modelState.Key;
                    var errors = modelState.Value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Error in {propertyName}: {error.ErrorMessage}");
                    }
                }
                // Nếu dữ liệu không hợp lệ, nạp lại danh sách Category và trả về trang
                Categories = await _context.Categories.ToListAsync();
                return Page();
            }


            // Lấy sản phẩm hiện tại từ database
            var productToUpdate = await _context.Products.FindAsync(Product.ProductId);
            if (productToUpdate == null)
            {
                return NotFound();
            }

            // Cập nhật các trường cần thiết
            productToUpdate.ProductName = Product.ProductName;
            productToUpdate.Price = Product.Price;
            productToUpdate.CategoryId = Product.CategoryId;
            productToUpdate.Stock = Product.Stock;

            try
            {
                _context.Products.Update(productToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Ghi log lỗi (bạn có thể sử dụng ILogger thay cho Console)
                Console.WriteLine("Error updating product: " + ex.Message);
                ModelState.AddModelError("", "Unable to update product. Please try again.");
                // Nạp lại danh sách Category nếu có lỗi
                Categories = await _context.Categories.ToListAsync();
                return Page();
            }

            // Sau khi cập nhật thành công, chuyển hướng về trang danh sách sản phẩm
            return RedirectToPage("Product");
        }
    }
}
