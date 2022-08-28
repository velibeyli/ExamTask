using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repositories.Interfaces;
using ProductApi.Services.Interfaces;
using ProductApi.Validation;
using Serilog;
using System.Web.Mvc;

namespace ProductApi.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Product> CreateProduct(Product product)
        {
            //validation begin
            var context = new ValidationContext<Product>(product);
            ProductValidator productValidator = new ProductValidator();
            var result = productValidator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
                Log.Error("Product properties are not valid");
            }
            //validation end
            try
            {
                return await _productRepository.Create(product);
                Log.Information("New Product added to database");
            }
            catch (Exception exception)
            {
                Log.Error($"Error while trying to call Create in service class, Error Message = {exception}.");
                throw; 
            }
        }

        public async Task<Product> DeleteProductById(int id)
        {
            try
            {
                var result = await _productRepository.GetById(x => x.ProductId == id);
                if (result is null)
                {
                    throw new Exception();
                    Log.Error("Product not found");
                }

                Log.Warning("Warning! This item will be deleted from database");
                var deletedProduct = await _productRepository.Delete(result);
                return deletedProduct;
            }
            catch (Exception exception)
            {
                Log.Error($"Error while trying to call Deleted in service class, Error Message = {exception}.");
                throw; // if an uncaught exception occurs,return an error response ,with status code 500 (internal server Error)
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            Log.Information("All products was called");
            return await _productRepository.GetAll();
        }

        public async Task<Product> GetById(int id)
        {
            try
            {
                return await _productRepository.GetById(x => x.ProductId == id);
                Log.Information("Successfully operation");
            }
            catch (Exception exception)
            {
                Log.Error($"Error while trying to call GetById in service class, Error Message = {exception}.");
                throw;
            }
        }

        public async Task<Product> UpdateProduct(int id,Product product)
        {
            //validation begin
            var context = new ValidationContext<Product>(product);
            ProductValidator productValidator = new ProductValidator();
            var resultValidator = productValidator.Validate(context);
            if (!resultValidator.IsValid)
            {
                throw new ValidationException(resultValidator.Errors);
                Log.Error("Product properties are not valid");
            }
            //validation end
            try
            {
                var result = await _productRepository.GetById(x => x.ProductId == id);
                if (result is null)
                {
                    throw new Exception();
                    Log.Error("Product not found");
                }

                Product updatedProduct = new Product()
                {
                    ProductName = product.ProductName,
                    ProductCategoryId = product.ProductCategoryId,
                    Price = product.Price,
                    IsDeleted = product.IsDeleted,
                    CreatedDate = product.CreatedDate,
                    State = product.State
                };
                return await _productRepository.Update(updatedProduct);
            }
            catch (Exception exception)
            {
                Log.Error($"Error while trying to call Update in service class, Error Message = {exception}.");
                    throw;
            }

        }
    }
}
