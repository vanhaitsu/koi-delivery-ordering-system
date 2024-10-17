using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Data.Base
{
    public class ShipmentTrackingFilterModel
    {
        public string? Search { get; set; }
        public string? Order { get; set; } = "trackingId";
        public bool OrderByDescending { get; set; } = true;
        public DateOnly? UpdateDate {  get; set; }
        public int PageNumber { get; set; } = 1;
    }
}
