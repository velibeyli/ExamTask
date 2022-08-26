using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services.Interfaces;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll() =>
            Ok(await _service.GetAll());
        [HttpGet("getById")]
        public async Task<ActionResult<Product>> GetById(int id) =>
            Ok(await _service.GetById(id));
        [HttpPost("create")]
        public async Task<ActionResult<Product>> Create(Product product) =>
            Ok(await _service.Create(product));
        [HttpDelete("delete")]
        public async Task<ActionResult<Product>> Delete(int id) =>
            Ok(await _service.Delete(id));
        [HttpPut("update")]
        public async Task<ActionResult<Product>> Update(int id, Product product) =>
            Ok(await _service.Update(id,product));

    }
}
