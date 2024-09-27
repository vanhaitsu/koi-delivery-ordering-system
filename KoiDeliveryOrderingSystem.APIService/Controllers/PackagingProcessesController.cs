using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KoiDeliveryOrderingSystem.Service;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Common;

namespace KoiDeliveryOrderingSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagingProcessesController : ControllerBase
    {
        private readonly IPackagingProcessService _packagingProcessService;

        public PackagingProcessesController(IPackagingProcessService packagingProcessService)
        {
            _packagingProcessService = packagingProcessService;
        }

        // GET: api/PackagingProcesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackagingProcess>>> GetPackagingProcesses()
        {
            var result = await _packagingProcessService.GetAll();

            if (result.Status != Const.SUCCESS_READ_CODE)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }

        // GET: api/PackagingProcesses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PackagingProcess>> GetPackagingProcess(int id)
        {
            var result = await _packagingProcessService.GetById(id);

            if (result.Status != Const.SUCCESS_READ_CODE)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }

        // PUT: api/PackagingProcesses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackagingProcess(int id, PackagingProcess packagingProcess)
        {
            if (id != packagingProcess.PackagingProcessId)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _packagingProcessService.Update(packagingProcess);

            if (result.Status != Const.SUCCESS_UPDATE_CODE)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // POST: api/PackagingProcesses
        [HttpPost]
        public async Task<ActionResult<PackagingProcess>> PostPackagingProcess(PackagingProcess packagingProcess)
        {
            var result = await _packagingProcessService.Create(packagingProcess);

            if (result.Status != Const.SUCCESS_CREATE_CODE)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction("GetPackagingProcess", new { id = packagingProcess.PackagingProcessId }, packagingProcess);
        }

        // DELETE: api/PackagingProcesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackagingProcess(int id)
        {
            var result = await _packagingProcessService.DeleteById(id);

            if (result.Status != Const.SUCCESS_DELETE_CODE)
            {
                return NotFound(result.Message);
            }

            return NoContent();
        }

    }
}
