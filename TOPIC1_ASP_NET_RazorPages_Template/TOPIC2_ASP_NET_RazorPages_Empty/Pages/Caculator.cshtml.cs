using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TOPIC2_ASP_NET_RazorPages_Empty.Pages
{
    public class CaculatorModel : PageModel
    {
        [BindProperty]
        public int sum { get; set; }
        [BindProperty]
        public int n1 { get; set; }
        [BindProperty]
        public int n2 { get; set; }
        //public async Task<IActionResult> OnGetAsync()
        //{
        //    return Page();
        //}

        public void OnPost() {
            sum = n1 + n2;
        }
    }
}
