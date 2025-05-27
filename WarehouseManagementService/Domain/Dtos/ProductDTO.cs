using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementService.Domain.Dtos
{
    public class ProductDto : BaseProductDto
    {
        public int ProductId { get; set; }
    }
    public class BaseProductDto
    {
        [Required]
        public string ProductCode { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Dimensions { get; set; }
    }
}
