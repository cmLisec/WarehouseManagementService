using AutoMapper;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Models;
using WarehouseManagementService.Domain.Repositories;
using WarehouseManagementService.Domain.Utilities;

namespace WarehouseManagementService.Domain.Services
{
    public class CustomersService
    {
        private readonly CustomersRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(CustomersRepository repo, IMapper mapper, ILogger<CustomersService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<CommonResponseType<List<GetCustomerDto>>> GetAllCustomersAsync()
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var customers = await _repo.GetAllCustomersAsync();
                if (customers.Count == 0)
                    return new CommonResponseType<List<GetCustomerDto>>("No customers available", StatusCodes.Status204NoContent);

                var dtoList = _mapper.Map<List<GetCustomerDto>>(customers);
                return new CommonResponseType<List<GetCustomerDto>>(dtoList, StatusCodes.Status200OK);
            }, _logger, nameof(GetAllCustomersAsync));
        }

        public Task<CommonResponseType<GetCustomerDto>> GetCustomerByIdAsync(int id)
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var customer = await _repo.GetCustomerByIdAsync(id);
                if (customer == null)
                    return new CommonResponseType<GetCustomerDto>("Customer with the given Id not found", StatusCodes.Status404NotFound);

                var dto = _mapper.Map<GetCustomerDto>(customer);
                return new CommonResponseType<GetCustomerDto>(dto, StatusCodes.Status200OK);
            }, _logger, nameof(GetCustomerByIdAsync));
        }

        public Task<CommonResponseType<GetCustomerDto>> CreateCustomerAsync(CustomerDto dto)
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var customer = _mapper.Map<Customer>(dto);
                var customerAdded = await _repo.CreateCustomerAsync(customer);
                await _repo.SaveChangesAsync();

                var result = _mapper.Map<GetCustomerDto>(customerAdded);
                return new CommonResponseType<GetCustomerDto>(result, StatusCodes.Status201Created);
            }, _logger, nameof(CreateCustomerAsync));
        }

        public Task<CommonResponseType<GetCustomerDto>> UpdateCustomerAsync(int id, CustomerDto dto)
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var customer = await _repo.GetCustomerByIdAsync(id);
                if (customer == null)
                    return new CommonResponseType<GetCustomerDto>("Customer with the given Id not found", StatusCodes.Status404NotFound);

                var customerToUpdate = _mapper.Map<Customer>(dto);
                customerToUpdate.CustomerId = id;

                await _repo.UpdateCustomerAsync(customerToUpdate);
                await _repo.SaveChangesAsync();

                var result = _mapper.Map<GetCustomerDto>(customerToUpdate);
                return new CommonResponseType<GetCustomerDto>(result, StatusCodes.Status200OK);
            }, _logger, nameof(UpdateCustomerAsync));
        }

        public Task<CommonResponseType<GetCustomerDto>> DeleteCustomerAsync(int id)
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var customer = await _repo.GetCustomerByIdAsync(id);
                if (customer == null)
                    return new CommonResponseType<GetCustomerDto>("Customer with the given Id not found", StatusCodes.Status404NotFound);

                await _repo.DeleteCustomerAsync(customer);
                await _repo.SaveChangesAsync();

                return new CommonResponseType<GetCustomerDto>();
            }, _logger, nameof(DeleteCustomerAsync));
        }
    }
}
