using Newtonsoft.Json;
using ProductStockApi.Models;
using ProductStockApi.Repositories.Interfaces;
using ProductStockApi.Services.Interfaces;
using System.Text.Json;

namespace ProductStockApi.Services.Implementations
{
    public class ProductStockService : IProductStockService
    {
        private readonly IProductStockRepository _repo;
        public ProductStockService(IProductStockRepository repo)
        {
            _repo = repo;
        }

        public async Task<ProductStock> AddStock(int productId, int newAddedProductCount)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7280/api/Product/getById");
            }
            throw new NotImplementedException();
        }

        public async Task<ProductStock> GetStock(int productId)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7280/api/Product/getById/{productId}");

                var responseData = response.Content.ReadAsStringAsync().Result;

                if (responseData.Length == 0)
                {
                    throw new Exception("Out of stock");
                }
                 var result = JsonConvert.DeserializeObject<ProductStock>(responseData);
                return result;
            }
        }

        public Task<IEnumerable<ProductStock>> GetStocks()
        {
            throw new NotImplementedException();
        }

        public Task<ProductStock> RemoveStock(int productId, int newSoldProductCount)
        {
            throw new NotImplementedException();
        }
    }
}
