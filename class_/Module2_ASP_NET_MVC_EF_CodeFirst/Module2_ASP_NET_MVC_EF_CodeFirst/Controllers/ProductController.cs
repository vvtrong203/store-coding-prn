using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Module2_ASP_NET_MVC_EF_CodeFirst.Models;
using System.Text.Json;

namespace Module2_ASP_NET_MVC_EF_CodeFirst.Controllers
{
    public class ProductController : Controller
    {
        private readonly PRN222DBContext _context;
        public ProductController(PRN222DBContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Product>>> Index()
        {
            List<Product> products = await _context.products.Include(p => p.Category).ToListAsync();
            ViewBag.Cart = GetCartFromSession(); 
            return View("List", products);
        }

        public async Task<ActionResult<Product>> Detail(Guid id)
        {
            Product product = await _context.products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId.Equals(id));
            return View("Detail", product);
        }
        [HttpPost]
        public IActionResult AddToCart(Guid productId, int quantity = 1)
        {
            if (quantity < 1) return BadRequest("Số lượng phải lớn hơn 0");

            var product = _context.products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null) return NotFound();

            List<CartItem> cart = GetCartFromSession(); 
            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity; 
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    UnitPrice = product.Price,
                    Quantity = quantity
                });
            }

            SaveCartToSession(cart); 
            return RedirectToAction("Index"); 
        }



        public IActionResult Cart()
        {
            return View(GetCartFromSession());
        }

        public IActionResult RemoveFromCart(Guid productId)
        {
            List<CartItem> cart = GetCartFromSession();
            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                SaveCartToSession(cart);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(string ShipAddress)
        {
            List<CartItem> cart = GetCartFromSession();
            if (cart.Count == 0)
            {
                TempData["Error"] = "Giỏ hàng trống!";
                return RedirectToAction("Cart");
            }
        
            var order = new Order
            {
                OrderID = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                ShipAddress = ShipAddress,
                Orderdetail = new List<OrderDetail>()
            };
        
            foreach (var item in cart)
            {
                order.Orderdetail.Add(new OrderDetail
                {
                    OrderDetailId = Guid.NewGuid(),
                    OrderId = order.OrderID,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = 0
                });
            }
        
            _context.orders.Add(order);
            await _context.SaveChangesAsync();
        
            HttpContext.Session.Remove("Cart");
        
            TempData["SuccessMessage"] = "Thank you for your order! Your order has been placed successfully.";
            return RedirectToAction("Index");
        }

        private void SaveCartToSession(List<CartItem> cart)
        {
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
        }

        private List<CartItem> GetCartFromSession()
        {
            string cartJson = HttpContext.Session.GetString("Cart");
            return string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }
    }
}