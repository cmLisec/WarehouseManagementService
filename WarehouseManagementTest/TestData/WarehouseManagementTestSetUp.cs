using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WarehouseManagementService.Domain;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Models;


namespace WarehouseManagementTest.TestData
{
    public class WarehouseManagementTestSetUp
    {
        protected WarehouseManagementDbContext _context;
        protected IMapper _mapper;

        public void InitialiseSetup()
        {
            var options = new DbContextOptionsBuilder<WarehouseManagementDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new WarehouseManagementDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, GetCustomerDto>().ReverseMap();
                cfg.CreateMap<Customer, CustomerDto>().ReverseMap();

                cfg.CreateMap<Product, GetProductDto>().ReverseMap();
                cfg.CreateMap<Product, ProductDto>().ReverseMap();

                cfg.CreateMap<PurchaseOrder, GetPurchaseOrderDto>().ReverseMap();
                cfg.CreateMap<PurchaseOrderDto, PurchaseOrder>()
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)).ReverseMap()
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

                cfg.CreateMap<PurchaseOrderItem, GetPurchaseOrderItemDto>().ReverseMap();
                cfg.CreateMap<PurchaseOrderItemDto, PurchaseOrderItem>().ReverseMap();

                cfg.CreateMap<SalesOrder, GetSalesOrderDto>().ReverseMap();
                cfg.CreateMap<SalesOrderDto, SalesOrder>()
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)).ReverseMap()
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

                cfg.CreateMap<SalesOrderItem, GetSalesOrderItemDto>().ReverseMap();
                cfg.CreateMap<SalesOrderItemDto, SalesOrderItem>().ReverseMap();
            });

            _mapper = config.CreateMapper();
        }
        public ILogger<T> CreateLoggerInstance<T>() where T : class
        {
            return LoggerFactory
                .Create(builder => builder.AddConsole())
                .CreateLogger<T>();
        }
        public async Task AddRequiredTestData()
        {
            var purchaseOrder = new PurchaseOrder
            {
                CustomerId = 1,
                Items = new List<PurchaseOrderItem>()
                {
                    new PurchaseOrderItem()
                    {
                        ProductId = 1,
                        Quantity = 10
                    }
                },
                ProcessingDate = DateTime.Now
            };
            _context.PurchaseOrders.Add(purchaseOrder);
            var salesOrder = new SalesOrder
            {
                CustomerId = 1,
                Items = new List<SalesOrderItem>()
                {
                    new SalesOrderItem()
                    {
                        ProductId = 1,
                        Quantity = 10
                    }
                },
                ProcessingDate = DateTime.Now,
                ShipmentAddress = "dubai"
            };
            _context.SalesOrders.Add(salesOrder);
            var customer = new Customer { Name = "John Doe", Address = "456 Lane" };
            await _context.Customers.AddAsync(customer);
            _context.Products.Add(new Product
            {
                Description = "product1",
                Dimensions = "100*200",
                ProductCode = "p1",
                Title = "new"
            });
            await _context.SaveChangesAsync();
            // Detach to prevent multiple tracking issues
            _context.Entry(purchaseOrder).State = EntityState.Detached;
        }
    }
}
