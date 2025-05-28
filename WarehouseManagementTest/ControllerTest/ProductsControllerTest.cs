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
    public class ProductsControllerTests : WarehouseManagementTestSetUp
    {
        private ProductsController _controller;

        [SetUp]
        public void Setup()
        {
            InitialiseSetup();
            var repo = new ProductsRepository(_context);
            var service = new ProductsService(repo, _mapper, CreateLoggerInstance<ProductsService>());
            _controller = new ProductsController(service);
        }

        [Test]
        public async Task CreateProduct_ShouldReturnCreatedProduct()
        {
            var dto = new ProductDto
            {
                Description = "product1",
                Dimensions = "100*200",
                ProductCode = "p1",
                Title = "new"
            };

            var result = await _controller.CreateProductAsync(dto);

            Assert.That(result.Result, Is.TypeOf<ObjectResult>());
            var objectResult = result.Result as ObjectResult;
            Assert.That(objectResult.StatusCode, Is.EqualTo(201));

            var product = objectResult.Value as GetProductDto;
            Assert.That(product.ProductCode, Is.EqualTo(dto.ProductCode));
        }

        [Test]
        public async Task GetAllProducts_ShouldReturnList()
        {
            _context.Products.Add(new Product
            {
                Description = "product1",
                Dimensions = "100*200",
                ProductCode = "p1",
                Title = "new"
            });
            await _context.SaveChangesAsync();

            var result = await _controller.GetAllProductsAsync();
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as List<GetProductDto>;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(response, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task GetProductById_ShouldReturnProduct()
        {
            _context.Products.Add(new Product
            {
                Description = "product1",
                Dimensions = "100*200",
                ProductCode = "p1",
                Title = "new"
            });
            await _context.SaveChangesAsync();

            var result = await _controller.GetProductByIdAsync(1);
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as GetProductDto;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(response?.ProductCode, Is.EqualTo("p1"));
        }

        [Test]
        public async Task UpdateProduct_ShouldReturnUpdatedProduct()
        {
            var product = new Product
            {
                Description = "product1",
                Dimensions = "100*200",
                ProductCode = "p1",
                Title = "new"
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Detach to prevent multiple tracking issues
            _context.Entry(product).State = EntityState.Detached;

            var dto = new ProductDto
            {
                Description = "product1",
                Dimensions = "100*200",
                ProductCode = "p1",
                Title = "new-updated"
            };

            var result = await _controller.UpdateProductAsync(product.ProductId, dto);
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as GetProductDto;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(response?.Title, Is.EqualTo(dto.Title));
        }

        [Test]
        public async Task DeleteProduct_ShouldReturnSuccess()
        {
            var product = new Product
            {
                Description = "product1",
                Dimensions = "100*200",
                ProductCode = "p1",
                Title = "new"
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Detach to prevent multiple tracking issues
            _context.Entry(product).State = EntityState.Detached;

            var result = await _controller.DeleteProductAsync(product.ProductId);
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
