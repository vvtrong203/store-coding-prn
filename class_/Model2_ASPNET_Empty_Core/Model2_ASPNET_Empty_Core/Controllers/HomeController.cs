using Microsoft.AspNetCore.Mvc;

namespace Model2_ASPNET_Empty_Core.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}

