using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KoiDeliveryOrderingSystem.Service;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Common;
using KoiDeliveryOrderingSystem.Service.Base;

namespace KoiDeliveryOrderingSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagingProcessesController : ControllerBase
    {
        private readonly PackagingProcessService _packagingProcessService;

        public PackagingProcessesController(PackagingProcessService packagingProcessService)
        {
            _packagingProcessService = packagingProcessService;
        }

        // GET: api/PackagingProcesses
        [HttpGet]
        public async Task<IBusinessResult> GetPackagingProcesses()
        {
            return await _packagingProcessService.GetAll();
        }

        // GET: api/PackagingProcesses/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetPackagingProcess(int id)
        {
            return await _packagingProcessService.GetById(id);      
        }

        // PUT: api/PackagingProcesses/5
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutPackagingProcess(PackagingProcess packagingProcess)
        {
            return await _packagingProcessService.Save(packagingProcess);
        }

        // POST: api/PackagingProcesses
        [HttpPost]
        public async Task<IBusinessResult> PostPackagingProcess(PackagingProcess packagingProcess)
        {
            return await _packagingProcessService.Save(packagingProcess);
        }

        // DELETE: api/PackagingProcesses/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeletePackagingProcess(int id)
        {
          return await _packagingProcessService.DeleteById(id);
        }

    }
}
