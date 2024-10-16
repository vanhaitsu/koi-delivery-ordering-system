﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiDeliveryOrderingSystem.Common;
using Newtonsoft.Json;
using KoiDeliveryOrderingSystem.MVCWebApp.Models;
using KoiDeliveryOrderingSystem.MVCWebApp.Base;

namespace KoiDeliveryOrderingSystem.MVCWebApp.Controllers
{
    public class HealthChecksController : Controller
    {
        //private readonly HealthChecksController _healthChecksApiController;
        //public HealthChecksController(HealthChecksController healthChecksApiController)
        //{
        //    _healthChecksApiController = healthChecksApiController;
        //}

        // GET: HealthChecks
        public async Task<IActionResult> Index( string? search, int pageNumber = 1, string? order = null, bool? orderByDescending = false, string? packagingType = null)
        {
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"HealthChecks?search={search}&pageNumber={pageNumber}&order={order}&orderByDescending={orderByDescending}&packagingType={packagingType}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<QueryResultModel<HealthCheck>>(result.Data.ToString());

                            if (data != null && data.Data != null)
                            {
                                ViewBag.TotalCount = data.TotalCount;
                                ViewBag.CurrentPage = pageNumber;
                                return View(data.Data);
                            }
                        }
                    }
                }
            }
            return View(new List<HealthCheck>());
        }


        // GET: HealthChecks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "HealthChecks/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<HealthCheck>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new HealthCheck());
        }

        // GET: HealthChecks/Create
        public async Task<IActionResult> Create()
        {
            var shipmentTrackings = new List<ShipmentTracking>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentTrackings"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var temp = JsonConvert.DeserializeObject<FilterResult<ShipmentTracking>>(result.Data.ToString());
                            shipmentTrackings = temp.Data;
                        }
                    }
                }
            }
            ViewData["ShipmentTrackingId"] = new SelectList(shipmentTrackings, "TrackingId", "TrackingId");

            var shipmentOrderDetails = new List<ShipmentOrderDetail>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrderDetails"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            shipmentOrderDetails = JsonConvert.DeserializeObject<List<ShipmentOrderDetail>>(result.Data.ToString());
                        }
                    }
                }
            }
            
            ViewData["ShipmentOrderDetailId"] = new SelectList(shipmentOrderDetails, "ShipmentOrderDetailId", "Description");
            return View();
        }

        // POST: HealthChecks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HealthCheckId,ShipmentOrderDetailId,ShipmentTrackingId,CheckDate,Weight,Condition,Temperature,DoctorName,Recommendations,PackagingType,NextCheckupDate")] HealthCheck healthCheck)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "HealthChecks/", healthCheck))
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
                var shipmentOrderDetails = new List<ShipmentOrderDetail>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrderDetails"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                shipmentOrderDetails = JsonConvert.DeserializeObject<List<ShipmentOrderDetail>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["ShipmentOrderDetailId"] = new SelectList(shipmentOrderDetails, "ShipmentOrderDetailId", "Description", healthCheck.ShipmentOrderDetailId);
                return View(healthCheck);
            }
        }

        //    // GET: HealthChecks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HealthCheck healthCheck = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "HealthChecks/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            healthCheck = JsonConvert.DeserializeObject<HealthCheck>(result.Data.ToString());
                        }
                    }
                }
            }

            if (healthCheck == null)
            {
                return NotFound();
            }


            // Get dropdown data ------------------------------------------------------------
            //ViewData["OrderId"] = new SelectList();

            var shipmentOrderDetails = new List<ShipmentOrderDetail>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrderDetails"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            shipmentOrderDetails = JsonConvert.DeserializeObject<List<ShipmentOrderDetail>>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["ShipmentOrderDetailId"] = new SelectList(shipmentOrderDetails, "ShipmentOrderDetailId", "Description", healthCheck.ShipmentOrderDetailId);
            return View(healthCheck);
        }

        //    // POST: HealthChecks/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HealthCheckId,ShipmentOrderDetailId,ShipmentTrackingId,CheckDate,Weight,Condition,Temperature,DoctorName,Recommendations,PackagingType,NextCheckupDate")] HealthCheck healthCheck)
        {
            if (id != healthCheck.HealthCheckId)
            {
                return NotFound();
            }

            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "HealthChecks/" + id, healthCheck))
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
                var shipmentOrderDetails = new List<ShipmentOrderDetail>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrderDetails"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                shipmentOrderDetails = JsonConvert.DeserializeObject<List<ShipmentOrderDetail>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["ShipmentOrderDetailId"] = new SelectList(shipmentOrderDetails, "ShipmentOrderDetailId", "Description", healthCheck.ShipmentOrderDetailId);
                return View(healthCheck);
            }
        }

        //    // GET: HealthChecks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HealthCheck healthCheck = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "HealthChecks/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            healthCheck = JsonConvert.DeserializeObject<HealthCheck>(result.Data.ToString());
                        }
                    }
                }
            }

            if (healthCheck == null)
            {
                return NotFound();
            }

            return View(healthCheck);
        }

        //    // POST: HealthChecks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "HealthChecks/" + id))
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
                var shipmentOrderDetails = new List<ShipmentOrderDetail>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrderDetails"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                shipmentOrderDetails = JsonConvert.DeserializeObject<List<ShipmentOrderDetail>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["ShipmentOrderDetailId"] = new SelectList(shipmentOrderDetails, "ShipmentOrderDetailId", "Description", id);
                return View();
            }
        }
    }
}
