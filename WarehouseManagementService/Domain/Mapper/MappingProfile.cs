using AutoMapper;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Models;

namespace WarehouseManagementService.Domain.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, GetCustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<PurchaseOrder, GetPurchaseOrderDto>().ReverseMap();
            CreateMap<PurchaseOrderDto, PurchaseOrder>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)).ReverseMap()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<PurchaseOrderItem, GetPurchaseOrderItemDto>().ReverseMap();
            CreateMap<PurchaseOrderItemDto, PurchaseOrderItem>().ReverseMap();

            CreateMap<SalesOrder, GetSalesOrderDto>().ReverseMap();
            CreateMap<SalesOrderDto, SalesOrder>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)).ReverseMap()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<SalesOrderItem, GetSalesOrderItemDto>().ReverseMap();
            CreateMap<SalesOrderItemDto, SalesOrderItem>().ReverseMap();
        }
    }
}
