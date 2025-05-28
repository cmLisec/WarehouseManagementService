using Microsoft.AspNetCore.Mvc;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Services;

namespace WarehouseManagementService.Controllers
{
    /// <summary>
    /// Controller responsible for managing customer-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : CommonController
    {
        private readonly CustomersService _customerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController"/> class.
        /// </summary>
        /// <param name="customerService">Service for customer operations.</param>
        public CustomersController(CustomersService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Retrieves a list of all customers.
        /// </summary>
        /// <returns>A list of customer DTOs.</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<GetCustomerDto>>> GetAllCustomersAsync()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return ReplyCommonResponse(customers);
        }

        /// <summary>
        /// Retrieves a specific customer by their ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>A customer DTO if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetCustomerDto>> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            return ReplyCommonResponse(customer);
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customerDto">The customer data to create.</param>
        /// <returns>The created customer DTO.</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult<GetCustomerDto>> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            var createdCustomer = await _customerService.CreateCustomerAsync(customerDto);
            return ReplyCommonResponse(createdCustomer);
        }

        /// <summary>
        /// Updates an existing customer's information.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="customerDto">The updated customer data.</param>
        /// <returns>The updated customer DTO.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetCustomerDto>> UpdateCustomer(int id, [FromBody] CustomerDto customerDto)
        {
            var updatedCustomer = await _customerService.UpdateCustomerAsync(id, customerDto);
            return ReplyCommonResponse(updatedCustomer);
        }

        /// <summary>
        /// Deletes a customer by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>The deleted customer DTO if successful.</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetCustomerDto>> DeleteCustomer(int id)
        {
            var deleted = await _customerService.DeleteCustomerAsync(id);
            return ReplyCommonResponse(deleted);
        }
    }
}
