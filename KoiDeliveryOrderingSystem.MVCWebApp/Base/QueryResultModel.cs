namespace KoiDeliveryOrderingSystem.MVCWebApp.Base
{
    public class QueryResultModel<TEntity> where TEntity : class
    {
        public int TotalCount { get; set; }
        public List<TEntity?> Data { get; set; }
    }
}
