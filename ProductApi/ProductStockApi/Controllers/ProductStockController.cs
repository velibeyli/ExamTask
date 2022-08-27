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
        [HttpGet("getStock")]
        public async Task<ActionResult<ProductStock>> GetStock(int productId) =>
            Ok(await _service.GetStock(productId));
    }
}
