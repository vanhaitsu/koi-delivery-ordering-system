using KoiDeliveryOrderingSystem.Data.Base;
using KoiDeliveryOrderingSystem.Data.BaseModels;
using KoiDeliveryOrderingSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrderingSystem.Data.Repository
{
    public class ShipmentOrderDetailRepository : GenericRepository<ShipmentOrderDetail>
    {
        public ShipmentOrderDetailRepository() { }
        public ShipmentOrderDetailRepository(FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context) => _context = context;
        public async Task<FilterResult<ShipmentOrderDetail>> GetAllAsync(ShipmentOrderDetailFilterModel shipmentOrderDetailFilterModel)
        {
            int totalCount = 0;
            IQueryable<ShipmentOrderDetail> query = _context.ShipmentOrderDetails
                .Include(x => x.AnimalType)
                    .Include(x => x.HealthChecks)
                        .Include(x => x.ShipmentOrder);

            // Search
            if (!string.IsNullOrWhiteSpace(shipmentOrderDetailFilterModel.Search))
            {
                query = query.Where(x => x.ShipmentOrderDetailId.ToString().Contains(shipmentOrderDetailFilterModel.Search));
            }

            // Filter
            if (!string.IsNullOrWhiteSpace(shipmentOrderDetailFilterModel.Status))
            {
                query = query.Where(x => x.Status.ToString().Contains(shipmentOrderDetailFilterModel.Status));
            }

            if (!string.IsNullOrWhiteSpace(shipmentOrderDetailFilterModel.Origin))
            {
                query = query.Where(x => x.Origin.ToString().Contains(shipmentOrderDetailFilterModel.Origin));
            }

            // Sort
            if (shipmentOrderDetailFilterModel.OrderByDescending)
            {
                query = shipmentOrderDetailFilterModel.Order switch
                {
                    _ => query.OrderByDescending(x => x.DateOfEntry),
                };
            }
            else
            {
                query = shipmentOrderDetailFilterModel.Order switch
                {
                    _ => query.OrderBy(x => x.DateOfEntry),
                };
            }

            totalCount = await query.CountAsync();

            // Pagination
            int skip = ((int)shipmentOrderDetailFilterModel.PageNumber - 1) * 10;
            query = query.Skip(skip).Take(10);

            return new FilterResult<ShipmentOrderDetail>
            {
                TotalCount = totalCount,
                Data = await query.ToListAsync()
            };
        }

        public async Task<int> GetMaxShipmentOrderDetailIdAsync()
        {
            var maxId = await _context.Set<ShipmentOrderDetail>()
                .MaxAsync(x => (int?)x.ShipmentOrderDetailId) ?? 0;
            return maxId;
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
