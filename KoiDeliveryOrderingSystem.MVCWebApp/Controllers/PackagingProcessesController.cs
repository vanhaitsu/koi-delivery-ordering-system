using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiDeliveryOrderingSystem.MVCWebApp.Models;
using KoiDeliveryOrderingSystem.Common;
using Newtonsoft.Json;
using KoiDeliveryOrderingSystem.MVCWebApp.Base;
using PackagingProcess = KoiDeliveryOrderingSystem.MVCWebApp.Models.PackagingProcess;
using static NuGet.Packaging.PackagingConstants;
using ShipmentOrder = KoiDeliveryOrderingSystem.MVCWebApp.Models.ShipmentOrder;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KoiDeliveryOrderingSystem.MVCWebApp.Controllers
{
    public class PackagingProcessesController : Controller
    {
        /* private readonly FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext _context;

         public PackagingProcessesController(FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context)
         {
             _context = context;
         }*/

        // GET: PackagingProcesses
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "PackagingProcesses"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                var data = JsonConvert.DeserializeObject<List<PackagingProcess>>(result.Data.ToString());
                                return View(data);
                            }
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Lỗi khi gửi yêu cầu: {ex.Message}");
                }
            }
            return View(new List<PackagingProcess>());
        }

        // GET: PackagingProcesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(Const.APIEndPoint + $"PackagingProcesses/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if (result != null && result.Data != null)
                    {
                        var packagingProcess = JsonConvert.DeserializeObject<PackagingProcess>(result.Data.ToString());

                        if (packagingProcess == null)
                        {
                            return NotFound();
                        }

                        return View(packagingProcess);
                    }
                }
            }
            return NotFound();
        }
        // POST: PackagingProcesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(Const.APIEndPoint + $"PackagingProcesses/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        // GET: PackagingProcesses/Create
        public async Task<IActionResult> CreateAsync()
        {
            var orders = new List<ShipmentOrder>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrders"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            Console.WriteLine("API response: " + result.Data);
                            orders = JsonConvert.DeserializeObject<List<ShipmentOrder>>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["ShipmentOrderId"] = new SelectList(orders, "OrderId", "OrderId");
            return View();
        }

        // POST: PackagingProcesses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PackagingProcessId,ShipmentOrderId,PackagingType,PackagingInstructions,PackagingDate,HandlerName,QualityCheckStatus,Remarks,EstimatedPackagingTime,ActualPackagingTime,PackagingCost")] PackagingProcess packagingProcess)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "PackagingProcesses/", packagingProcess))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var orders = new List<ShipmentOrder>();
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrders");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            orders = JsonConvert.DeserializeObject<List<ShipmentOrder>>(result.Data.ToString());
                        }
                    }
                }
                ViewData["ShipmentOrderId"] = new SelectList(orders, "OrderId", "OrderId", packagingProcess.ShipmentOrderId);
            }


            return View(packagingProcess);
        }
        // GET: PackagingProcesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "PackagingProcesses/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var packagingProcess = JsonConvert.DeserializeObject<PackagingProcess>(result.Data.ToString());
                            return View(packagingProcess);
                        }
                    }
                }
            }
            return View(new PackagingProcess());
        }
        //GET: PackagingProcesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PackagingProcess packagingProcess = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "PackagingProcesses/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            packagingProcess = JsonConvert.DeserializeObject<PackagingProcess>(result.Data.ToString());
                        }
                    }
                }
            }

            if (packagingProcess == null)
            {
                return NotFound();
            }


            // Get dropdown data ------------------------------------------------------------
            var orders = new List<ShipmentOrder>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrders"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            orders = JsonConvert.DeserializeObject<List<ShipmentOrder>>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["ShipmentOrderId"] = new SelectList(orders, "OrderId", "OrderId");
            return View(packagingProcess);
        }
        // POST: ShipmentTrackings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PackagingProcessId,ShipmentOrderId,PackagingType,PackagingInstructions,PackagingDate,HandlerName,QualityCheckStatus,Remarks,EstimatedPackagingTime,ActualPackagingTime,PackagingCost")] PackagingProcess packagingProcess)
        {
            if (id != packagingProcess.PackagingProcessId)
            {
                return NotFound();
            }

            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "PackagingProcesses/" + id, packagingProcess))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var orders = new List<ShipmentOrder>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrders"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                orders = JsonConvert.DeserializeObject<List<ShipmentOrder>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["ShipmentOrderId"] = new SelectList(orders, "OrderId", "OrderId");
                return View(packagingProcess);
            }
        }
    }
}



