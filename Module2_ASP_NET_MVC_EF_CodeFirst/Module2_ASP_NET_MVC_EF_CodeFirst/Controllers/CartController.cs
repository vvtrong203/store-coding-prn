using Microsoft.AspNetCore.Mvc;
using Module2_ASP_NET_MVC_EF_CodeFirst.Models;
using System.Text.Json;

namespace Module2_ASP_NET_MVC_EF_CodeFirst.Controllers
{
    public class CartController : Controller
    {
        private readonly PRN222DBContext _context;
        public CartController(PRN222DBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(Guid productId, int quantity)
        {
            List<CartItem> cartItems = new List<CartItem>();
            var product = await _context.products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                if (HttpContext.Session.GetString("Cart") != null)
                {
                    // Read session -> convert to List<CartItem>
                    var cart = JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString("Cart"));
                    // Check exist CartItem
                    var existItem = cart.FirstOrDefault(x => x.ProductId == productId);
                    if (existItem != null)
                    {
                        existItem.Quantity += quantity;
                    }
                    else
                    {
                        cart.Add(new CartItem
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            Quantity = quantity,
                            Price = product.Price
                        });
                    }
                    HttpContext.Session.SetString("Cart", JsonSerializer.Serialize<List<CartItem>>(cart));
                }
                else
                {
                    cartItems.Add(new CartItem
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Quantity = quantity,
                        Price = product.Price
                    });
                    HttpContext.Session.SetString("Cart", JsonSerializer.Serialize<List<CartItem>>(cartItems));
                }
                //ViewData["Cart"] = HttpContext.Session.GetString("Cart");
            }
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid productId)
        {
            if (HttpContext.Session.GetString("Cart") != null)
            {
                // Read session -> convert to List<CartItem>
                var cart = JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString("Cart"));
                // Check exist CartItem
                var existItem = cart.FirstOrDefault(x => x.ProductId == productId);
                if (existItem != null)
                {
                    cart.Remove(existItem);
                    HttpContext.Session.SetString("Cart", JsonSerializer.Serialize<List<CartItem>>(cart));
                }
            }
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(string shipAddress)
        {
            if (HttpContext.Session.GetString("Cart") != null)
            {
                var cart = JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString("Cart"));
                var order = new Order { OrderId = Guid.NewGuid(), OrderDate = DateTime.Now, ShipAddress = shipAddress };
                await _context.Orders.AddAsync(order);
                foreach(var item in cart)
                {
                    await _context.OrderDetails.AddAsync(new OrderDetail
                    {
                        OrderDetailId = Guid.NewGuid(),
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        UnitPrice = item.Price,
                        Quantity = item.Quantity,
                        Discount=0
                    });
                }
                await _context.SaveChangesAsync();
                HttpContext.Session.Remove("Cart");
                TempData["message"] = "success";
            }
            return RedirectToAction("Index", "Product");
        }
    }
}
