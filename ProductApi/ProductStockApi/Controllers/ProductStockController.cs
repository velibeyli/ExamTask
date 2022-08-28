using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductStockApi.Models;
using ProductStockApi.Services.Interfaces;

namespace ProductStockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStockController : ControllerBase
    {
        private readonly IProductStockService _service;
        public ProductStockController(IProductStockService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ProductStock>> GetStock(int id) =>
            Ok(await _service.GetStock(id));

        [HttpPost]
        public async Task<ActionResult<ProductStock>> AddStock(int productId, int newAddedProductCount) =>
            Ok(await _service.AddStock(productId,newAddedProductCount));

        [HttpDelete]
        public async Task<ActionResult<ProductStock>> RemoveStock(int productId, int newSoldProductCount) =>
            Ok(await _service.RemoveStock(productId,newSoldProductCount));

       

    }
}
