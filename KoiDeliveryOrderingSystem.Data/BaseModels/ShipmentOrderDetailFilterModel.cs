namespace KoiDeliveryOrderingSystem.Data.BaseModels
{
    public class ShipmentOrderDetailFilterModel
    {
        public string? Order { get; set; } = "";
        public bool OrderByDescending { get; set; } = false;
        public int? AnimalTypeId { get; set; }
        public string? Status { get; set; }
        public string? Search { get; set; }
        public int PageNumber { get; set; } = 1;
    }
}
