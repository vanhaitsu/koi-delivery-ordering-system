using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.MVCWebApp.Controllers
{
    public class PackagingProcessesAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
