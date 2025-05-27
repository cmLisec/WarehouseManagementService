using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementService.Domain.Models
{
    public class SalesOrderItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SalesOrder")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
