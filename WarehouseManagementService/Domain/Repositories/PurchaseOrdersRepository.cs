using Microsoft.EntityFrameworkCore;
using WarehouseManagementService.Domain.Models;

namespace WarehouseManagementService.Domain.Repositories
{
    public class PurchaseOrdersRepository
    {
        private readonly WarehouseManagementDbContext _context;

        public PurchaseOrdersRepository(WarehouseManagementDbContext context)
        {
            _context = context;
        }
        public async Task<List<PurchaseOrder>> GetAllPurchaseOrdersAsync()
        {
            return await _context.PurchaseOrders.Include(po => po.Items).ToListAsync();
        }

        public async Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id)
        {
            return await _context.PurchaseOrders
                .Include(po => po.Items)
                .FirstOrDefaultAsync(po => po.OrderId == id);
        }

        public void CreatePurchaseOrderAsync(PurchaseOrder order)
        {
            _context.PurchaseOrders.Add(order);
        }

        public void DeletePurchaseOrderAsync(PurchaseOrder order)
        {
            _context.PurchaseOrders.Remove(order);
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
