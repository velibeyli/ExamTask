using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services.Interfaces;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll() =>
            Ok(await _service.GetAll());

        [HttpGet("getById/{id}")]
        public async Task<ActionResult<Product>> GetById(int id) =>
            Ok(await _service.GetById(id));

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _service.CreateProduct(product));

        }

        [HttpDelete]
        public async Task<ActionResult<Product>> Delete(int id) =>
            Ok(await _service.DeleteProductById(id));

        [HttpPut]
        public async Task<ActionResult<Product>> Update(int id, Product product)
        {
            if (!ModelState.IsValid || id <= 0)
                return BadRequest();

            return Ok(await _service.UpdateProduct(id, product));

        }
    }
}
