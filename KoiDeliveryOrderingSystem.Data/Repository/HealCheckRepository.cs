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
    public class HealCheckRepository : GenericRepository<HealthCheck>
    {
        public HealCheckRepository() { }
        public HealCheckRepository(FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context) => _context = context;
        public async Task<List<HealthCheck>> GetAllAsync()
        {
            return await _context.HealthChecks.Include(a => a.ShipmentOrderDetail).ToListAsync();
        }
        public async Task<HealthCheck> GetByIdAsync(int id)
        {
            return await _context.HealthChecks.Include(a => a.ShipmentOrderDetail).FirstOrDefaultAsync(a => a.HealthCheckId == id);
        }
    }

}
