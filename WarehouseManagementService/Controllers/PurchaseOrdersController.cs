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
        public async Task<ActionResult<List<GetPurchaseOrderDto>>> GetAllPurchaseOrdersAsync()
        {
            var PurchaseOrders = await _purchaseOrdersService.GetAllPurchaseOrdersAsync();
            return ReplyCommonResponse(PurchaseOrders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPurchaseOrderDto>> GetPurchaseOrderByIdAsync(int id)
        {
            var PurchaseOrder = await _purchaseOrdersService.GetPurchaseOrderByIdAsync(id);
            return ReplyCommonResponse(PurchaseOrder);
        }

        [HttpPost]
        public async Task<ActionResult<GetPurchaseOrderDto>> CreatePurchaseOrderAsync([FromBody] PurchaseOrderDto purchaseOrderDto)
        {
            var createdPurchaseOrder = await _purchaseOrdersService.CreatePurchaseOrderAsync(purchaseOrderDto);
            return ReplyCommonResponse(createdPurchaseOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetPurchaseOrderDto>> UpdatePurchaseOrderAsync(int id, [FromBody] PurchaseOrderDto purchaseOrderDto)
        {
            var updatedPurchaseOrder = await _purchaseOrdersService.UpdatePurchaseOrderAsync(id, purchaseOrderDto);
            return ReplyCommonResponse(updatedPurchaseOrder);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PurchaseOrderDto>> DeletePurchaseOrderAsync(int id)
        {
            var deleted = await _purchaseOrdersService.DeletePurchaseOrderAsync(id);
            return ReplyCommonResponse(deleted);
        }
    }
}
