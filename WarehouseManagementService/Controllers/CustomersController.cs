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
        public async Task<ActionResult<List<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllAsync();
            return ReplyCommonResponse(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            return ReplyCommonResponse(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] BaseCustomerDto customerDto)
        {
            var createdCustomer = await _customerService.CreateAsync(customerDto);
            return ReplyCommonResponse(createdCustomer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto>> UpdateCustomer(int id, [FromBody] BaseCustomerDto customerDto)
        {
            var updatedCustomer = await _customerService.UpdateAsync(id, customerDto);
            return ReplyCommonResponse(updatedCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerDto>> DeleteCustomer(int id)
        {
            var deleted = await _customerService.DeleteAsync(id);
            return ReplyCommonResponse(deleted);
        }
    }
}
