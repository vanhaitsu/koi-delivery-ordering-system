using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Service.Base;
using KoiDeliveryOrderingSystem.Service;

namespace KoiDeliveryOrderingSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthChecksController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;

        public HealthChecksController() => _healthCheckService ??= new HealthCheckService();
        //public HealthChecksController(HealthCheckService healthCheckService)
        //{
        //    _healthCheckService = healthCheckService;
        //}

        // GET: api/HealthChecks
        [HttpGet]
        public async Task<IBusinessResult> GetHealthChecks()
        {
            return await _healthCheckService.GetAll();
        }

        // GET: api/HealthChecks/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetHealthCheck(int id)
        {
            return await _healthCheckService.GetById(id);
        }

        // PUT: api/HealthChecks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutHealthCheck(HealthCheck healthCheck)
        {
          return  await _healthCheckService.Save(healthCheck);
        }

        // POST: api/HealthChecks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostHealthCheck(HealthCheck healthCheck)
        {
            return await _healthCheckService.Save(healthCheck);
        }

        // DELETE: api/HealthChecks/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteHealthCheck(int id)
        {
            return await _healthCheckService.DeleteById(id);
        }
    }
}
