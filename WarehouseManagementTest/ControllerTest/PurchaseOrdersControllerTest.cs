using Microsoft.AspNetCore.Mvc;
using WarehouseManagementService.Controllers;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Repositories;
using WarehouseManagementService.Domain.Services;
using WarehouseManagementTest.TestData;

namespace WarehouseManagementTest.ControllerTest
{
    public class PurchaseOrdersControllerTest : WarehouseManagementTestSetUp
    {
        private PurchaseOrdersController _controller;

        [SetUp]
        public void Setup()
        {
            InitialiseSetup();
            var repo = new PurchaseOrdersRepository(_context);
            var service = new PurchaseOrdersService(repo, _mapper, CreateLoggerInstance<PurchaseOrdersService>());
            _controller = new PurchaseOrdersController(service);
        }
        [Test]
        public async Task CreatePurchaseOrder_ShouldReturnCreatedPurchaseOrder()
        {
            await AddRequiredTestData();
            var dto = new PurchaseOrderDto
            {
                CustomerId = 1,
                Items = new List<PurchaseOrderItemDto>()
                {
                    new PurchaseOrderItemDto()
                    {
                        ProductId = 1,
                        Quantity = 10
                    }
                }
            };

            var result = await _controller.CreatePurchaseOrderAsync(dto);

            Assert.That(result.Result, Is.TypeOf<ObjectResult>());
            var objectResult = result.Result as ObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo(201));

            var PurchaseOrder = objectResult.Value as PurchaseOrderDto;
            Assert.That(PurchaseOrder.CustomerId, Is.EqualTo(dto.CustomerId));
        }

        [Test]
        public async Task GetAllPurchaseOrders_ShouldReturnList()
        {
            await AddRequiredTestData();

            var result = await _controller.GetAllPurchaseOrdersAsync();
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as List<PurchaseOrderDto>;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetPurchaseOrderById_ShouldReturnPurchaseOrder()
        {
            await AddRequiredTestData();

            var result = await _controller.GetPurchaseOrderByIdAsync(1);
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as PurchaseOrderDto;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(response?.CustomerId, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdatePurchaseOrder_ShouldReturnUpdatedPurchaseOrder()
        {
            await AddRequiredTestData();
            var dto = new PurchaseOrderDto
            {
                CustomerId = 1,
                Items = new List<PurchaseOrderItemDto>()
                {
                    new PurchaseOrderItemDto()
                    {
                        ProductId = 1,
                        Quantity = 101
                    }
                }
            };

            var result = await _controller.UpdatePurchaseOrderAsync(1, dto);
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as PurchaseOrderDto;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(response?.CustomerId, Is.EqualTo(1));
        }

        [Test]
        public async Task DeletePurchaseOrder_ShouldReturnSuccess()
        {
            await AddRequiredTestData();

            var result = await _controller.DeletePurchaseOrderAsync(1);
            var okResult = result.Result as OkResult;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
        }


        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
