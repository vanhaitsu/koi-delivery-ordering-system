using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Service;
using KoiDeliveryOrderingSystem.Service.Base;
using KoiDeliveryOrderingSystem.Data.Base;

namespace KoiDeliveryOrderingSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentTrackingsController : ControllerBase
    {
        private readonly ShipmentTrackingService _shipmentTrackingService;

        //public ShipmentTrackingsController() => _shipmentTrackingService ??= new ShipmentTrackingService();

        public ShipmentTrackingsController(ShipmentTrackingService shipmentTrackingService)
        {
            _shipmentTrackingService = shipmentTrackingService;
        }

        // GET: api/ShipmentTrackings
        [HttpGet]

        public async Task<IBusinessResult> GetShipmentTrackings([FromQuery] ShipmentTrackingFilterModel model)
        {
            return await _shipmentTrackingService.GetAll(model);
        }

        // GET: api/ShipmentTrackings/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetShipmentTracking(int id)
        {
            return await _shipmentTrackingService.GetById(id);
        }

        // PUT: api/ShipmentTrackings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutShipmentTracking(ShipmentTracking shipmentTracking)
        {
            return await _shipmentTrackingService.Save(shipmentTracking);
        }

        // POST: api/ShipmentTrackings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostShipmentTracking(ShipmentTracking shipmentTracking)
        {
            return await _shipmentTrackingService.Save(shipmentTracking);
        }

        // DELETE: api/ShipmentTrackings/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteShipmentTracking(int id)
        {
            return await _shipmentTrackingService.DeleteById(id);
        }
    }
}
