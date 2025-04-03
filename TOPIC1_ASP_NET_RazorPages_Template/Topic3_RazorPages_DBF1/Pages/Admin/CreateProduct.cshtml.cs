using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Topic3_RazorPages_DBF1.Hubs;
using Topic3_RazorPages_DBF1.Modelss;

namespace Topic3_RazorPages_DBF1.Pages.Admin
{
    public class CreateProductModel : PageModel
    {
        private readonly Prn222dbFirstContext _context;
        private readonly IHubContext<SignalRServer> _hubContext;
        public int CartItemCount { get; set; } = 0;
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<Product> ProductList { set; get; } = new List<Product>();
        
        public CreateProductModel(Prn222dbFirstContext context, IHubContext<SignalRServer> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Product Product { get; set; } = new Product();
        public List<Category> Categories { get; set; } = new List<Category>();

        public async Task<IActionResult> OnGetAsync()
        {
            CartItemCount = HttpContext.Session.GetInt32("CartCount") ?? 0;
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
            }
            
            // Lấy danh sách danh mục để hiển thị dropdown
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

            // Khởi tạo ProductId mới cho sản phẩm
            Product.ProductId = Guid.NewGuid();

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();




            // await _hubContext.Clients.All.SendAsync("ProductCreated", Product);
            await _hubContext.Clients.All.SendAsync("ReloadData");







            // try
            // {
            //     await _context.SaveChangesAsync();
            //     await _hubContext.Clients.All.SendAsync("ProductCreated", new
            //     {
            //         ProductId = Product.ProductId,
            //         ProductName = Product.ProductName,
            //         // Add other properties as needed
            //     });
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"SignalR Error: {ex.Message}");
            //     // Still return success if DB save worked
            // }
            return RedirectToPage();
        }
    }
}
