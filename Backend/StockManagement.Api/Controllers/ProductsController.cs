using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Api.Core.Entities;
using StockManagement.Api.Core.Interfaces;
using StockManagement.Api.Services;
using StockManagement.Core.Enums;

namespace StockManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }
        // 1. Visualizar productos por estado
        [HttpGet("state/{state}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public async Task<IActionResult> GetByState(ProductState state)
        {
            var products = await _service.GetByStateAsync(state);
            return Ok(products);
        }

        // 2. Registrar un producto individual
        [HttpPost]
        [ProducesResponseType(typeof(Product), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            try
            {
                await _service.AddProductAsync(product);
                return CreatedAtAction(nameof(GetByState), new { state = product.State }, product);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 2. Registrar productos masivos
        [HttpPost("bulk")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddBulk([FromBody] List<Product> products)
        {
            try
            {
                await _service.AddProductsAsync(products);
                return Ok(new { message = "Productos agregados correctamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 3. Marcar salida de producto
        [HttpPut("{id}/ship")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Ship(Guid id)
        {
            try
            {
                await _service.MarkAsShippedAsync(id);
                return Ok(new { message = "Producto marcado como salido" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 4. Marcar producto como defectuoso
        [HttpPut("{id}/defect")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> MarkAsDefective(Guid id)
        {
            try
            {
                await _service.MarkAsDefectiveAsync(id);
                return Ok(new { message = "Producto marcado como defectuoso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
