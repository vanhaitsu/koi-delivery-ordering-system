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
    public class ShipmentTrackingRepository : GenericRepository<ShipmentTracking>
    {
        public ShipmentTrackingRepository() { }
        public ShipmentTrackingRepository(FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context) => _context = context;
        public async Task<List<ShipmentTracking>> GetAllAsync()
        {
            var result = await _context.ShipmentTrackings.Include(x => x.Order).ThenInclude(x => x.ShipmentOrderDetails).ThenInclude(x => x.AnimalType).ToListAsync();
            return result;
        }

        public async Task<ShipmentTracking> GetByIdAsync(int id)
        {
            return await _context.ShipmentTrackings.Include(x => x.Shipper).Include(x => x.Order).ThenInclude(x => x.ShipmentOrderDetails).ThenInclude(x => x.AnimalType).FirstOrDefaultAsync(x => x.TrackingId == id);
        }
    }
}
