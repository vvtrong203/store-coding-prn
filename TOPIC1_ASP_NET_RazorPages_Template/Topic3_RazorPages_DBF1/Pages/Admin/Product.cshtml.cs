using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topic3_RazorPages_DBF1.Modelss;

namespace Topic3_RazorPages_DBF1.Pages.Admin
{
    public class ProductModel(Prn222dbFirstContext context) : PageModel
    {
        
        public int CartItemCount { get; set; } = 0;
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<Product> ProductList { set; get; } = new List<Product>();

        public async Task OnGet()
        {
            CartItemCount = HttpContext.Session.GetInt32("CartCount") ?? 0;
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
            }
            
            ProductList = await context.Products.Include(
                p => p.Category
            ).ToListAsync();
        }
    }
}
