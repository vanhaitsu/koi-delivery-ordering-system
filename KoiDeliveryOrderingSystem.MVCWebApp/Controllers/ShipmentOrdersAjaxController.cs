using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.MVCWebApp.Controllers
{
    public class ShipmentOrdersAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
