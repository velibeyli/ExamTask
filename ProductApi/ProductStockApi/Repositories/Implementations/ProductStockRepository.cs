using ProductStockApi.Db;
using ProductStockApi.Models;
using ProductStockApi.Repositories.Interfaces;

namespace ProductStockApi.Repositories.Implementations
{
    public class ProductStockRepository : GenericRepository<ProductStock>,IProductStockRepository
    {
        public ProductStockRepository(ProductStockContext context) : base(context) { }
    }
}
