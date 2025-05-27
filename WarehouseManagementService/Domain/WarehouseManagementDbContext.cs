using Microsoft.EntityFrameworkCore;
using WarehouseManagementService.Domain.Models;

namespace WarehouseManagementService.Domain
{
    public class WarehouseManagementDbContext : DbContext
    {
        public WarehouseManagementDbContext(DbContextOptions<WarehouseManagementDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }
    }
}
