 using KoiDeliveryOrderingSystem.Data.Base;
using KoiDeliveryOrderingSystem.Data.BaseModels;
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
        public async Task<QueryResultModel<HealthCheck>> GetAllAsync(HealthCheckFilterModel healthCheckFilterModel)
        {
            IQueryable<HealthCheck> query = _context.HealthChecks.Include(a => a.ShipmentOrderDetail);
            // Search
            if (!string.IsNullOrEmpty(healthCheckFilterModel.Search))
            {
                string searchLower = healthCheckFilterModel.Search.ToLower();
                query = query.Where(a =>
                    a.DoctorName.ToLower().Contains(searchLower) ||
                    a.Condition.ToLower().Contains(searchLower) ||
                    a.Temperature.ToString().Equals(healthCheckFilterModel.Search) ||
                    a.Weight.ToString().Equals(healthCheckFilterModel.Search));
            }
            if (!string.IsNullOrEmpty(healthCheckFilterModel.PackagingType))
            {
                query = query.Where(a => a.PackagingType == healthCheckFilterModel.PackagingType);
            }

            if (!string.IsNullOrEmpty(healthCheckFilterModel.Condition))
            {
                query = query.Where(a => a.Condition == healthCheckFilterModel.Condition);
            }
            if (!string.IsNullOrEmpty(healthCheckFilterModel.Order))
            {
                switch (healthCheckFilterModel.Order)
                {
                    case "doctorName":
                        query = healthCheckFilterModel.OrderByDescending
                            ? query.OrderBy(a => a.DoctorName)
                            : query.OrderByDescending(a => a.DoctorName);
                        break;
                    case "temperature":
                        query = healthCheckFilterModel.OrderByDescending
                            ? query.OrderBy(a => a.Temperature)
                            : query.OrderByDescending(a => a.Temperature);
                        break;
                    case "weight":
                        query = healthCheckFilterModel.OrderByDescending
                            ? query.OrderBy(a => a.Weight)
                            : query.OrderByDescending(a => a.Weight);
                        break;
                    case "id":
                        query = healthCheckFilterModel.OrderByDescending
                            ? query.OrderBy(a => a.HealthCheckId)
                            : query.OrderByDescending(a => a.HealthCheckId);
                        break;
                    default:
                        query = healthCheckFilterModel.OrderByDescending
                            ? query.OrderBy(a => a.CheckDate)
                            : query.OrderByDescending(a => a.CheckDate);
                        break;
                }
            }
            else
            {
                query = healthCheckFilterModel.OrderByDescending
                    ? query.OrderBy(a => a.HealthCheckId)
                    : query.OrderByDescending(a => a.HealthCheckId);
            }
            int totalRecords = await query.CountAsync();
            int pageSize = 10;
            query = query.Skip((healthCheckFilterModel.PageNumber - 1) * pageSize).Take(pageSize);
            List<HealthCheck> healthChecks = await query.ToListAsync();
            return new QueryResultModel<HealthCheck>
            {
                TotalCount = totalRecords,
                Data = healthChecks
            };
    }

        public async Task<HealthCheck> GetByIdAsync(int id)
        {
            return await _context.HealthChecks.Include(a => a.ShipmentOrderDetail).FirstOrDefaultAsync(a => a.HealthCheckId == id);
        }
    }

}
