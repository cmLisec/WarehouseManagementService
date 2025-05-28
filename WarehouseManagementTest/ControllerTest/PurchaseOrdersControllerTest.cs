using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementService.Controllers;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Models;
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
            var service = new PurchaseOrdersService(repo, _mapper);
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

            var result = await _controller.CreatePurchaseOrder(dto);

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

            var result = await _controller.GetAllPurchaseOrders();
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as List<PurchaseOrderDto>;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetPurchaseOrderById_ShouldReturnPurchaseOrder()
        {
            await AddRequiredTestData();

            var result = await _controller.GetPurchaseOrder(1);
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

            var result = await _controller.UpdatePurchaseOrder(1, dto);
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as PurchaseOrderDto;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(response?.CustomerId, Is.EqualTo(1));
        }

        [Test]
        public async Task DeletePurchaseOrder_ShouldReturnSuccess()
        {
            await AddRequiredTestData();

            var result = await _controller.DeletePurchaseOrder(1);
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
