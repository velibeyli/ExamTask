namespace ProductApi.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public Product Product { get; set; }
    }
}
