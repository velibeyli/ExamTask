using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repositories.Interfaces;
using ProductApi.Services.Interfaces;

namespace ProductApi.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<Product> Create(Product product)
        {
            var res = await _repo.GetAll(x => x.ProductName == product.ProductName);
            if (res is not null)
                throw new Exception("Bu adla mehsul movcuddur");
            return await _repo.Create(product);
        }

        public async Task<Product> Delete(int id)
        {
            var res = await _repo.GetById(x => x.ProductId == id);
            if (res is null)
                throw new Exception("Mehsul tapilmadi");
            var deletedProduct = await _repo.Delete(res);
            return deletedProduct;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Product> GetById(int id)
        {

            //using (var client = new HttpClient())
            //{
            //    HttpRequestMessage request = new()
            //    {
            //        RequestUri = new Uri($"https://localhost:7280/api/Product/getById/{id}"),
            //        Method = HttpMethod.Get
            //    };
            //    HttpResponseMessage response = await client.GetAsync(request);

            //    var responseData = response.Content.ReadAsStringAsync().Result;

            //    if (responseData.Length !=0)
            //    {
            //        throw new Exception("");
            //    }
            //}
            var res = await _repo.GetById(x => x.ProductId == id);
            if (res is null)
                throw new Exception("Mehsul tapilmadi");
            return res;
        }

        public async Task<Product> Update(int id, [FromBody]Product product)
        {
            var res = await _repo.GetById(x => x.ProductId == id);
            if (res is null)
                throw new Exception("Mehsul tapilmadi");
            res.ProductName = product.ProductName;
            res.Price = product.Price;
            res.ProductCategoryId = product.ProductCategoryId;
            res.CreatedDate = product.CreatedDate;
            res.State = product.State;
            res.IsDeleted = product.IsDeleted;

            return await _repo.Update(res);

        }
    }
}
