using Microsoft.AspNetCore.Mvc;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Services;

namespace WarehouseManagementService.Controllers
{
    /// <summary>
    /// Controller for managing product-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CommonController
    {
        private readonly ProductsService _productsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="customerService">The service responsible for product operations.</param>
        public ProductsController(ProductsService customerService)
        {
            _productsService = customerService;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of product DTOs.</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<GetProductDto>>> GetAllProductsAsync()
        {
            var products = await _productsService.GetAllProductsAsync();
            return ReplyCommonResponse(products);
        }

        /// <summary>
        /// Retrieves a product by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product DTO if found.</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetProductDto>> GetProductByIdAsync(int id)
        {
            var product = await _productsService.GetProductByIdAsync(id);
            return ReplyCommonResponse(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="ProductDto">The product data transfer object.</param>
        /// <returns>The created product DTO.</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult<GetProductDto>> CreateProductAsync([FromBody] ProductDto ProductDto)
        {
            var createdProduct = await _productsService.CreateProductAsync(ProductDto);
            return ReplyCommonResponse(createdProduct);
        }

        /// <summary>
        /// Updates an existing product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="ProductDto">The updated product data.</param>
        /// <returns>The updated product DTO.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetProductDto>> UpdateProductAsync(int id, [FromBody] ProductDto ProductDto)
        {
            var updatedProduct = await _productsService.UpdateProductAsync(id, ProductDto);
            return ReplyCommonResponse(updatedProduct);
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>The deleted product DTO if successful.</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetProductDto>> DeleteProductAsync(int id)
        {
            var deleted = await _productsService.DeleteProductAsync(id);
            return ReplyCommonResponse(deleted);
        }
    }
}
