using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Common;
using KoiDeliveryOrderingSystem.MVCWebApp.Models;
using KoiDeliveryOrderingSystem.MVCWebApp.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KoiDeliveryOrderingSystem.MVCWebApp.Controllers
{
    public class ShipmentTrackingsController : Controller
    {
        // GET: ShipmentTrackings
        public async Task<IActionResult> Index()
        {
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
                            var data = JsonConvert.DeserializeObject<List<ShipmentTracking>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<ShipmentTracking>());
        }

        // GET: ShipmentTrackings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentTrackings/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<ShipmentTracking>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new ShipmentTracking());
        }

        // GET: ShipmentTrackings/Create
        public async Task<IActionResult> CreateAsync()
        {
            //ViewData["OrderId"] = new SelectList();

            var shippers = new List<Shipper>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Shippers"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            shippers = JsonConvert.DeserializeObject<List<Shipper>>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["ShipperId"] = new SelectList(shippers, "ShipperId", "FullName");
            return View();
        }

        // POST: ShipmentTrackings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackingId,ShipperId,OrderId,UpdateTime,CurrentLocation,ShipmentStatus,TemperatureDuringTransit,HumidityDuringTransit,HandlerName,Remarks,EstimatedArrival")] ShipmentTracking shipmentTracking)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "ShipmentTrackings/", shipmentTracking))
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
                var shippers = new List<Shipper>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Shippers"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                shippers = JsonConvert.DeserializeObject<List<Shipper>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["ShipperId"] = new SelectList(shippers, "ShipperId", "FullName", shipmentTracking.ShipperId);
                return View(shipmentTracking);
            }
        }

        // GET: ShipmentTrackings/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var shipmentTracking = await _context.ShipmentTrackings.FindAsync(id);
        //    if (shipmentTracking == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["OrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId", shipmentTracking.OrderId);
        //    ViewData["ShipperId"] = new SelectList(_context.Shippers, "ShipperId", "ShipperId", shipmentTracking.ShipperId);
        //    return View(shipmentTracking);
        //}

        //    // POST: ShipmentTrackings/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("TrackingId,ShipperId,OrderId,UpdateTime,CurrentLocation,ShipmentStatus,TemperatureDuringTransit,HumidityDuringTransit,HandlerName,Remarks,EstimatedArrival")] ShipmentTracking shipmentTracking)
        //    {
        //        if (id != shipmentTracking.TrackingId)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(shipmentTracking);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!ShipmentTrackingExists(shipmentTracking.TrackingId))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        ViewData["OrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId", shipmentTracking.OrderId);
        //        ViewData["ShipperId"] = new SelectList(_context.Shippers, "ShipperId", "ShipperId", shipmentTracking.ShipperId);
        //        return View(shipmentTracking);
        //    }

        //    // GET: ShipmentTrackings/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var shipmentTracking = await _context.ShipmentTrackings
        //            .Include(s => s.Order)
        //            .Include(s => s.Shipper)
        //            .FirstOrDefaultAsync(m => m.TrackingId == id);
        //        if (shipmentTracking == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(shipmentTracking);
        //    }

        //    // POST: ShipmentTrackings/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var shipmentTracking = await _context.ShipmentTrackings.FindAsync(id);
        //        if (shipmentTracking != null)
        //        {
        //            _context.ShipmentTrackings.Remove(shipmentTracking);
        //        }

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool ShipmentTrackingExists(int id)
        //    {
        //        return _context.ShipmentTrackings.Any(e => e.TrackingId == id);
        //    }
    }
}
