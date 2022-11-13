using Microsoft.AspNetCore.Mvc;

namespace MVCWorkshop.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
