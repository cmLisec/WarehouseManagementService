using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManagementService.Domain.Models
{
    public class SalesOrder
    {
        [Key]
        public int OrderId { get; set; }

        public DateTime ProcessingDate { get; set; }
        public string ShipmentAddress { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<SalesOrderItem> Items { get; set; }
    }
}
