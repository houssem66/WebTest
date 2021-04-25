using Microsoft.AspNetCore.Mvc;

namespace TourMe.Web.Controllers
{
    public class LogementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
