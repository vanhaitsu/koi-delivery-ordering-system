using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Common;
using Newtonsoft.Json;
using KoiDeliveryOrderingSystem.MVCWebApp.Base;
using KoiDeliveryOrderingSystem.MVCWebApp.Models;
using KoiDeliveryOrderingSystem.Service;

namespace KoiDeliveryOrderingSystem.MVCWebApp.Controllers
{
    public class ShipmentOrdersController : Controller
    {
        /*  private readonly FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext _context;

          public ShipmentOrdersController(FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context)
          {
              _context = context;
          }*/

        // GET: ShipmentOrders
       

        public async Task<IActionResult> Index(string originLocation, string destinationLocation, string additionalServices, string shipmentMethod, string orderDateSort, string totalQuantitySort, string orderStatus, int page = 1)
        {
            List<Data.Models.ShipmentOrder> data = new List<Data.Models.ShipmentOrder>();

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
                            data = JsonConvert.DeserializeObject<List<Data.Models.ShipmentOrder>>(result.Data.ToString());
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(originLocation))
            {
                data = data.Where(_ => _.OriginLocation.Contains(originLocation, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(destinationLocation))
            {
                data = data.Where(_ => _.DestinationLocation.Contains(destinationLocation, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(additionalServices))
            {
                data = data.Where(_ => _.AdditionalServices.Contains(additionalServices, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(shipmentMethod))
            {
                data = data.Where(_ => _.ShipmentMethod.Contains(shipmentMethod, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(orderStatus))
            {
                data = data.Where(_ => _.OrderStatus.Equals(orderStatus, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (orderDateSort == "asc")
            {
                data = data.OrderBy(_ => _.OrderDate).ToList();
            }
            else if (orderDateSort == "desc")
            {
                data = data.OrderByDescending(_ => _.OrderDate).ToList();
            }

            if (totalQuantitySort == "asc")
            {
                data = data.OrderBy(_ => _.TotalQuantity).ToList();
            }
            else if (totalQuantitySort == "desc")
            {
                data = data.OrderByDescending(_ => _.TotalQuantity).ToList();
            }

            int pageSize = 3; 
            int totalItems = data.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var pagedData = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["OriginLocation"] = originLocation;
            ViewData["DestinationLocation"] = destinationLocation;
            ViewData["AdditionalServices"] = additionalServices;
            ViewData["ShipmentMethod"] = shipmentMethod;
            ViewData["OrderDateSort"] = orderDateSort;
            ViewData["OrderStatus"] = orderStatus;
            ViewData["TotalQuantitySort"] = totalQuantitySort;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedData);
        }



        // GET: ShipmentOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrders/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Data.Models.ShipmentOrder>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Data.Models.ShipmentOrder());
        }

        // GET: ShipmentOrders/Create
        public async Task<IActionResult> Create()
        {
            var customers = new List<Data.Models.Customer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Users"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            customers = JsonConvert.DeserializeObject<List<Data.Models.Customer>>(result.Data.ToString());
                        }
                    }
                }
            }

            var pricingPolicys = new List<Data.Models.PricingPolicy>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "PricingPolicy"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            pricingPolicys = JsonConvert.DeserializeObject<List<Data.Models.PricingPolicy>>(result.Data.ToString());
                        }
                    }
                }
            }

            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerId");
            ViewData["PricingId"] = new SelectList(pricingPolicys, "PricingId", "PricingId");
            return View();
        }

        // POST: ShipmentOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,PricingId,OrderDate,OriginLocation,DestinationLocation,TotalWeight,TotalQuantity,ShipmentMethod,AdditionalServices,AdditionalFee,OrderStatus")] Data.Models.ShipmentOrder shipmentOrder)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "ShipmentOrders/", shipmentOrder))
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
                var customers = new List<Data.Models.Customer>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Users"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                customers = JsonConvert.DeserializeObject<List<Data.Models.Customer>>(result.Data.ToString());
                            }
                        }
                    }
                }
                var pricingPolicys = new List<Data.Models.PricingPolicy>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "PricingPolicy"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                pricingPolicys = JsonConvert.DeserializeObject<List<Data.Models.PricingPolicy>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerId");
                ViewData["PricingId"] = new SelectList(pricingPolicys, "PricingId", "PricingId", shipmentOrder.PricingId);

                return View(shipmentOrder);
            }
        }

        // GET: ShipmentOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Data.Models.ShipmentOrder shipmentOrder = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrders/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            shipmentOrder = JsonConvert.DeserializeObject<Data.Models.ShipmentOrder>(result.Data.ToString());
                        }
                    }
                }
            }

            if (shipmentOrder == null)
            {
                return NotFound();
            }
            var pricingPolicys = new List<Data.Models.PricingPolicy>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "PricingPolicy"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            pricingPolicys = JsonConvert.DeserializeObject<List<Data.Models.PricingPolicy>>(result.Data.ToString());
                        }
                    }
                }
            }
            var customers = new List<Data.Models.Customer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Users"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            customers = JsonConvert.DeserializeObject<List<Data.Models.Customer>>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerId");
            ViewData["PricingId"] = new SelectList(pricingPolicys, "PricingId", "PricingId", shipmentOrder.PricingId);
            return View(shipmentOrder);
        }

        // POST: ShipmentOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,PricingId,OrderDate,OriginLocation,DestinationLocation,TotalWeight,TotalQuantity,ShipmentMethod,AdditionalServices,AdditionalFee,OrderStatus")] Data.Models.ShipmentOrder shipmentOrder)
        {
            if (id != shipmentOrder.OrderId)
            {
                return NotFound();
            }

            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "ShipmentOrders/" + id, shipmentOrder))
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
                var pricingPolicys = new List<Data.Models.PricingPolicy>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "PricingPolicy"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                pricingPolicys = JsonConvert.DeserializeObject<List<Data.Models.PricingPolicy>>(result.Data.ToString());
                            }
                        }
                    }
                }
                var customers = new List<Data.Models.Customer>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Users"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                customers = JsonConvert.DeserializeObject<List<Data.Models.Customer>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerId");
                ViewData["PricingId"] = new SelectList(pricingPolicys, "PricingId", "PricingId", shipmentOrder.PricingId);
                return View(shipmentOrder);
            }
        }

        // GET: ShipmentOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Data.Models.ShipmentOrder shipmentOrder = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrders/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            shipmentOrder = JsonConvert.DeserializeObject<Data.Models.ShipmentOrder>(result.Data.ToString());
                        }
                    }
                }
            }

            if (shipmentOrder == null)
            {
                return NotFound();
            }

            return View(shipmentOrder);
        }

        // POST: ShipmentOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "ShipmentOrders/" + id))
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
                var pricingPolicys = new List<Data.Models.PricingPolicy>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "PricingPolicy"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                pricingPolicys = JsonConvert.DeserializeObject<List<Data.Models.PricingPolicy>>(result.Data.ToString());
                            }
                        }
                    }
                }
                var customers = new List<Data.Models.Customer>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Users"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                customers = JsonConvert.DeserializeObject<List<Data.Models.Customer>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerId");
                ViewData["PricingId"] = new SelectList(pricingPolicys, "PricingId", "PricingId");
                return View();
            }
        }
    }
}
