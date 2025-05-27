using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementService.Domain.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
