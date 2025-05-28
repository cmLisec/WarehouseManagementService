using Microsoft.EntityFrameworkCore;
using WarehouseManagementService.Domain.Models;

namespace WarehouseManagementService.Domain.Repositories
{
    public class SalesOrdersRepository
    {
        private readonly WarehouseManagementDbContext _context;

        public SalesOrdersRepository(WarehouseManagementDbContext context)
        {
            _context = context;
        }
        public async Task<List<SalesOrder>> GetAllAsync()
        {
            return await _context.SalesOrders.Include(po => po.Items).ToListAsync();
        }

        public async Task<SalesOrder?> GetByIdAsync(int id)
        {
            return await _context.SalesOrders
                .Include(po => po.Items)
                .FirstOrDefaultAsync(po => po.OrderId == id);
        }

        public void Add(SalesOrder order)
        {
            _context.SalesOrders.Add(order);
        }

        public void Remove(SalesOrder order)
        {
            _context.SalesOrders.Remove(order);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool CustomerExists(int customerId)
        {
            return _context.Customers.Any(c => c.CustomerId == customerId);
        }

        public List<int> ProductsExists(IEnumerable<int> productIds)
        {
            var existingIds = _context.Products
                .Where(p => productIds.Contains(p.ProductId))
                .Select(p => p.ProductId)
                .ToList();

            return productIds.Except(existingIds).ToList();
        }
    }
}
