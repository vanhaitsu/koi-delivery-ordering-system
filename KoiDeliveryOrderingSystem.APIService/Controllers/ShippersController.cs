﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Service;
using KoiDeliveryOrderingSystem.Service.Base;

namespace KoiDeliveryOrderingSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private readonly ShipperService _shipperService;

        public ShippersController(ShipperService shipperService)
        {
            _shipperService = shipperService;   
        }

        // GET: api/Shippers
        [HttpGet]
        public async Task<IBusinessResult> GetShippers()
        {
            return await _shipperService.GetAll();
        }

        //// GET: api/Shippers/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Shipper>> GetShipper(int id)
        //{
        //    var shipper = await _context.Shippers.FindAsync(id);

        //    if (shipper == null)
        //    {
        //        return NotFound();
        //    }

        //    return shipper;
        //}

        //// PUT: api/Shippers/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutShipper(int id, Shipper shipper)
        //{
        //    if (id != shipper.ShipperId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(shipper).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ShipperExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Shippers
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Shipper>> PostShipper(Shipper shipper)
        //{
        //    _context.Shippers.Add(shipper);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ShipperExists(shipper.ShipperId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetShipper", new { id = shipper.ShipperId }, shipper);
        //}

        //// DELETE: api/Shippers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteShipper(int id)
        //{
        //    var shipper = await _context.Shippers.FindAsync(id);
        //    if (shipper == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Shippers.Remove(shipper);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ShipperExists(int id)
        //{
        //    return _context.Shippers.Any(e => e.ShipperId == id);
        //}
    }
}
