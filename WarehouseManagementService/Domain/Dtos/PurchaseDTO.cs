namespace WarehouseManagementService.Domain.Dtos
{
    public class PurchaseOrderDto
    {
        public DateTime ProcessingDate { get; set; }
        public int CustomerId { get; set; }
        public List<PurchaseOrderItemDto> Items { get; set; }
    }

    public class GetPurchaseOrderDto : PurchaseOrderDto
    {
        public int OrderId { get; set; }
    }
    public class PurchaseOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class GetPurchaseOrderItemDto : PurchaseOrderItemDto
    {
        public int Id { get; set; }
    }
}
