using Microsoft.AspNetCore.Mvc;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Services;

namespace WarehouseManagementService.Controllers
{
    /// <summary>
    /// Controller responsible for managing sales order operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrdersController : CommonController
    {
        private readonly SalesOrdersService _salesOrdersService;

        /// <summary>
        /// Constructor for <see cref="SalesOrdersController"/>.
        /// </summary>
        /// <param name="salesOrdersService">Service for handling sales orders.</param>
        public SalesOrdersController(SalesOrdersService salesOrdersService)
        {
            _salesOrdersService = salesOrdersService;
        }

        /// <summary>
        /// Retrieves all sales orders.
        /// </summary>
        /// <returns>A list of sales order DTOs.</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<GetSalesOrderDto>>> GetAllSalesOrdersAsync()
        {
            var salesOrders = await _salesOrdersService.GetAllSalesOrdersAsync();
            return ReplyCommonResponse(salesOrders);
        }

        /// <summary>
        /// Retrieves a specific sales order by its ID.
        /// </summary>
        /// <param name="id">The ID of the sales order.</param>
        /// <returns>The sales order DTO if found.</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetSalesOrderDto>> GetSalesOrderByIdAsync(int id)
        {
            var salesOrder = await _salesOrdersService.GetSalesOrderByIdAsync(id);
            return ReplyCommonResponse(salesOrder);
        }

        /// <summary>
        /// Creates a new sales order.
        /// </summary>
        /// <param name="salesOrderDto">The sales order data.</param>
        /// <returns>The created sales order DTO.</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult<GetSalesOrderDto>> CreateSalesOrderAsync([FromBody] SalesOrderDto salesOrderDto)
        {
            var createdSalesOrder = await _salesOrdersService.CreateSalesOrderAsync(salesOrderDto);
            return ReplyCommonResponse(createdSalesOrder);
        }

        /// <summary>
        /// Updates an existing sales order by ID.
        /// </summary>
        /// <param name="id">The ID of the sales order to update.</param>
        /// <param name="salesOrderDto">The updated sales order data.</param>
        /// <returns>The updated sales order DTO.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetSalesOrderDto>> UpdateSalesOrderAsync(int id, [FromBody] SalesOrderDto salesOrderDto)
        {
            var updatedSalesOrder = await _salesOrdersService.UpdateSalesOrderAsync(id, salesOrderDto);
            return ReplyCommonResponse(updatedSalesOrder);
        }

        /// <summary>
        /// Deletes a sales order by its ID.
        /// </summary>
        /// <param name="id">The ID of the sales order to delete.</param>
        /// <returns>The deleted sales order DTO if successful.</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SalesOrderDto>> DeleteSalesOrderAsync(int id)
        {
            var deleted = await _salesOrdersService.DeleteSalesOrderAsync(id);
            return ReplyCommonResponse(deleted);
        }
    }
}
