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
        public async Task<ActionResult<List<PurchaseOrderReadDto>>> GetAllPurchaseOrders()
        {
            var PurchaseOrders = await _purchaseOrdersService.GetAllAsync();
            return ReplyCommonResponse(PurchaseOrders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrderReadDto>> GetPurchaseOrder(int id)
        {
            var PurchaseOrder = await _purchaseOrdersService.GetByIdAsync(id);
            return ReplyCommonResponse(PurchaseOrder);
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseOrderReadDto>> CreatePurchaseOrder([FromBody] PurchaseOrderDto purchaseOrderDto)
        {
            var createdPurchaseOrder = await _purchaseOrdersService.CreateAsync(purchaseOrderDto);
            return ReplyCommonResponse(createdPurchaseOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PurchaseOrderReadDto>> UpdatePurchaseOrder(int id, [FromBody] PurchaseOrderDto purchaseOrderDto)
        {
            var updatedPurchaseOrder = await _purchaseOrdersService.UpdateAsync(id, purchaseOrderDto);
            return ReplyCommonResponse(updatedPurchaseOrder);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PurchaseOrderDto>> DeletePurchaseOrder(int id)
        {
            var deleted = await _purchaseOrdersService.DeleteAsync(id);
            return ReplyCommonResponse(deleted);
        }
    }
}
