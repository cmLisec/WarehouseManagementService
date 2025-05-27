using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementService.Domain.Models
{
    public class PurchaseOrderItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
