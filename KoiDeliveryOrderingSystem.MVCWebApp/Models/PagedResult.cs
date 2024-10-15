namespace KoiDeliveryOrderingSystem.MVCWebApp.Models
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int TotalCount { get; set; }
    }

}
