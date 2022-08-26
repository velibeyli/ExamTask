using ProductApi.Db;
using ProductApi.Models;
using ProductApi.Repositories.Interfaces;

namespace ProductApi.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(ProductContext context) : base(context) { }
    }
}
