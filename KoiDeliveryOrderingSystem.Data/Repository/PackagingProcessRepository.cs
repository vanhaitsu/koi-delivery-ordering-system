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
    public class PackagingProcessRepository : GenericRepository<PackagingProcess>
    {
        public PackagingProcessRepository() { }

        public PackagingProcessRepository(FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context)
        {
            _context = context;
        }
    }
}
