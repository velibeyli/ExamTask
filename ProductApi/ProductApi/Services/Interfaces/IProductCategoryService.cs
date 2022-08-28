using ProductApi.Models;

namespace ProductApi.Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAll();
        Task<ProductCategory> GetById(int id);
        Task<ProductCategory> CreateProductCategory(ProductCategory category);
        Task<ProductCategory> UpdateCategoryById(int id, ProductCategory category);
        Task<ProductCategory> DeleteCategoryById(int id);
    }
}
