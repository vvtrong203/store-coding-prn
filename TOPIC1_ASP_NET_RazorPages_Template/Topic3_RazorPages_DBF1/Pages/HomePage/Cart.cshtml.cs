using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Topic3_RazorPages_DBF1.Modelss;

namespace Topic3_RazorPages_DBF1.Pages.HomePage
{
    public class CartModel : PageModel
    {
        private readonly Prn222dbFirstContext _context;

        public CartModel(Prn222dbFirstContext context)
        {
            _context = context;
        }
        
        public int CartItemCount { get; set; } = 0;

        [BindProperty]
        [Required(ErrorMessage = "Ship address is required")]
        public string ShipAddress { get; set; } = string.Empty; // Giá trị mặc định tránh null

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        
        public void OnGet()
        {
            CartItemCount = HttpContext.Session.GetInt32("CartCount") ?? 0;
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
            }
        }
        
        public async Task<IActionResult> OnPostRemoveFromCartAsync(Guid productId)
        {
            if (HttpContext.Session.GetString("Cart") != null)
            {
                var cart = JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString("Cart"));
                var existItem = cart.FirstOrDefault(x => x.ProductId == productId);
                if (existItem != null)
                {
                    cart.Remove(existItem);
                    HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
                }
            }
            return RedirectToPage("/HomePage/Cart");
        }
        
        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            if (!ModelState.IsValid) 
            {
                return Page();
            }

            if (HttpContext.Session.GetString("Cart") != null)
            {
                var cart = JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString("Cart"));
                if (cart == null || !cart.Any())
                {
                    ModelState.AddModelError(string.Empty, "Your cart is empty.");
                    return Page();
                }

                Guid customerId;
                if (HttpContext.Session.GetString("CustomerId").IsNullOrEmpty())
                {
                    return RedirectToPage("/HomePage/Login");
                }
                else
                {
                    customerId = Guid.Parse(HttpContext.Session.GetString("CustomerId"));
                }
                
                var order = new Order 
                { 
                    OrderId = Guid.NewGuid(), 
                    OrderDate = DateTime.Now, 
                    CustomerId = customerId, 
                    ShipAddress = ShipAddress 
                };
                await _context.Orders.AddAsync(order);
                
                foreach (var item in cart)
                {
                    await _context.OrderDetails.AddAsync(new OrderDetail
                    {
                        OrderDetailId = Guid.NewGuid(),
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        UnitPrice = item.Price,
                        Quantity = item.Quantity,
                        Discount = 0
                    });
                }

                await _context.SaveChangesAsync();
                HttpContext.Session.Remove("Cart");
                HttpContext.Session.Remove("CartCount");
                TempData["message"] = "Checkout success";
            }
            return RedirectToPage("/HomePage/Cart");
        }
    }
}
