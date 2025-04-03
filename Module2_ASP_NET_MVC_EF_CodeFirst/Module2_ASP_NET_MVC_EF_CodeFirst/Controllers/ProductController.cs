using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Module2_ASP_NET_MVC_EF_CodeFirst.Models;

namespace Module2_ASP_NET_MVC_EF_CodeFirst.Controllers
{
    public class ProductController : Controller
    {
        private readonly PRN222DBContext _context;
        public ProductController(PRN222DBContext context)
        {
            _context = context;
        }
        // Get all Products
        
        public async Task<ActionResult<IEnumerable<Product>>> Index()
        {
            List<Product> products = await _context.products.Include(p=>p.Category).ToListAsync();
            return View("List", products);
        }

        public async Task<ActionResult<Product>> Detail(Guid id)
        {
            Product product = await _context.products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId.Equals(id));
            return View(product);
        }
    }
}
