using ProductApi.Db;
using ProductApi.Models;
using ProductApi.Repositories.Interfaces;

namespace ProductApi.Repositories.Implementations
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory>,IProductCategoryRepository
    {
        public ProductCategoryRepository(ProductContext context) : base(context) { }
    }
}
