using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TOPIC2_ASP_NET_RazorPages_Empty.Models;

namespace TOPIC2_ASP_NET_RazorPages_Empty.Pages
{
    public class Index1Model : PageModel
    {
        [BindProperty]
        public string Message { get; set; }
        public List<Student> studentsList { get; set; } = new List<Student>();
        public void OnGet()
        {
            studentsList.Add(new Student { Id = 1, Name = "Hoang", Major = ".Net" });
            studentsList.Add(new Student { Id = 2, Name = "Cuong", Major = "Nodejs" });
            studentsList.Add(new Student { Id = 3, Name = "Phuong", Major = "KS" });
            Message = " Index Model data";
        }
    }
}
