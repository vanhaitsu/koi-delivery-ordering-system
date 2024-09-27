using KoiDeliveryOrderingSystem.Data.Base;
using KoiDeliveryOrderingSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Data.Repository
{
    public class ShipperRepository: GenericRepository<Shipper>
    {
        public ShipperRepository() { }
        public ShipperRepository(FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context) => _context = context;
    }
}
