using ProductApi.Models;

namespace ProductApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product> DeleteProductById(int id);
        Task<Product> UpdateProduct(int id, Product product);
    }
}
