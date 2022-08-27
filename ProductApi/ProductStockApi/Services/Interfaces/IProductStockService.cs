using ProductStockApi.Models;

namespace ProductStockApi.Services.Interfaces
{
    public interface IProductStockService
    {
        Task<IEnumerable<ProductStock>> GetStocks();
        Task<ProductStock> GetStock(int productId);
        Task<ProductStock> AddStock(int productId, int newAddedProductCount);
        Task<ProductStock> RemoveStock(int productId, int newSoldProductCount);
    }
}
