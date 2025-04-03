using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Topic3_RazorPages_DBF1.Modelss;

namespace Topic3_RazorPages_DBF1.Pages.HomePage
{
    public class RegisterModel : PageModel
    {
        private readonly Prn222dbFirstContext _context;

        public RegisterModel(Prn222dbFirstContext context)
        {
            _context = context;
        }
        public int CartItemCount { get; set; } = 0;

        [BindProperty]
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string CustomerName { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;
        public string SuccessMessage { get; set; } = string.Empty;

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

            var existingCustomer = _context.Customers.FirstOrDefault(c => c.Email == Email);
            if (existingCustomer != null)
            {
                ErrorMessage = "Email already registered!";
                return Page();
            }

            var hashedPassword = HashPassword(Password);

            var newCustomer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                CustomerName = CustomerName,
                Email = Email,
                Password = hashedPassword
            };

            _context.Customers.Add(newCustomer);
            _context.SaveChanges();

            SuccessMessage = "Registration successful! You can now login.";
            return Page();
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
