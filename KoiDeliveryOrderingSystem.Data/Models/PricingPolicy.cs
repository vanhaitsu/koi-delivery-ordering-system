﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KoiDeliveryOrderingSystem.Data.Models;

public partial class PricingPolicy
{
    public int PricingId { get; set; }

    public string WeightRange { get; set; }

    public string ShippingMethod { get; set; }

    public decimal? BasePrice { get; set; }

    public decimal? AdditionalServicePrice { get; set; }

    public decimal? InsuranceFee { get; set; }

    public decimal? CustomsFee { get; set; }

    public string Currency { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public virtual ICollection<ShipmentOrder> ShipmentOrders { get; set; } = new List<ShipmentOrder>();
}