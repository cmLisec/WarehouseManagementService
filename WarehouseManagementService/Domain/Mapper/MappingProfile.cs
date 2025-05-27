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
        }
    }
}
