using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topic3_RazorPages_DBF1.Modelss;

namespace Topic3_RazorPages_DBF1.Pages;
public class IndexModel : PageModel
{
 public class HomeModel(Prn222dbFirstContext context) : PageModel
 {
        public List<Product> Products { get; set; } = new();
        public string CustomerName { get; set; } = "Unknown Name";
        public int CartItemCount { get; set; } = 0;
        
        public async Task OnGetAsync()
        {
            Products = await context.Products.ToListAsync();
            var name = HttpContext.Session.GetString("CustomerName");
            if (!string.IsNullOrEmpty(name))
            {
                CustomerName = name;
            }
            CartItemCount = HttpContext.Session.GetInt32("CartCount") ?? 0;
        }

        public async Task<IActionResult> OnPostAddToCartAsync()
        {
            //Guid id, int quantity
            Guid id = Guid.Parse(Request.Form["productId"]);
            int quantity = Convert.ToInt32(Request.Form["quantity"]);
            
            var name = HttpContext.Session.GetString("CustomerName");
            
            // if (string.IsNullOrEmpty(name))
            // {
            //     TempData["Message"] = "You must log in to add products to the cart.";
            //     return RedirectToPage();
            // }
            
            // int id = Convert.ToInt32(Request.Query["id"]);
            // int quantity = Request.Form.ContainsKey("quantity") 
            //     ? Convert.ToInt32(Request.Form["quantity"]) 
            //     : 1; // Default value
            
            
            if (quantity <= 0)
            {
                TempData["ErrorMessage"] = "Quantity must be greater than 0!";
                return Page(); // Trả về trang hiện tại thay vì redirect
            }

            // var product = await context.Products.FindAsync(id);
            var product = await context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            
            if (product == null)
            {
                return NotFound();
            }

            // Lấy giỏ hàng từ Session
            var cartJson = HttpContext.Session.GetString("Cart");
            
            
            
            List<CartItem> cartItems = new();
            
            if (!string.IsNullOrEmpty(cartJson))
            {
                cartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
            }

            var existingItem = cartItems.FirstOrDefault(c => c.ProductId == id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity; 
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Quantity = quantity
                });
            }

            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));

            // int totalItems = cartItems.Sum(item => item.Quantity);
            int totalItems = cartItems.Count;
            
            HttpContext.Session.SetInt32("CartCount", totalItems);
            TempData["Message"] = $"Added {quantity} {product.ProductName}(s) to Cart!";
            return RedirectToPage();
        }
    }
}
