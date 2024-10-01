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

namespace KoiDeliveryOrderingSystem.MVCWebApp.Controllers
{
    public class ShipmentOrderDetailsController : Controller
    {
        private readonly FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext _context;

        public ShipmentOrderDetailsController(FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context)
        {
            _context = context;
        }

        // GET: ShipmentOrderDetails
        public async Task<IActionResult> Index()
        {
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
                            var data = JsonConvert.DeserializeObject<List<ShipmentTracking>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<ShipmentTracking>());
        }

        // GET: ShipmentOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentOrderDetail = await _context.ShipmentOrderDetails
                .Include(s => s.AnimalType)
                .Include(s => s.ShipmentOrder)
                .FirstOrDefaultAsync(m => m.ShipmentOrderDetailId == id);
            if (shipmentOrderDetail == null)
            {
                return NotFound();
            }

            return View(shipmentOrderDetail);
        }

        // GET: ShipmentOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalTypes, "AnimalTypeId", "AnimalTypeId");
            ViewData["ShipmentOrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId");
            return View();
        }

        // POST: ShipmentOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipmentOrderDetailId,ShipmentOrderId,AnimalTypeId,AdditionalServices,Fee,Status,Weight,Length,DateOfEntry,Gender,Color,HealthStatus,Age,Achievement,Origin,ImageUrl,Description")] ShipmentOrderDetail shipmentOrderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipmentOrderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalTypes, "AnimalTypeId", "AnimalTypeId", shipmentOrderDetail.AnimalTypeId);
            ViewData["ShipmentOrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId", shipmentOrderDetail.ShipmentOrderId);
            return View(shipmentOrderDetail);
        }

        // GET: ShipmentOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentOrderDetail = await _context.ShipmentOrderDetails.FindAsync(id);
            if (shipmentOrderDetail == null)
            {
                return NotFound();
            }
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalTypes, "AnimalTypeId", "AnimalTypeId", shipmentOrderDetail.AnimalTypeId);
            ViewData["ShipmentOrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId", shipmentOrderDetail.ShipmentOrderId);
            return View(shipmentOrderDetail);
        }

        // POST: ShipmentOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShipmentOrderDetailId,ShipmentOrderId,AnimalTypeId,AdditionalServices,Fee,Status,Weight,Length,DateOfEntry,Gender,Color,HealthStatus,Age,Achievement,Origin,ImageUrl,Description")] ShipmentOrderDetail shipmentOrderDetail)
        {
            if (id != shipmentOrderDetail.ShipmentOrderDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipmentOrderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentOrderDetailExists(shipmentOrderDetail.ShipmentOrderDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalTypes, "AnimalTypeId", "AnimalTypeId", shipmentOrderDetail.AnimalTypeId);
            ViewData["ShipmentOrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId", shipmentOrderDetail.ShipmentOrderId);
            return View(shipmentOrderDetail);
        }

        // GET: ShipmentOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentOrderDetail = await _context.ShipmentOrderDetails
                .Include(s => s.AnimalType)
                .Include(s => s.ShipmentOrder)
                .FirstOrDefaultAsync(m => m.ShipmentOrderDetailId == id);
            if (shipmentOrderDetail == null)
            {
                return NotFound();
            }

            return View(shipmentOrderDetail);
        }

        // POST: ShipmentOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipmentOrderDetail = await _context.ShipmentOrderDetails.FindAsync(id);
            if (shipmentOrderDetail != null)
            {
                _context.ShipmentOrderDetails.Remove(shipmentOrderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentOrderDetailExists(int id)
        {
            return _context.ShipmentOrderDetails.Any(e => e.ShipmentOrderDetailId == id);
        }
    }
}
