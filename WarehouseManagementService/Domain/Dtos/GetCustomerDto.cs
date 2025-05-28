namespace WarehouseManagementService.Domain.Dtos
{
    public class GetCustomerDto : CustomerDto
    {
        public int CustomerId { get; set; }
    }
    public class CustomerDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
