using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Service.Base;
using KoiDeliveryOrderingSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentOrdersController : ControllerBase
    {
        private readonly ShipmentOrderService _shipmentOrderService;

        //public ShipmentOrdersController() => _shipmentOrderService ??= new ShipmentOrderService();

        public ShipmentOrdersController(ShipmentOrderService shipmentOrderService)
        {
            _shipmentOrderService = shipmentOrderService;
        }

        // GET: api/ShipmentOrders
        [HttpGet]

        public async Task<IBusinessResult> GetShipmentOrders()
        {
            return await _shipmentOrderService.GetAll();
        }

        // GET: api/ShipmentOrders/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetShipmentOrder(int id)
        {
            return await _shipmentOrderService.GetById(id);
        }

        // PUT: api/ShipmentOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutShipmentOrder(ShipmentOrder shipmentOrder)
        {
            return await _shipmentOrderService.Save(shipmentOrder);
        }

        // POST: api/ShipmentOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostShipmentOrder(ShipmentOrder shipmentOrder)
        {
            return await _shipmentOrderService.Save(shipmentOrder);
        }

        // DELETE: api/ShipmentOrders/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteShipmentOrder(int id)
        {
            return await _shipmentOrderService.DeleteById(id);
        }
    }
}
