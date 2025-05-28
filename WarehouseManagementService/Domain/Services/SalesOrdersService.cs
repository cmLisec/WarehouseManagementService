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
        private readonly ILogger<SalesOrdersService> _logger;

        public SalesOrdersService(SalesOrdersRepository repo, IMapper mapper, ILogger<SalesOrdersService> logger)
        {
            _repo = repo; _mapper = mapper; _logger = logger;
        }

        public Task<CommonResponseType<List<GetSalesOrderDto>>> GetAllSalesOrdersAsync()
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var salesOrders = await _repo.GetAllAsync();
                if (salesOrders.Count == 0)
                    return new CommonResponseType<List<GetSalesOrderDto>>("No Sales orders available", StatusCodes.Status204NoContent);

                return new CommonResponseType<List<GetSalesOrderDto>>(_mapper.Map<List<GetSalesOrderDto>>(salesOrders), StatusCodes.Status200OK);
            }, _logger, nameof(GetAllSalesOrdersAsync));
        }

        public Task<CommonResponseType<GetSalesOrderDto>> GetSalesOrderByIdAsync(int id)
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var salesOrder = await _repo.GetByIdAsync(id);
                if (salesOrder == null)
                    return new CommonResponseType<GetSalesOrderDto>("Order with the given Id not found", StatusCodes.Status404NotFound);

                return new CommonResponseType<GetSalesOrderDto>(_mapper.Map<GetSalesOrderDto>(salesOrder), StatusCodes.Status200OK);
            }, _logger, nameof(GetSalesOrderByIdAsync));
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

        public Task<CommonResponseType<GetSalesOrderDto>> CreateSalesOrderAsync(SalesOrderDto dto)
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var validateResponse = Validate(dto);
                if (!validateResponse.IsSuccessStatusCode())
                    return new CommonResponseType<GetSalesOrderDto>(validateResponse.Message, validateResponse.StatusCode);

                var order = _mapper.Map<SalesOrder>(dto);
                _repo.Add(order);
                await _repo.SaveAsync();

                return new CommonResponseType<GetSalesOrderDto>(_mapper.Map<GetSalesOrderDto>(order), StatusCodes.Status201Created);
            }, _logger, nameof(CreateSalesOrderAsync));
        }

        public Task<CommonResponseType<GetSalesOrderDto>> UpdateSalesOrderAsync(int id, SalesOrderDto dto)
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var existing = await _repo.GetByIdAsync(id);
                if (existing == null)
                    return new CommonResponseType<GetSalesOrderDto>("Order with the given Id not found", StatusCodes.Status404NotFound);

                existing.Items.Clear();
                foreach (var itemDto in dto.Items)
                    existing.Items.Add(_mapper.Map<SalesOrderItem>(itemDto));

                existing.ProcessingDate = dto.ProcessingDate;
                existing.CustomerId = dto.CustomerId;

                await _repo.SaveAsync();

                return new CommonResponseType<GetSalesOrderDto>(_mapper.Map<GetSalesOrderDto>(existing), StatusCodes.Status200OK);
            }, _logger, nameof(UpdateSalesOrderAsync));
        }

        public Task<CommonResponseType<SalesOrderDto>> DeleteSalesOrderAsync(int id)
        {
            return ServiceExecutor.TryExecuteAsync(async () =>
            {
                var existing = await _repo.GetByIdAsync(id);
                if (existing == null)
                    return new CommonResponseType<SalesOrderDto>("Order with the given Id not found", StatusCodes.Status404NotFound);

                _repo.Remove(existing);
                await _repo.SaveAsync();

                return new CommonResponseType<SalesOrderDto>();
            }, _logger, nameof(DeleteSalesOrderAsync));
        }
    }
}
