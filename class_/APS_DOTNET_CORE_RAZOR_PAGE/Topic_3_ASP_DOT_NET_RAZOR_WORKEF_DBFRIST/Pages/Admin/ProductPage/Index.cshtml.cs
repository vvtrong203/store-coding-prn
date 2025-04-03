using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Topic_3_ASP_DOT_NET_RAZOR_WORKEF_DBFRIST.Models;

namespace Topic_3_ASP_DOT_NET_RAZOR_WORKEF_DBFRIST.Pages.Admin.ProductPage
{
    public class IndexModel : PageModel
    {
        public readonly Prn22dbfContext _context;
        public IndexModel(Prn22dbfContext context)
        {
            _context = context;
        }
        public IList<Product> productList { get; set; }
        public async Task OnGet()
        {
            productList = await _context.Products.Include(p 
                => p.Category
            ).ToListAsync();
        }
    }
}

