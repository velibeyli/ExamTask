using ProductApi.Models;

namespace ProductApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> Create(Product product);
        Task<Product> Delete(int id);
        Task<Product> Update(int id, Product product);
    }
}
