using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topic3_RazorPages_DBF1.Modelss;

namespace Topic3_RazorPages_DBF1.Pages.Admin
{
    public class DeleteProductModel : PageModel
    {
        private readonly Prn222dbFirstContext _context;
        public int CartItemCount { get; set; } = 0;
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<Product> ProductList { set; get; } = new List<Product>();
        public DeleteProductModel(Prn222dbFirstContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = new Product();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            CartItemCount = HttpContext.Session.GetInt32("CartCount") ?? 0;
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
            }
            
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            Product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var productToDelete = await _context.Products.FindAsync(id);

            if (productToDelete == null)
            {
                return NotFound();
            }

            try
            {
                _context.Products.Remove(productToDelete);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Error deleting product: " + ex.Message);
                ModelState.AddModelError("", "Unable to delete product. Please try again.");
                return Page();
            }

            return RedirectToPage("Product");
        }
    }
}