using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topic3_RazorPages_DBF1.Modelss;

namespace Topic3_RazorPages_DBF1.Pages.HomePage
{
    public class OrderHistoryModel : PageModel
    {
        private readonly Prn222dbFirstContext _context;
        
        public OrderHistoryModel(Prn222dbFirstContext context)
        {
            _context = context;
        }
        [BindProperty]
        public List<OrderHistoryViewModel> OrderHistory { get; set; } = new List<OrderHistoryViewModel>();
        public int CartItemCount { get; set; } = 0;

        public async Task<IActionResult> OnGet()
        {
            
            CartItemCount = HttpContext.Session.GetInt32("CartCount") ?? 0;

            if (HttpContext.Session.GetString("CustomerId") == null)
            {
                return RedirectToPage("/HomePage/Login");
            }
            
            Guid customerId = Guid.Parse(HttpContext.Session.GetString("CustomerId"));

            OrderHistory = await _context.Orders
                .Where(x => x.CustomerId == customerId)
                .SelectMany(o => o.OrderDetails
                    .Select(od => new OrderHistoryViewModel
                    {
                        OrderDate = o.OrderDate,
                        ProductName = od.Product.ProductName,
                        Quantity = od.Quantity
                    }))
                .OrderByDescending(o => o.OrderDate).ToListAsync();
            return Page();
        }
    }
}
