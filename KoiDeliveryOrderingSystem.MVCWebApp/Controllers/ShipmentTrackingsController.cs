using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrderingSystem.Data.Models;

namespace KoiDeliveryOrderingSystem.MVCWebApp.Controllers
{
    public class ShipmentTrackingsController : Controller
    {
        private readonly FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext _context;

        public ShipmentTrackingsController(FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context)
        {
            _context = context;
        }

        // GET: ShipmentTrackings
        public async Task<IActionResult> Index()
        {
            var fA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext = _context.ShipmentTrackings.Include(s => s.Order).Include(s => s.Shipper);
            return View(await fA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext.ToListAsync());
        }

        // GET: ShipmentTrackings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentTracking = await _context.ShipmentTrackings
                .Include(s => s.Order)
                .Include(s => s.Shipper)
                .FirstOrDefaultAsync(m => m.TrackingId == id);
            if (shipmentTracking == null)
            {
                return NotFound();
            }

            return View(shipmentTracking);
        }

        // GET: ShipmentTrackings/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId");
            ViewData["ShipperId"] = new SelectList(_context.Shippers, "ShipperId", "ShipperId");
            return View();
        }

        // POST: ShipmentTrackings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackingId,ShipperId,OrderId,UpdateTime,CurrentLocation,ShipmentStatus,TemperatureDuringTransit,HumidityDuringTransit,HandlerName,Remarks,EstimatedArrival")] ShipmentTracking shipmentTracking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipmentTracking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId", shipmentTracking.OrderId);
            ViewData["ShipperId"] = new SelectList(_context.Shippers, "ShipperId", "ShipperId", shipmentTracking.ShipperId);
            return View(shipmentTracking);
        }

        // GET: ShipmentTrackings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentTracking = await _context.ShipmentTrackings.FindAsync(id);
            if (shipmentTracking == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId", shipmentTracking.OrderId);
            ViewData["ShipperId"] = new SelectList(_context.Shippers, "ShipperId", "ShipperId", shipmentTracking.ShipperId);
            return View(shipmentTracking);
        }

        // POST: ShipmentTrackings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrackingId,ShipperId,OrderId,UpdateTime,CurrentLocation,ShipmentStatus,TemperatureDuringTransit,HumidityDuringTransit,HandlerName,Remarks,EstimatedArrival")] ShipmentTracking shipmentTracking)
        {
            if (id != shipmentTracking.TrackingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipmentTracking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentTrackingExists(shipmentTracking.TrackingId))
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
            ViewData["OrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId", shipmentTracking.OrderId);
            ViewData["ShipperId"] = new SelectList(_context.Shippers, "ShipperId", "ShipperId", shipmentTracking.ShipperId);
            return View(shipmentTracking);
        }

        // GET: ShipmentTrackings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentTracking = await _context.ShipmentTrackings
                .Include(s => s.Order)
                .Include(s => s.Shipper)
                .FirstOrDefaultAsync(m => m.TrackingId == id);
            if (shipmentTracking == null)
            {
                return NotFound();
            }

            return View(shipmentTracking);
        }

        // POST: ShipmentTrackings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipmentTracking = await _context.ShipmentTrackings.FindAsync(id);
            if (shipmentTracking != null)
            {
                _context.ShipmentTrackings.Remove(shipmentTracking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentTrackingExists(int id)
        {
            return _context.ShipmentTrackings.Any(e => e.TrackingId == id);
        }
    }
}
