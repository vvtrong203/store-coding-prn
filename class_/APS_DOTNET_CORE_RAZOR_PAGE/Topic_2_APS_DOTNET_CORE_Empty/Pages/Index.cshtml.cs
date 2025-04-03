using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Topic_2_APS_DOTNET_CORE_Empty.Model;

namespace Topic_2_APS_DOTNET_CORE_Empty.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; } // Không cần [BindProperty] vì không bind từ form

        public List<Student> Students { get; set; } = new List<Student>(); // Property thay vì field
        public Student Student { get; set; } = new Student(); // Property thay vì field

        public IActionResult OnGet()
        {
            Message = "Index model data";
            Student = new Student(1, "Nguyen Van A", new DateTime(2003, 10, 3));
            Students = new List<Student>
            {
                new Student(1, "Nguyen Van A", new DateTime(2003, 10, 3)),
                new Student(2, "Nguyen Van B", new DateTime(2003, 10, 3)),
                new Student(3, "Nguyen Van C", new DateTime(2003, 10, 3))
            };
            return Page();
        }
    }
}