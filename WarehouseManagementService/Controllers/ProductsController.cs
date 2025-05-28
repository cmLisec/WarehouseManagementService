using Microsoft.AspNetCore.Mvc;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Services;

namespace WarehouseManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CommonController
    {
        private readonly ProductsService _productsService;

        public ProductsController(ProductsService customerService)
        {
            _productsService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetProductDto>>> GetAllProductsAsync()
        {
            var products = await _productsService.GetAllProductsAsync();
            return ReplyCommonResponse(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> GetProductByIdAsync(int id)
        {
            var product = await _productsService.GetProductByIdAsync(id);
            return ReplyCommonResponse(product);
        }

        [HttpPost]
        public async Task<ActionResult<GetProductDto>> CreateProductAsync([FromBody] ProductDto ProductDto)
        {
            var createdProduct = await _productsService.CreateProductAsync(ProductDto);
            return ReplyCommonResponse(createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetProductDto>> UpdateProductAsync(int id, [FromBody] ProductDto ProductDto)
        {
            var updatedProduct = await _productsService.UpdateProductAsync(id, ProductDto);
            return ReplyCommonResponse(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GetProductDto>> DeleteProductAsync(int id)
        {
            var deleted = await _productsService.DeleteProductAsync(id);
            return ReplyCommonResponse(deleted);
        }
    }
}
