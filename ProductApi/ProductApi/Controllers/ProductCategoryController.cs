using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services.Interfaces;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _service;
        public ProductCategoryController(IProductCategoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAll() =>
            Ok(await _service.GetAll());
        [HttpGet("getById")]
        public async Task<ActionResult<ProductCategory>> GetById(int id) =>
            Ok(await _service.GetById(id));
        [HttpPost("create")]
        public async Task<ActionResult<ProductCategory>> Create(ProductCategory category) =>
            Ok(await _service.Create(category));
        [HttpPut("update")]
        public async Task<ActionResult<ProductCategory>> Update(int id, ProductCategory category) =>
            Ok(await _service.Update(id,category));
        [HttpDelete("delete")]
        public async Task<ActionResult<ProductCategory>> Delete(int id) =>
            Ok(await _service.Delete(id));
    }
}
