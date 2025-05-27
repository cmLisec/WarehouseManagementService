using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementService.Domain.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductCode { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Dimensions { get; set; }

        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public ICollection<SalesOrderItem> SalesOrderItems { get; set; }
    }
}
