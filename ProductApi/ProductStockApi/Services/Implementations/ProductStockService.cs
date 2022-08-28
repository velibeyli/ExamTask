using FluentValidation;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using ProductStockApi.Db;
using ProductStockApi.Models;
using ProductStockApi.Repositories.Interfaces;
using ProductStockApi.Services.Interfaces;
using ProductStockApi.Validation;
using Serilog;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace ProductStockApi.Services.Implementations
{
    public class ProductStockService : IProductStockService
    {
        private readonly IProductStockRepository _repo;
        private readonly ProductStockContext _context;
        public ProductStockService(IProductStockRepository repo,ProductStockContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<ProductStock> AddStock(int productId, int newAddedProductCount)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7280/api/Product/getById/{productId}");

                    ProductStock responseData = JsonConvert.DeserializeObject
                        <ProductStock>(response.Content.ReadAsStringAsync().Result);

                    //validation begin
                    var context = new ValidationContext<ProductStock>(responseData);
                    ProductStockValidator productStockValidator = new ProductStockValidator();
                    var result = productStockValidator.Validate(context);
                    if (!result.IsValid)
                    {
                        throw new ValidationException(result.Errors);
                        Log.Error("ProductStock properties are not valid");
                    }
                    //validation end

                    if (responseData.ProductId == null)
                    {
                        Log.Error("Deleted product");
                        throw new Exception("This data is already deleted from database!");
                    }

                }
                var stock = _context.ProductStocks.OrderByDescending(x => x.Id).
                                    FirstOrDefault(x => x.ProductId == productId);

                ProductStock productStock = new ProductStock();

                if (stock == null)
                {
                    productStock.ProductId = productId;
                    productStock.NewAddedProductCount = newAddedProductCount;
                    productStock.StockCount = productStock.NewAddedProductCount;
                    return await _repo.Create(productStock);
                }

                    productStock.ProductId = productId;
                    productStock.NewAddedProductCount = newAddedProductCount;
                    productStock.StockCount = stock.StockCount + productStock.NewAddedProductCount;

                return await _repo.Create(productStock);
            }
            catch (Exception EX)
            {
                Log.Error("could not connected to database");
                throw;
            }
            
           
        }

        public async Task<ProductStock> GetStock(int productId)
        {
            try
            {
                //Create HttpCLient CAll for Product Service
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7280/api/Product/getById/{productId}");

                    Product responseData = JsonConvert.DeserializeObject<Product>(response.Content.ReadAsStringAsync().Result);

                    if (responseData.IsDeleted == true)
                    {
                        Log.Error("This data is already deleted from database!");
                        throw new Exception();
                    }
                }

                var stock = _context.ProductStocks.OrderByDescending(x => x.Id).
                                    FirstOrDefault(x => x.ProductId == productId);

                if(stock == null)
                {
                    Log.Error("There is not any data with this productId in database");
                    throw new Exception();
                }

                ProductStock product = new ProductStock()
                {
                    ProductId = productId,
                    StockCount = stock.StockCount
                };

                return product;
            }
            catch (Exception EX)
            {
                Log.Error("could not connected to database");
                throw;
            }
        }

        public async Task<ProductStock> RemoveStock(int productId, int newSoldProductCount)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7280/api/Product/getById/{productId}");

                    Product responseData = JsonConvert.DeserializeObject<Product>(response.Content.ReadAsStringAsync().Result);

                    if (responseData == null)
                    {
                        Log.Error("There is not any product with this id in Product database");
                        throw new Exception();
                    }

                    if (responseData.IsDeleted == true)
                    {
                        Log.Error("This data is already deleted from database!");
                        throw new Exception();
                    }
                }
                var stock = _context.ProductStocks.OrderByDescending(x => x.Id).
                                    FirstOrDefault(x => x.ProductId == productId);

                ProductStock productStock = new ProductStock();

                if (stock == null)
                {
                    Log.Error("There is not any data with this productId in database");
                    throw new Exception();
                }
                if (stock.StockCount <= 0)
                {
                    Log.Error("Out of stock");
                    throw new Exception();
                }
                else if(stock.StockCount < newSoldProductCount)
                {
                    Log.Error("Out of stock");
                    throw new Exception();
                }

                productStock.ProductId = productId;
                productStock.NewSoldProductCount = newSoldProductCount;
                productStock.StockCount = stock.StockCount - productStock.NewSoldProductCount;


                Log.Information("Successfully operation");
                // string data = JsonConvert.SerializeObject(productStock);
                // var contentData = new StringContent(data);

                // HttpResponseMessage request = await client.PostAsync("https://localhost:7280/api/Product/create",contentData);

                return await _repo.Create(productStock);
            }
            catch (Exception EX)
            {
                Log.Error("could not connected to database");
                throw;
            }
        }
    }
}
