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
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<GetSalesOrderDto>>> GetAllSalesOrdersAsync()
        {
            var SalesOrders = await _SalesOrdersService.GetAllSalesOrdersAsync();
            return ReplyCommonResponse(SalesOrders);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetSalesOrderDto>> GetSalesOrderByIdAsync(int id)
        {
            var SalesOrder = await _SalesOrdersService.GetSalesOrderByIdAsync(id);
            return ReplyCommonResponse(SalesOrder);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult<GetSalesOrderDto>> CreateSalesOrderAsync([FromBody] SalesOrderDto SalesOrderDto)
        {
            var createdSalesOrder = await _SalesOrdersService.CreateSalesOrderAsync(SalesOrderDto);
            return ReplyCommonResponse(createdSalesOrder);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetSalesOrderDto>> UpdateSalesOrderAsync(int id, [FromBody] SalesOrderDto SalesOrderDto)
        {
            var updatedSalesOrder = await _SalesOrdersService.UpdateSalesOrderAsync(id, SalesOrderDto);
            return ReplyCommonResponse(updatedSalesOrder);
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SalesOrderDto>> DeleteSalesOrderAsync(int id)
        {
            var deleted = await _SalesOrdersService.DeleteSalesOrderAsync(id);
            return ReplyCommonResponse(deleted);
        }
    }
}
