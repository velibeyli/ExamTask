namespace ProductStockApi.Models
{
    public class ProductStock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int NewAddedProductCount { get; set; }
        public int NewSoldProductCount { get; set; }
        public int StockCount { get; set; }
    }
}
