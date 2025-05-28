using Microsoft.EntityFrameworkCore;
using WarehouseManagementService.Domain.Models;

namespace WarehouseManagementService.Domain.Repositories
{
    public class CustomersRepository
    {
        private readonly WarehouseManagementDbContext _context;

        public CustomersRepository(WarehouseManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.AsNoTracking().FirstOrDefaultAsync(i => i.CustomerId == id);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            return customer;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await Task.CompletedTask;
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
