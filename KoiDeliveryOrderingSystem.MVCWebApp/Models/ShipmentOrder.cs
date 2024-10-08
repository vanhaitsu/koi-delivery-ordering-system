﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KoiDeliveryOrderingSystem.MVCWebApp.Models;

public partial class ShipmentOrder
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public int? PricingId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string OriginLocation { get; set; }

    public string DestinationLocation { get; set; }

    public decimal? TotalWeight { get; set; }

    public int? TotalQuantity { get; set; }

    public string ShipmentMethod { get; set; }

    public string AdditionalServices { get; set; }

    public decimal? AdditionalFee { get; set; }

    public string OrderStatus { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<PackagingProcess> PackagingProcesses { get; set; } = new List<PackagingProcess>();

    public virtual PricingPolicy Pricing { get; set; }

    public virtual ICollection<ShipmentOrderDetail> ShipmentOrderDetails { get; set; } = new List<ShipmentOrderDetail>();

    public virtual ICollection<ShipmentTracking> ShipmentTrackings { get; set; } = new List<ShipmentTracking>();
}