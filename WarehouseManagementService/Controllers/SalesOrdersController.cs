using Microsoft.AspNetCore.Mvc;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Services;

namespace WarehouseManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrdersController : CommonController
    {
        private readonly SalesOrdersService _SalesOrdersService;

        public SalesOrdersController(SalesOrdersService SalesOrdersService)
        {
            _SalesOrdersService = SalesOrdersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetSalesOrderDto>>> GetAllSalesOrders()
        {
            var SalesOrders = await _SalesOrdersService.GetAllAsync();
            return ReplyCommonResponse(SalesOrders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetSalesOrderDto>> GetSalesOrder(int id)
        {
            var SalesOrder = await _SalesOrdersService.GetByIdAsync(id);
            return ReplyCommonResponse(SalesOrder);
        }

        [HttpPost]
        public async Task<ActionResult<GetSalesOrderDto>> CreateSalesOrder([FromBody] SalesOrderDto SalesOrderDto)
        {
            var createdSalesOrder = await _SalesOrdersService.CreateAsync(SalesOrderDto);
            return ReplyCommonResponse(createdSalesOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetSalesOrderDto>> UpdateSalesOrder(int id, [FromBody] SalesOrderDto SalesOrderDto)
        {
            var updatedSalesOrder = await _SalesOrdersService.UpdateAsync(id, SalesOrderDto);
            return ReplyCommonResponse(updatedSalesOrder);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SalesOrderDto>> DeleteSalesOrder(int id)
        {
            var deleted = await _SalesOrdersService.DeleteAsync(id);
            return ReplyCommonResponse(deleted);
        }
    }
}
