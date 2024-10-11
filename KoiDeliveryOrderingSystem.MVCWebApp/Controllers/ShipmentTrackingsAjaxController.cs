using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.MVCWebApp.Controllers
{
    public class ShipmentTrackingsAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
