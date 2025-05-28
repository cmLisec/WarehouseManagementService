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
        public async Task<ActionResult<List<ProductDto>>> GetAllCustomers()
        {
            var products = await _productsService.GetAllAsync();
            return ReplyCommonResponse(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetCustomer(int id)
        {
            var product = await _productsService.GetByIdAsync(id);
            return ReplyCommonResponse(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateCustomer([FromBody] BaseProductDto ProductDto)
        {
            var createdProduct = await _productsService.CreateAsync(ProductDto);
            return ReplyCommonResponse(createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> UpdateCustomer(int id, [FromBody] BaseProductDto ProductDto)
        {
            var updatedProduct = await _productsService.UpdateAsync(id, ProductDto);
            return ReplyCommonResponse(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDto>> DeleteCustomer(int id)
        {
            var deleted = await _productsService.DeleteAsync(id);
            return ReplyCommonResponse(deleted);
        }
    }
}
