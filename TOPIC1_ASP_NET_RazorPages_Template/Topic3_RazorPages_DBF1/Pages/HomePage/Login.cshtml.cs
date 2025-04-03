using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Topic3_RazorPages_DBF1.Modelss;

namespace Topic3_RazorPages_DBF1.Pages.HomePage
{
    public class LoginModel : PageModel
    {
        private readonly Prn222dbFirstContext _context;

        public LoginModel(Prn222dbFirstContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public int CartItemCount { get; set; } = 0;

        public void OnGet()
        {
            CartItemCount = HttpContext.Session.GetInt32("CartCount") ?? 0;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var hashedPassword = HashPassword(Password);
            var customer = _context.Customers
                .FirstOrDefault(c => c.Email == Email && c.Password == hashedPassword);

            if (customer == null)
            {
                ErrorMessage = "Invalid email or password!";
                return Page();
            }

            HttpContext.Session.SetString("CustomerId", customer.CustomerId.ToString());
            HttpContext.Session.SetString("CustomerName", customer.CustomerName ?? "");

            return RedirectToPage("/Index");
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
