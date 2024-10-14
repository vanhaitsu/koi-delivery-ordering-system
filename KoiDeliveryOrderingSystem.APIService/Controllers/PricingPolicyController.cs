
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Service;
using KoiDeliveryOrderingSystem.Service.Base;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.APIService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PricingPolicyController : ControllerBase
  {
    private readonly PricingPolicyService _pricingPolicyService;

    public PricingPolicyController(PricingPolicyService pricingPolicyService)
    {
      _pricingPolicyService = pricingPolicyService;
    }

    // GET: api/PackagingProcesses
    [HttpGet]
    public async Task<IBusinessResult> GetPricingPolicies()
    {
      return await _pricingPolicyService.GetAll();
    }

    // GET: api/PackagingProcesses/5
    [HttpGet("{id}")]
    public async Task<IBusinessResult> GetPricingPolicyById(int id)
    {
      return await _pricingPolicyService.GetById(id);
    }

    // PUT: api/PackagingProcesses/5
    [HttpPut("{id}")]
    public async Task<IBusinessResult> PutPricingPolicy(PricingPolicy pricingPolicy)
    {
      return await _pricingPolicyService.Save(pricingPolicy);
    }

    // POST: api/PackagingProcesses
    [HttpPost]
    public async Task<IBusinessResult> PostPricingPolicy(PricingPolicy pricingPolicy)
    {
      return await _pricingPolicyService.Save(pricingPolicy);
    }

    // DELETE: api/PackagingProcesses/5
    [HttpDelete("{id}")]
    public async Task<IBusinessResult> DeletePricingPolicy(int id)
    {
      return await _pricingPolicyService.DeleteById(id);
    }
  }
}
