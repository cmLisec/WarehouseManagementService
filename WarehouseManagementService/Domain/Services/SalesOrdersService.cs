using AutoMapper;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Models;
using WarehouseManagementService.Domain.Repositories;
using WarehouseManagementService.Domain.Utilities;

namespace WarehouseManagementService.Domain.Services
{
    public class SalesOrdersService
    {
        private readonly SalesOrdersRepository _repo;
        private readonly IMapper _mapper;

        public SalesOrdersService(SalesOrdersRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<CommonResponseType<List<GetSalesOrderDto>>> GetAllAsync()
        {
            var SalesOrders = await _repo.GetAllAsync();
            if (SalesOrders.Count == 0)
                return new CommonResponseType<List<GetSalesOrderDto>>("No Sales orders available", StatusCodes.Status204NoContent);

            return new CommonResponseType<List<GetSalesOrderDto>>(_mapper.Map<List<GetSalesOrderDto>>(SalesOrders), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<GetSalesOrderDto>> GetByIdAsync(int id)
        {
            var SalesOrder = await _repo.GetByIdAsync(id);
            if (SalesOrder == null)
                return new CommonResponseType<GetSalesOrderDto>("Order with the given Id not found", StatusCodes.Status404NotFound);

            return new CommonResponseType<GetSalesOrderDto>(_mapper.Map<GetSalesOrderDto>(SalesOrder), StatusCodes.Status200OK);
        }

        public CommonResponseType<SalesOrderDto> Validate(SalesOrderDto dto)
        {
            if (!_repo.CustomerExists(dto.CustomerId))
                return new CommonResponseType<SalesOrderDto>("Customer does not exist", StatusCodes.Status424FailedDependency);

            var productIds = dto.Items.Select(i => i.ProductId).Distinct();
            var missingIds = _repo.ProductsExists(productIds);

            if (missingIds.Count != 0)
                return new CommonResponseType<SalesOrderDto>($"Product(s) not found: {string.Join(", ", missingIds)}", StatusCodes.Status424FailedDependency);

            return new CommonResponseType<SalesOrderDto>();
        }

        public async Task<CommonResponseType<GetSalesOrderDto>> CreateAsync(SalesOrderDto dto)
        {
            var validateResponse = Validate(dto);
            if (!validateResponse.IsSuccessStatusCode())
                return new CommonResponseType<GetSalesOrderDto>(validateResponse.Message, validateResponse.StatusCode);

            var order = _mapper.Map<SalesOrder>(dto);
            _repo.Add(order);
            await _repo.SaveAsync();
            return new CommonResponseType<GetSalesOrderDto>(_mapper.Map<GetSalesOrderDto>(order), StatusCodes.Status201Created);
        }

        public async Task<CommonResponseType<GetSalesOrderDto>> UpdateAsync(int id, SalesOrderDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return new CommonResponseType<GetSalesOrderDto>("Order with the given Id not found", StatusCodes.Status404NotFound);

            // Clear existing items and add new
            existing.Items.Clear();
            foreach (var itemDto in dto.Items)
            {
                var item = _mapper.Map<SalesOrderItem>(itemDto);
                existing.Items.Add(item);
            }

            existing.ProcessingDate = dto.ProcessingDate;
            existing.CustomerId = dto.CustomerId;

            await _repo.SaveAsync();
            return new CommonResponseType<GetSalesOrderDto>(_mapper.Map<GetSalesOrderDto>(existing), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<SalesOrderDto>> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return new CommonResponseType<SalesOrderDto>("Order with the given Id not found", StatusCodes.Status404NotFound); ;

            _repo.Remove(existing);
            await _repo.SaveAsync();
            return new CommonResponseType<SalesOrderDto>();
        }
    }
}
