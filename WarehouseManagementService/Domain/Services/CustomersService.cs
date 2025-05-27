using AutoMapper;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Models;
using WarehouseManagementService.Domain.Repositories;

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

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> CreateAsync(BaseCustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            await _repo.AddAsync(customer);
            await _repo.SaveChangesAsync();
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateAsync(int id, BaseCustomerDto dto)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {id} not found.");

            _mapper.Map(dto, customer);
            await _repo.UpdateAsync(customer);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {id} not found.");

            await _repo.DeleteAsync(customer);
            await _repo.SaveChangesAsync();
        }
    }
}
