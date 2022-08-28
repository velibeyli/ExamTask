using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using ProductStockApi.Db;
using ProductStockApi.Models;
using ProductStockApi.Repositories.Interfaces;
using ProductStockApi.Services.Interfaces;
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

                    ProductStock responseData = JsonConvert.DeserializeObject<ProductStock>(response.Content.ReadAsStringAsync().Result);

                    if (responseData.ProductId == null)
                    {
                        Log.Error("Deleted product");
                        throw new Exception("This data is already deleted from database!");
                    }
                }
                var stock = _context.ProductStocks.OrderByDescending(x => x.Id).
                                    FirstOrDefault(x => x.ProductId == productId);

                if (stock == null)
                {
                    Log.Error("Out of stock");
                    throw new Exception("There is not any product stock with this Id in database");
                }


                ProductStock productStock = new ProductStock();
                productStock.ProductId = productId;
                productStock.NewAddedProductCount = newAddedProductCount;
                productStock.StockCount = stock.StockCount + productStock.NewAddedProductCount;


                Log.Information("successfully operation");
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
                        throw new Exception("This data is already deleted from database!");
                    }
                }
                var stock = _context.ProductStocks.OrderByDescending(x => x.Id).
                                    FirstOrDefault(x => x.ProductId == productId);

                //Create new Product
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

                    if (responseData.IsDeleted == true)
                    {
                        throw new Exception("This data is already deleted from database!");
                    }
                }
                var stock = _context.ProductStocks.OrderByDescending(x => x.Id).
                                    FirstOrDefault(x => x.ProductId == productId);
                if (stock.StockCount <= 0)
                {
                    Log.Error("Out of stock");
                    throw new Exception("OutOfStock");
                }
                else if(stock.StockCount < newSoldProductCount)
                {
                    Log.Error("Out of stock");
                    throw new Exception("OutOfStock");
                }

                ProductStock productStock = new ProductStock();
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
