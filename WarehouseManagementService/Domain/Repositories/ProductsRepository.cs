using Microsoft.EntityFrameworkCore;
using WarehouseManagementService.Domain.Models;

namespace WarehouseManagementService.Domain.Repositories
{
    public class ProductsRepository
    {
        private readonly WarehouseManagementDbContext _context;

        public ProductsRepository(WarehouseManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(i => i.ProductId == id);
        }

        public async Task<Product> AddAsync(Product Product)
        {
            await _context.Products.AddAsync(Product);
            return Product;
        }

        public async Task UpdateAsync(Product Product)
        {
            _context.Products.Update(Product);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Product Product)
        {
            _context.Products.Remove(Product);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
