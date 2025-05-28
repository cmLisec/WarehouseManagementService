using Microsoft.AspNetCore.Mvc;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Services;

namespace WarehouseManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : CommonController
    {
        private readonly PurchaseOrdersService _purchaseOrdersService;

        public PurchaseOrdersController(PurchaseOrdersService purchaseOrdersService)
        {
            _purchaseOrdersService = purchaseOrdersService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<GetPurchaseOrderDto>>> GetAllPurchaseOrdersAsync()
        {
            var PurchaseOrders = await _purchaseOrdersService.GetAllPurchaseOrdersAsync();
            return ReplyCommonResponse(PurchaseOrders);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetPurchaseOrderDto>> GetPurchaseOrderByIdAsync(int id)
        {
            var PurchaseOrder = await _purchaseOrdersService.GetPurchaseOrderByIdAsync(id);
            return ReplyCommonResponse(PurchaseOrder);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult<GetPurchaseOrderDto>> CreatePurchaseOrderAsync([FromBody] PurchaseOrderDto purchaseOrderDto)
        {
            var createdPurchaseOrder = await _purchaseOrdersService.CreatePurchaseOrderAsync(purchaseOrderDto);
            return ReplyCommonResponse(createdPurchaseOrder);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetPurchaseOrderDto>> UpdatePurchaseOrderAsync(int id, [FromBody] PurchaseOrderDto purchaseOrderDto)
        {
            var updatedPurchaseOrder = await _purchaseOrdersService.UpdatePurchaseOrderAsync(id, purchaseOrderDto);
            return ReplyCommonResponse(updatedPurchaseOrder);
        }

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
