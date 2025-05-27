using AutoMapper;
using System.Net;
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

        public async Task<CommonResponseType<List<CustomerDto>>> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            if (customers.Count == 0)
                return new CommonResponseType<List<CustomerDto>>("No customers available", StatusCodes.Status204NoContent);

            return new CommonResponseType<List<CustomerDto>>(_mapper.Map<List<CustomerDto>>(customers), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<CustomerDto>> GetByIdAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null)
                return new CommonResponseType<CustomerDto>("Customer with the given Id not found", StatusCodes.Status404NotFound);

            return  new CommonResponseType<CustomerDto>(_mapper.Map<CustomerDto>(customer), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<CustomerDto>> CreateAsync(BaseCustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            var customerAdded = await _repo.AddAsync(customer);
            await _repo.SaveChangesAsync();

            return new CommonResponseType<CustomerDto>(_mapper.Map<CustomerDto>(customerAdded), StatusCodes.Status201Created);
        }

        public async Task<CommonResponseType<CustomerDto>> UpdateAsync(int id, BaseCustomerDto dto)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null)
                return new CommonResponseType<CustomerDto>("Customer with the given Id not found", StatusCodes.Status404NotFound);

            var customerToUpdate = _mapper.Map<Customer>(dto);
            customerToUpdate.CustomerId = id;

            await _repo.UpdateAsync(customerToUpdate);
            await _repo.SaveChangesAsync();
            return new CommonResponseType<CustomerDto>(_mapper.Map<CustomerDto>(customerToUpdate), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<CustomerDto>> DeleteAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null)
                return new CommonResponseType<CustomerDto>("Customer with the given Id not found", StatusCodes.Status404NotFound);

            await _repo.DeleteAsync(customer);
            await _repo.SaveChangesAsync();
            return new CommonResponseType<CustomerDto>();
        }
    }
}
