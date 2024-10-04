﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KoiDeliveryOrderingSystem.Data.Models;

public partial class ShipmentTracking
{
    public int TrackingId { get; set; }

    public int? ShipperId { get; set; }

    public int? OrderId { get; set; }

    public DateTime? UpdateTime { get; set; }

    public string CurrentLocation { get; set; }

    public string ShipmentStatus { get; set; }

    public decimal? TemperatureDuringTransit { get; set; }

    public decimal? HumidityDuringTransit { get; set; }

    public string HandlerName { get; set; }

    public string Remarks { get; set; }

    public DateTime? EstimatedArrival { get; set; }

    public virtual ICollection<HealthCheck> HealthChecks { get; set; } = new List<HealthCheck>();

    public virtual ShipmentOrder Order { get; set; }

    public virtual Shipper Shipper { get; set; }
}