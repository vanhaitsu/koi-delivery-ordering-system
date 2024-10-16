using KoiDeliveryOrderingSystem.Data.Base;
using KoiDeliveryOrderingSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrderingSystem.Data.Repository
{
    public class AnimalTypeRepository : GenericRepository<AnimalType>
    {
        public AnimalTypeRepository() { }
        public AnimalTypeRepository(FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context) => _context = context;
    }
}
