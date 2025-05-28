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

        public ProductsService(ProductsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CommonResponseType<List<GetProductDto>>> GetAllAsync()
        {
            var products = await _repo.GetAllAsync();
            if (products.Count == 0)
                return new CommonResponseType<List<GetProductDto>>("No products available", StatusCodes.Status204NoContent);

            return new CommonResponseType<List<GetProductDto>>(_mapper.Map<List<GetProductDto>>(products), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<GetProductDto>> GetByIdAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                return new CommonResponseType<GetProductDto>("Product with the given Id not found", StatusCodes.Status404NotFound);

            return new CommonResponseType<GetProductDto>(_mapper.Map<GetProductDto>(product), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<GetProductDto>> CreateAsync(ProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            var productAdded = await _repo.AddAsync(product);
            await _repo.SaveChangesAsync();

            return new CommonResponseType<GetProductDto>(_mapper.Map<GetProductDto>(productAdded), StatusCodes.Status201Created);
        }

        public async Task<CommonResponseType<GetProductDto>> UpdateAsync(int id, ProductDto dto)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                return new CommonResponseType<GetProductDto>("Product with the given Id not found", StatusCodes.Status404NotFound);

            var productToUpdate = _mapper.Map<Product>(dto);
            productToUpdate.ProductId = id;

            await _repo.UpdateAsync(productToUpdate);
            await _repo.SaveChangesAsync();
            return new CommonResponseType<GetProductDto>(_mapper.Map<GetProductDto>(productToUpdate), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<GetProductDto>> DeleteAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                return new CommonResponseType<GetProductDto>("Product with the given Id not found", StatusCodes.Status404NotFound);

            await _repo.DeleteAsync(product);
            await _repo.SaveChangesAsync();
            return new CommonResponseType<GetProductDto>();
        }
    }
}
