using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repositories.Interfaces;
using ProductApi.Services.Interfaces;
using ProductApi.Validation;
using Serilog;

namespace ProductApi.Services.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ILogger<ProductService> _logger;
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, ILogger<ProductService> logger)
        {
            _productCategoryRepository = productCategoryRepository ?? throw new ArgumentNullException(nameof(productCategoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        public async Task<ProductCategory> CreateProductCategory(ProductCategory category)
        {
            //validation begin
            var context = new ValidationContext<ProductCategory>(category);
            ProductCategoryValidator productValidator = new ProductCategoryValidator();
            var result = productValidator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
                Log.Error("Category not found");
            }
            //validation end
            try
            {
                Log.Information("New Category was successfully added to database");
                return await _productCategoryRepository.Create(category);
            }
            catch (Exception exception)
            {
                Log.Error($"Error while trying to call Create in service class, Error Message = {exception}.");
                throw;
            }
        }

        
        public async Task<ProductCategory> DeleteCategoryById(int id)
        {
            try
            {
                var result = await _productCategoryRepository.GetById(x => x.ProductCategoryId == id);
                if (result is null)
                {
                    throw new Exception("Category not found");
                    Log.Error("Category not found");
                }

                Log.Warning("Warning! This item will be deleted from database");
                var category = await _productCategoryRepository.Delete(result);
                return category;
            }
            catch (Exception exception)
            {
                Log.Error($"Error while trying to call Deleted in service class, Error Message = {exception}.");
                throw; // if an uncaught exception occurs,return an error response ,with status code 500 (internal server Error)
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            return await _productCategoryRepository.GetAll();
        }
        public async Task<ProductCategory> GetById(int id)
        {
            try
            {
                return await _productCategoryRepository.GetById(x => x.ProductCategoryId == id);
            }
            catch (Exception exception)
            {
                Log.Error($"Error while trying to call GetById in service class, Error Message = {exception}.");
                throw;
            }
        }

        public async Task<ProductCategory> UpdateCategoryById(int id, ProductCategory category)
        {
            //validation begin
            var context = new ValidationContext<ProductCategory>(category);
            ProductCategoryValidator productValidator = new ProductCategoryValidator();
            var resultValidator = productValidator.Validate(context);
            if (!resultValidator.IsValid)
            {
                throw new ValidationException(resultValidator.Errors);
                Log.Error("Category not found");
            }
            //validation end
            try
            {
                var result = await _productCategoryRepository.GetById(x => x.ProductCategoryId == id);
                if (result is null)
                {
                    throw new Exception();
                    Log.Error("Category not found");
                }

                result.ProductCategoryName = category.ProductCategoryName;

                return await _productCategoryRepository.Update(result);
            }
            catch (Exception exception)
            {
                Log.Error($"Error while trying to call Update in service class, Error Message = {exception}.");
                throw;
            }

        }
    }
}
