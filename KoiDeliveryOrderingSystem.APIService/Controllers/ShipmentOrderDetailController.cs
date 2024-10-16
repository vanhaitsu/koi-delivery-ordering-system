using KoiDeliveryOrderingSystem.Data.Base;
using KoiDeliveryOrderingSystem.Data.BaseModels;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Service;
using KoiDeliveryOrderingSystem.Service.Base;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentOrderDetailController : ControllerBase
    {
        private readonly ShipmentOrderDetailService _shipmentOrderDetailService;

        //public ShipmentTrackingsController() => _shipmentTrackingService ??= new ShipmentTrackingService();

        public ShipmentOrderDetailController(ShipmentOrderDetailService shipmentOrderDetailService)
        {
            _shipmentOrderDetailService = shipmentOrderDetailService;
        }

        // GET: api/ShipmentTrackings
        [HttpGet]

        public async Task<IBusinessResult> GetShipmentOrderDetails([FromQuery] ShipmentOrderDetailFilterModel model)
        {
            return await _shipmentOrderDetailService.GetAllFilter(model);
        }

        // GET: api/ShipmentTrackings/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetShipmentOrderDetails(int id)
        {
            return await _shipmentOrderDetailService.GetById(id);
        }

        // PUT: api/ShipmentTrackings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutShipmentOrderDetails(ShipmentOrderDetail shipmentOrderDetail)
        {
            return await _shipmentOrderDetailService.Save(shipmentOrderDetail);
        }

        // POST: api/ShipmentTrackings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostShipmentOrderDetails(ShipmentOrderDetail shipmentOrderDetail)
        {
            return await _shipmentOrderDetailService.Save(shipmentOrderDetail);
        }

        // DELETE: api/ShipmentTrackings/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteShipmentOrderDetails(int id)
        {
            return await _shipmentOrderDetailService.DeleteById(id);
        }
    }
}
