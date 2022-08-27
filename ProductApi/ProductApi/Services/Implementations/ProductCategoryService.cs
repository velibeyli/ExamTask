using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repositories.Interfaces;
using ProductApi.Services.Interfaces;

namespace ProductApi.Services.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _repo;
        public ProductCategoryService(IProductCategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<ProductCategory> Create(ProductCategory category)
        {
            var result = await _repo.GetAll(x => x.ProductCategoryName == category.ProductCategoryName);
            if (result is not null)
                throw new Exception("Bu adla kategoriya movcuddur");
            return await _repo.Create(category);
        }

        public async Task<ProductCategory> Delete(int id)
        {
            var result = await _repo.GetById(x => x.ProductCategoryId == id);
            if (result is null)
                throw new Exception("Bu ID-li kategoriya movcud deyil");
            var deletedCategory = await _repo.Delete(result);
            return deletedCategory;
        }

        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<ProductCategory> GetById(int id)
        {
            var result = await _repo.GetById(x => x.ProductCategoryId == id);
            if (result is null)
                throw new Exception("Bu ID-li kategoriya movcud deyil");
            return result;
        }

        public async Task<ProductCategory> Update(int id, [FromBody] ProductCategory category)
        {
            var result = await _repo.GetById(x => x.ProductCategoryId == id);
            if (result is null)
                throw new Exception("Bu ID-li kategoriya movcud deyil");
            result.ProductCategoryName = category.ProductCategoryName;
            return await _repo.Update(result);
        }
    }
}
