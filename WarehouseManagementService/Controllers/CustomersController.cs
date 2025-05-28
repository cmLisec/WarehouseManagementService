using Microsoft.AspNetCore.Mvc;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Services;

namespace WarehouseManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : CommonController
    {
        private readonly CustomersService _customerService;

        public CustomersController(CustomersService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCustomerDto>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllAsync();
            return ReplyCommonResponse(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerDto>> GetCustomer(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            return ReplyCommonResponse(customer);
        }

        [HttpPost]
        public async Task<ActionResult<GetCustomerDto>> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            var createdCustomer = await _customerService.CreateAsync(customerDto);
            return ReplyCommonResponse(createdCustomer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetCustomerDto>> UpdateCustomer(int id, [FromBody] CustomerDto customerDto)
        {
            var updatedCustomer = await _customerService.UpdateAsync(id, customerDto);
            return ReplyCommonResponse(updatedCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GetCustomerDto>> DeleteCustomer(int id)
        {
            var deleted = await _customerService.DeleteAsync(id);
            return ReplyCommonResponse(deleted);
        }
    }
}
