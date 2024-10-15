﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Data.Base
{
    public class FilterResult<T> where T : class
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}
