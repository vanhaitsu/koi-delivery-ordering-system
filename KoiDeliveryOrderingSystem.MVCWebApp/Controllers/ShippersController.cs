﻿using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.MVCWebApp.Controllers
{
    public class ShippersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}