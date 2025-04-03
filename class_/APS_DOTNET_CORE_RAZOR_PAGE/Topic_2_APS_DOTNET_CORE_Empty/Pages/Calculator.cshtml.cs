// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
//
// namespace Topic_2_APS_DOTNET_CORE_Empty.Pages
// {
//     public class CalculatorModel : PageModel
//     {
//         [BindProperty]
//         public int? Sum { get; set; }
//
//         [BindProperty]
//         public string? N1 { get; set; }
//
//         [BindProperty]
//         public string? N2 { get; set; }
//
//         public IActionResult OnGet()
//         {
//             return Page();
//         }
//
//         public IActionResult OnPost()
//         {
//             if (string.IsNullOrEmpty(N1) || string.IsNullOrEmpty(N2))
//             {
//                 ModelState.AddModelError("", "Please enter both numbers.");
//                 return Page();
//             }
//
//             if (int.TryParse(N1, out int n1) && int.TryParse(N2, out int n2))
//             {
//                 Sum = n1 + n2;
//             }
//             else
//             {
//                 ModelState.AddModelError("", "Invalid input! Please enter valid numbers.");
//             }
//             
//             return Page();
//         }
//     }
// }
//

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Topic_2_APS_DOTNET_CORE_Empty.Pages
{
    // [IgnoreAntiforgeryToken]
    public class CalculatorModel : PageModel
    {
        [BindProperty]
        public int? Sum { get; set; }

        [BindProperty]
        public string N1 { get; set; } = "";

        [BindProperty]
        public string N2 { get; set; } = "";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (int.TryParse(N1, out int n1) && int.TryParse(N2, out int n2))
            {
                Sum = n1 + n2;
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng nhập số hợp lệ.");
            }
            return Page();
        }
    }
}