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

        public async Task<FilterResult<ShipmentTracking>> GetAllAsync(ShipmentTrackingFilterModel model)
        {
            int totalCount = 0;
            IQueryable<ShipmentTracking> query = _context.ShipmentTrackings
                .Include(x => x.Order)
                    .ThenInclude(x => x.ShipmentOrderDetails)
                        .ThenInclude(x => x.AnimalType);

            // Search
            if (!string.IsNullOrWhiteSpace(model.CurrentLocation)|| !string.IsNullOrWhiteSpace(model.HandlerName) || !string.IsNullOrWhiteSpace(model.Remarks))
            {
                query = query.Where(x => x.HandlerName.ToLower().Contains(model.HandlerName.ToLower()) && x.CurrentLocation.ToLower().Contains(model.CurrentLocation.ToLower()) && x.Remarks.ToLower().Contains(model.Remarks.ToLower()));
            }

            // Filter
            if (model.UpdateDate != null)
            {
                query = query.Where(x => x.UpdateTime.HasValue && DateOnly.FromDateTime(x.UpdateTime.Value).Equals(model.UpdateDate));
            }

            // Sort
            if (model.OrderByDescending)
            {
                query = model.Order switch
                {
                    "updateTime" => query.OrderByDescending(x => x.UpdateTime),
                    "shipmentStatus" => query.OrderByDescending(x => x.ShipmentStatus),
                    _ => query.OrderByDescending(x => x.TrackingId),
                };
            }
            else
            {
                query = model.Order switch
                {
                    "updateTime" => query.OrderBy(x => x.UpdateTime),
                    "shipmentStatus" => query.OrderBy(x => x.ShipmentStatus),
                    _ => query.OrderBy(x => x.TrackingId),
                };
            }

            totalCount = await query.CountAsync();

            // Pagination
            int skip = (model.PageNumber - 1) * 10;
            query = query.Skip(skip).Take(10);

            return new FilterResult<ShipmentTracking>
            {
                TotalCount = totalCount,
                Data = await query.ToListAsync()
            };
        }

        public async Task<ShipmentTracking> GetByIdAsync(int id)
        {
            return await _context.ShipmentTrackings.Include(x => x.Shipper).Include(x => x.Order).ThenInclude(x => x.ShipmentOrderDetails).ThenInclude(x => x.AnimalType).FirstOrDefaultAsync(x => x.TrackingId == id);
        }
    }
}
