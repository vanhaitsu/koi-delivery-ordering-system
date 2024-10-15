using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Data.BaseModels
{
    public class QueryResultModel<TEntity> where TEntity : class
    {
        public int TotalCount { get; set; }
        public List<TEntity?> Data { get; set; }
    }
}
