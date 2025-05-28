namespace WarehouseManagementService.Domain.Dtos
{
    public class SalesOrderDto
    {
        public DateTime ProcessingDate { get; set; }
        public string ShipmentAddress { get; set; }
        public int CustomerId { get; set; }
        public List<SalesOrderItemDto> Items { get; set; }
    }

    public class GetSalesOrderDto : SalesOrderDto
    {
        public int OrderId { get; set; }
    }

    public class GetSalesOrderItemDto : SalesOrderItemDto
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }
    }

    public class SalesOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
