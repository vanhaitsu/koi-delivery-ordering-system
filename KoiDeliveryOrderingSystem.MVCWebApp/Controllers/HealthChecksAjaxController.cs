using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.MVCWebApp.Controllers
{
    public class HealthChecksAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
