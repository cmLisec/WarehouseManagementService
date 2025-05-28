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

        public CustomersService(CustomersRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CommonResponseType<List<GetCustomerDto>>> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            if (customers.Count == 0)
                return new CommonResponseType<List<GetCustomerDto>>("No customers available", StatusCodes.Status204NoContent);

            return new CommonResponseType<List<GetCustomerDto>>(_mapper.Map<List<GetCustomerDto>>(customers), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<GetCustomerDto>> GetByIdAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null)
                return new CommonResponseType<GetCustomerDto>("Customer with the given Id not found", StatusCodes.Status404NotFound);

            return new CommonResponseType<GetCustomerDto>(_mapper.Map<GetCustomerDto>(customer), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<GetCustomerDto>> CreateAsync(CustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            var customerAdded = await _repo.AddAsync(customer);
            await _repo.SaveChangesAsync();

            return new CommonResponseType<GetCustomerDto>(_mapper.Map<GetCustomerDto>(customerAdded), StatusCodes.Status201Created);
        }

        public async Task<CommonResponseType<GetCustomerDto>> UpdateAsync(int id, CustomerDto dto)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null)
                return new CommonResponseType<GetCustomerDto>("Customer with the given Id not found", StatusCodes.Status404NotFound);

            var customerToUpdate = _mapper.Map<Customer>(dto);
            customerToUpdate.CustomerId = id;

            await _repo.UpdateAsync(customerToUpdate);
            await _repo.SaveChangesAsync();
            return new CommonResponseType<GetCustomerDto>(_mapper.Map<GetCustomerDto>(customerToUpdate), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<GetCustomerDto>> DeleteAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null)
                return new CommonResponseType<GetCustomerDto>("Customer with the given Id not found", StatusCodes.Status404NotFound);

            await _repo.DeleteAsync(customer);
            await _repo.SaveChangesAsync();
            return new CommonResponseType<GetCustomerDto>();
        }
    }
}
