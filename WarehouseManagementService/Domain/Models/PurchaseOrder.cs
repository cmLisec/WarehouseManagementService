using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementService.Domain.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int OrderId { get; set; }

        public DateTime ProcessingDate { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<PurchaseOrderItem> Items { get; set; }
    }
}
