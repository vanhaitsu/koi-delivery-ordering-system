using KoiDeliveryOrderingSystem.Data.Base;
using KoiDeliveryOrderingSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Data.Repository
{
    public class ShipmentOrderRepository : GenericRepository<ShipmentOrder>
    {
        public ShipmentOrderRepository() { }
        public ShipmentOrderRepository(FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context) => _context = context;
      /*  public async Task<List<ShipmentOrder>> GetAllAsync()
        {
            var result = await _context.ShipmentOrders.Include(x => x.).ThenInclude(x => x.ShipmentOrderDetails).ThenInclude(x => x.AnimalType).ToListAsync();
            return result;
        }*/

        public async Task<ShipmentOrder> GetByIdAsync(int id)
        {
            return await _context.ShipmentOrders.Include(x => x.Customer).Include(x => x.Pricing).FirstOrDefaultAsync(x => x.OrderId == id);
        }
    }
}
