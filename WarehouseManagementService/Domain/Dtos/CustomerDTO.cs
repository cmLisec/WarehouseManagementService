namespace WarehouseManagementService.Domain.Dtos
{
    public class CustomerDto : BaseCustomerDto
    {
        public int CustomerId { get; set; }
    }
    public class BaseCustomerDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
