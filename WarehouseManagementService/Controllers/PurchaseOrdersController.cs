using Microsoft.AspNetCore.Mvc;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Services;

namespace WarehouseManagementService.Controllers
{
    /// <summary>
    /// Controller for handling purchase order operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : CommonController
    {
        private readonly PurchaseOrdersService _purchaseOrdersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseOrdersController"/> class.
        /// </summary>
        /// <param name="purchaseOrdersService">Service for purchase order operations.</param>
        public PurchaseOrdersController(PurchaseOrdersService purchaseOrdersService)
        {
            _purchaseOrdersService = purchaseOrdersService;
        }

        /// <summary>
        /// Retrieves all purchase orders.
        /// </summary>
        /// <returns>A list of purchase order DTOs.</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<GetPurchaseOrderDto>>> GetAllPurchaseOrdersAsync()
        {
            var purchaseOrders = await _purchaseOrdersService.GetAllPurchaseOrdersAsync();
            return ReplyCommonResponse(purchaseOrders);
        }

        /// <summary>
        /// Retrieves a purchase order by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the purchase order.</param>
        /// <returns>The purchase order DTO if found.</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetPurchaseOrderDto>> GetPurchaseOrderByIdAsync(int id)
        {
            var purchaseOrder = await _purchaseOrdersService.GetPurchaseOrderByIdAsync(id);
            return ReplyCommonResponse(purchaseOrder);
        }

        /// <summary>
        /// Creates a new purchase order.
        /// </summary>
        /// <param name="purchaseOrderDto">The data for the new purchase order.</param>
        /// <returns>The created purchase order DTO.</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult<GetPurchaseOrderDto>> CreatePurchaseOrderAsync([FromBody] PurchaseOrderDto purchaseOrderDto)
        {
            var createdPurchaseOrder = await _purchaseOrdersService.CreatePurchaseOrderAsync(purchaseOrderDto);
            return ReplyCommonResponse(createdPurchaseOrder);
        }

        /// <summary>
        /// Updates an existing purchase order by ID.
        /// </summary>
        /// <param name="id">The ID of the purchase order to update.</param>
        /// <param name="purchaseOrderDto">The updated purchase order data.</param>
        /// <returns>The updated purchase order DTO.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetPurchaseOrderDto>> UpdatePurchaseOrderAsync(int id, [FromBody] PurchaseOrderDto purchaseOrderDto)
        {
            var updatedPurchaseOrder = await _purchaseOrdersService.UpdatePurchaseOrderAsync(id, purchaseOrderDto);
            return ReplyCommonResponse(updatedPurchaseOrder);
        }

        /// <summary>
        /// Deletes a purchase order by its ID.
        /// </summary>
        /// <param name="id">The ID of the purchase order to delete.</param>
        /// <returns>The deleted purchase order DTO if successful.</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PurchaseOrderDto>> DeletePurchaseOrderAsync(int id)
        {
            var deleted = await _purchaseOrdersService.DeletePurchaseOrderAsync(id);
            return ReplyCommonResponse(deleted);
        }
    }
}
