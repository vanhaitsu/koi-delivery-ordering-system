﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Data.BaseModels
{
    public class HealthCheckFilterModel
    {
        public string? Order { get; set; } = "id";
        public bool OrderByDescending { get; set; } = false;
        public string? Condition { get; set; } 
        public string? Search { get; set; } 
        public string? PackagingType { get; set; }
        public int PageNumber { get; set; } = 1;
    }
}