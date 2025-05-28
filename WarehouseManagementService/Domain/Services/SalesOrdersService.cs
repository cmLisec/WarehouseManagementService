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
        public async Task<CommonResponseType<List<SalesOrderReadDto>>> GetAllAsync()
        {
            var SalesOrders = await _repo.GetAllAsync();
            if (SalesOrders.Count == 0)
                return new CommonResponseType<List<SalesOrderReadDto>>("No Sales orders available", StatusCodes.Status204NoContent);

            return new CommonResponseType<List<SalesOrderReadDto>>(_mapper.Map<List<SalesOrderReadDto>>(SalesOrders), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<SalesOrderReadDto>> GetByIdAsync(int id)
        {
            var SalesOrder = await _repo.GetByIdAsync(id);
            if (SalesOrder == null)
                return new CommonResponseType<SalesOrderReadDto>("Order with the given Id not found", StatusCodes.Status404NotFound);

            return new CommonResponseType<SalesOrderReadDto>(_mapper.Map<SalesOrderReadDto>(SalesOrder), StatusCodes.Status200OK);
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

        public async Task<CommonResponseType<SalesOrderReadDto>> CreateAsync(SalesOrderDto dto)
        {
            var validateResponse = Validate(dto);
            if (!validateResponse.IsSuccessStatusCode())
                return new CommonResponseType<SalesOrderReadDto>(validateResponse.Message, validateResponse.StatusCode);

            var order = _mapper.Map<SalesOrder>(dto);
            _repo.Add(order);
            await _repo.SaveAsync();
            return new CommonResponseType<SalesOrderReadDto>(_mapper.Map<SalesOrderReadDto>(order), StatusCodes.Status201Created);
        }

        public async Task<CommonResponseType<SalesOrderReadDto>> UpdateAsync(int id, SalesOrderDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return new CommonResponseType<SalesOrderReadDto>("Order with the given Id not found", StatusCodes.Status404NotFound);

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
            return new CommonResponseType<SalesOrderReadDto>(_mapper.Map<SalesOrderReadDto>(existing), StatusCodes.Status200OK);
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
