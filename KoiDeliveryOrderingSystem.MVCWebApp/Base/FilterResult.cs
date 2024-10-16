namespace KoiDeliveryOrderingSystem.MVCWebApp.Base
{
    public class FilterResult<T> where T : class
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}
