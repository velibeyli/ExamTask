using ProductApi.Models;

namespace ProductApiTest.MockData
{
    public class ProductMockData
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductCategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Price { get; set; }
        public bool IsDeleted { get; set; }
        public string State { get; set; }
        public string GetProductName()
        {
            return ProductName;
        }
        
    }
}