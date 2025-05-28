using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementService.Controllers;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Repositories;
using WarehouseManagementService.Domain.Services;
using WarehouseManagementTest.TestData;

namespace WarehouseManagementTest.ControllerTest
{
    public class SalesOrdersControllerTest : WarehouseManagementTestSetUp
    {
        private SalesOrdersController _controller;

        [SetUp]
        public void Setup()
        {
            InitialiseSetup();
            var repo = new SalesOrdersRepository(_context);
            var service = new SalesOrdersService(repo, _mapper);
            _controller = new SalesOrdersController(service);
        }
        [Test]
        public async Task CreateSalesOrder_ShouldReturnCreatedSalesOrder()
        {
            await AddRequiredTestData();
            var dto = new SalesOrderDto
            {
                CustomerId = 1,
                Items = new List<SalesOrderItemDto>()
                {
                    new SalesOrderItemDto()
                    {
                        ProductId = 1,
                        Quantity = 10
                    }
                },
                ProcessingDate = DateTime.Now,
                ShipmentAddress = "Dubai"
            };

            var result = await _controller.CreateSalesOrder(dto);

            Assert.That(result.Result, Is.TypeOf<ObjectResult>());
            var objectResult = result.Result as ObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo(201));

            var SalesOrder = objectResult.Value as SalesOrderDto;
            Assert.That(SalesOrder.CustomerId, Is.EqualTo(dto.CustomerId));
        }

        [Test]
        public async Task GetAllSalesOrders_ShouldReturnList()
        {
            await AddRequiredTestData();

            var result = await _controller.GetAllSalesOrders();
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as List<SalesOrderDto>;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetSalesOrderById_ShouldReturnSalesOrder()
        {
            await AddRequiredTestData();

            var result = await _controller.GetSalesOrder(1);
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as SalesOrderDto;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(response?.CustomerId, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateSalesOrder_ShouldReturnUpdatedSalesOrder()
        {
            await AddRequiredTestData();
            var dto = new SalesOrderDto
            {
                CustomerId = 1,
                Items = new List<SalesOrderItemDto>()
                {
                    new SalesOrderItemDto()
                    {
                        ProductId = 1,
                        Quantity = 101
                    }
                }
            };

            var result = await _controller.UpdateSalesOrder(1, dto);
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as SalesOrderDto;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(response?.CustomerId, Is.EqualTo(1));
        }

        [Test]
        public async Task DeleteSalesOrder_ShouldReturnSuccess()
        {
            await AddRequiredTestData();

            var result = await _controller.DeleteSalesOrder(1);
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
