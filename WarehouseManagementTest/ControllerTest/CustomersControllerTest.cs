using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementService.Controllers;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Models;
using WarehouseManagementService.Domain.Repositories;
using WarehouseManagementService.Domain.Services;
using WarehouseManagementTest.TestData;

namespace WarehouseManagementService.Tests.Controllers
{
    [TestFixture]
    public class CustomersControllerTest : WarehouseManagementTestSetUp
    {
        private CustomersController _controller;

        [SetUp]
        public void Setup()
        {
            InitialiseSetup();
            var repo = new CustomersRepository(_context);
            var service = new CustomersService(repo, _mapper);
            _controller = new CustomersController(service);
        }

        [Test]
        public async Task GetAllCustomers_ReturnsEmptyList_WhenNoCustomersExist()
        {
            var result = await _controller.GetAllCustomers();

            Assert.That(result.Result, Is.TypeOf<NoContentResult>());
            var objectResult = result.Result as NoContentResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public async Task CreateCustomer_ReturnsCreatedCustomer()
        {
            var dto = new BaseCustomerDto
            {
                Name = "Test Customer",
                Address = "123 Street"
            };

            var result = await _controller.CreateCustomer(dto);

            Assert.That(result.Result, Is.TypeOf<ObjectResult>());
            var objectResult = result.Result as ObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo(201));

            var customer = objectResult.Value as CustomerDto;
            Assert.That(customer.Name, Is.EqualTo(dto.Name));
        }

        [Test]
        public async Task GetCustomer_ReturnsCustomer_WhenExists()
        {
            var customer = new Customer { Name = "John Doe", Address = "456 Lane" };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            var result = await _controller.GetCustomer(customer.CustomerId);

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var objectResult = result.Result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task UpdateCustomer_ReturnsUpdatedCustomer()
        {
            var customer = new Customer { Name = "Old Name", Address = "Old Address" };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            var updatedDto = new BaseCustomerDto
            {
                Name = "New Name",
                Address = "New Address"
            };
            // DETACH to avoid tracking conflict
            _context.Entry(customer).State = EntityState.Detached;
            var result = await _controller.UpdateCustomer(customer.CustomerId, updatedDto);

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var objectResult = result.Result as OkObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo(200));

            var updatedCustomer = objectResult.Value as CustomerDto;
            Assert.That(updatedCustomer.Name, Is.EqualTo(updatedDto.Name));
        }

        [Test]
        public async Task DeleteCustomer_ReturnsSuccess_WhenCustomerExists()
        {
            var customer = new Customer { Name = "To Delete", Address = "Somewhere" };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            // DETACH to avoid tracking conflict
            _context.Entry(customer).State = EntityState.Detached;
            var result = await _controller.DeleteCustomer(customer.CustomerId);

            Assert.That(result.Result, Is.TypeOf<OkResult>());
            var objectResult = result.Result as OkResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo(200));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
