using AutoMapper;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Models;

namespace WarehouseManagementService.Domain.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, BaseCustomerDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, BaseProductDto>().ReverseMap();

            CreateMap<PurchaseOrder, PurchaseOrderReadDto>().ReverseMap();
            CreateMap<PurchaseOrderDto, PurchaseOrder>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)).ReverseMap()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<PurchaseOrderItem, PurchaseOrderItemReadDto>().ReverseMap();
            CreateMap<PurchaseOrderItemDto, PurchaseOrderItem>().ReverseMap();

            CreateMap<SalesOrder, SalesOrderReadDto>().ReverseMap();
            CreateMap<SalesOrderDto, SalesOrder>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)).ReverseMap()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<SalesOrderItem, SalesOrderItemReadDto>().ReverseMap();
            CreateMap<SalesOrderItemDto, SalesOrderItem>().ReverseMap();
        }
    }
}
