namespace ProductApi.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductCategoryId { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string State { get; set; }
        public bool IsDeleted { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
