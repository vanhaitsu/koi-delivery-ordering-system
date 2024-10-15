using KoiDeliveryOrderingSystem.Data.Base;
using KoiDeliveryOrderingSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrderingSystem.Data.Repository
{
    public class ShipmentOrderDetailRepository : GenericRepository<ShipmentOrderDetail>
    {
        public ShipmentOrderDetailRepository() { }
        public ShipmentOrderDetailRepository(FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context) => _context = context;
        public async Task<List<ShipmentOrderDetail>> GetAllAsync()
        {
            var result = await _context.ShipmentOrderDetails
                .Include(x => x.ShipmentOrder)
                .Include(x => x.HealthChecks)
                .Include(x => x.AnimalType)
                .ToListAsync();
            return result;
        }

        public async Task<ShipmentOrderDetail> GetByIdAsync(int id)
        {
            return await _context.ShipmentOrderDetails
                .Include(x => x.ShipmentOrder)
                .Include(x => x.HealthChecks)
                .Include(x => x.AnimalType)
                .FirstOrDefaultAsync(x => x.ShipmentOrderDetailId == id);
        }
    }

}
