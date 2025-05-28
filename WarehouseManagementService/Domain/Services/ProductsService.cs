using AutoMapper;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Models;
using WarehouseManagementService.Domain.Repositories;
using WarehouseManagementService.Domain.Utilities;

namespace WarehouseManagementService.Domain.Services
{
    public class ProductsService
    {
        private readonly ProductsRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsService> _logger;

        public ProductsService(ProductsRepository repo, IMapper mapper, ILogger<ProductsService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<CommonResponseType<List<GetProductDto>>> GetAllProductsAsync()
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var products = await _repo.GetAllProductsAsync();
                if (products.Count == 0)
                    return new CommonResponseType<List<GetProductDto>>("No products available", StatusCodes.Status204NoContent);

                return new CommonResponseType<List<GetProductDto>>(_mapper.Map<List<GetProductDto>>(products), StatusCodes.Status200OK);
            }, _logger, nameof(GetAllProductsAsync));
        }

        public Task<CommonResponseType<GetProductDto>> GetProductByIdAsync(int id)
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var product = await _repo.GetProductByIdAsync(id);
                if (product == null)
                    return new CommonResponseType<GetProductDto>("Product with the given Id not found", StatusCodes.Status404NotFound);

                return new CommonResponseType<GetProductDto>(_mapper.Map<GetProductDto>(product), StatusCodes.Status200OK);
            }, _logger, nameof(GetProductByIdAsync));
        }

        public Task<CommonResponseType<GetProductDto>> CreateProductAsync(ProductDto dto)
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var product = _mapper.Map<Product>(dto);
                var productAdded = await _repo.CreateProductAsync(product);
                await _repo.SaveChangesAsync();

                return new CommonResponseType<GetProductDto>(_mapper.Map<GetProductDto>(productAdded), StatusCodes.Status201Created);
            }, _logger, nameof(CreateProductAsync));
        }

        public Task<CommonResponseType<GetProductDto>> UpdateProductAsync(int id, ProductDto dto)
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var product = await _repo.GetProductByIdAsync(id);
                if (product == null)
                    return new CommonResponseType<GetProductDto>("Product with the given Id not found", StatusCodes.Status404NotFound);

                var productToUpdate = _mapper.Map<Product>(dto);
                productToUpdate.ProductId = id;

                await _repo.UpdateProductAsync(productToUpdate);
                await _repo.SaveChangesAsync();

                return new CommonResponseType<GetProductDto>(_mapper.Map<GetProductDto>(productToUpdate), StatusCodes.Status200OK);
            }, _logger, nameof(UpdateProductAsync));
        }

        public Task<CommonResponseType<GetProductDto>> DeleteProductAsync(int id)
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var product = await _repo.GetProductByIdAsync(id);
                if (product == null)
                    return new CommonResponseType<GetProductDto>("Product with the given Id not found", StatusCodes.Status404NotFound);

                await _repo.DeleteProductAsync(product);
                await _repo.SaveChangesAsync();

                return new CommonResponseType<GetProductDto>();
            }, _logger, nameof(DeleteProductAsync));
        }
    }
}
