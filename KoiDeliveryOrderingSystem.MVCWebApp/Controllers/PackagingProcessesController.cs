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


        /* // GET: PackagingProcesses/Details/5
         public async Task<IActionResult> Details(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var packagingProcess = await _context.PackagingProcesses
                 .Include(p => p.ShipmentOrder)
                 .FirstOrDefaultAsync(m => m.PackagingProcessId == id);
             if (packagingProcess == null)
             {
                 return NotFound();
             }

             return View(packagingProcess);
         }

         // GET: PackagingProcesses/Create
         public IActionResult Create()
         {
             ViewData["ShipmentOrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId");
             return View();
         }

         // POST: PackagingProcesses/Create
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("PackagingProcessId,ShipmentOrderId,PackagingType,PackagingInstructions,PackagingDate,HandlerName,QualityCheckStatus,Remarks,EstimatedPackagingTime,ActualPackagingTime,PackagingCost")] PackagingProcess packagingProcess)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(packagingProcess);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             ViewData["ShipmentOrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId", packagingProcess.ShipmentOrderId);
             return View(packagingProcess);
         }

         // GET: PackagingProcesses/Edit/5
         public async Task<IActionResult> Edit(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var packagingProcess = await _context.PackagingProcesses.FindAsync(id);
             if (packagingProcess == null)
             {
                 return NotFound();
             }
             ViewData["ShipmentOrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId", packagingProcess.ShipmentOrderId);
             return View(packagingProcess);
         }

         // POST: PackagingProcesses/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, [Bind("PackagingProcessId,ShipmentOrderId,PackagingType,PackagingInstructions,PackagingDate,HandlerName,QualityCheckStatus,Remarks,EstimatedPackagingTime,ActualPackagingTime,PackagingCost")] PackagingProcess packagingProcess)
         {
             if (id != packagingProcess.PackagingProcessId)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(packagingProcess);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!PackagingProcessExists(packagingProcess.PackagingProcessId))
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
             ViewData["ShipmentOrderId"] = new SelectList(_context.ShipmentOrders, "OrderId", "OrderId", packagingProcess.ShipmentOrderId);
             return View(packagingProcess);
         }

         // GET: PackagingProcesses/Delete/5
         public async Task<IActionResult> Delete(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var packagingProcess = await _context.PackagingProcesses
                 .Include(p => p.ShipmentOrder)
                 .FirstOrDefaultAsync(m => m.PackagingProcessId == id);
             if (packagingProcess == null)
             {
                 return NotFound();
             }

             return View(packagingProcess);
         }

         // POST: PackagingProcesses/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             var packagingProcess = await _context.PackagingProcesses.FindAsync(id);
             if (packagingProcess != null)
             {
                 _context.PackagingProcesses.Remove(packagingProcess);
             }

             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }

         private bool PackagingProcessExists(int id)
         {
             return _context.PackagingProcesses.Any(e => e.PackagingProcessId == id);
         }*/
    }
}
