using ProductApi.Models;

namespace ProductApi.Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAll();
        Task<ProductCategory> GetById(int id);
        Task<ProductCategory> Create(ProductCategory category);
        Task<ProductCategory> Update(int id, ProductCategory category);
        Task<ProductCategory> Delete(int id);
    }
}
