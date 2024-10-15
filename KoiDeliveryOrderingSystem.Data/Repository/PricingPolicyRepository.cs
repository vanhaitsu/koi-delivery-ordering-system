using KoiDeliveryOrderingSystem.Data.Base;
using KoiDeliveryOrderingSystem.Data.Models;

namespace KoiDeliveryOrderingSystem.Data.Repository
{
  public class PricingPolicyRepository : GenericRepository<PricingPolicy>
  {
    public PricingPolicyRepository() { }
    public PricingPolicyRepository(FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context) => _context = context;
  }
}
