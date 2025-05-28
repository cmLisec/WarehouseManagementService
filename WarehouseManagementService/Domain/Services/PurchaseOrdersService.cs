using AutoMapper;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Models;
using WarehouseManagementService.Domain.Repositories;
using WarehouseManagementService.Domain.Utilities;

namespace WarehouseManagementService.Domain.Services
{
    public class PurchaseOrdersService
    {
        private readonly PurchaseOrdersRepository _repo;
        private readonly IMapper _mapper;

        public PurchaseOrdersService(PurchaseOrdersRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<CommonResponseType<List<GetPurchaseOrderDto>>> GetAllPurchaseOrdersAsync()
        {
            var purchaseOrders = await _repo.GetAllPurchaseOrdersAsync();
            if (purchaseOrders.Count == 0)
                return new CommonResponseType<List<GetPurchaseOrderDto>>("No purchase orders available", StatusCodes.Status204NoContent);

            return new CommonResponseType<List<GetPurchaseOrderDto>>(_mapper.Map<List<GetPurchaseOrderDto>>(purchaseOrders), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<GetPurchaseOrderDto>> GetPurchaseOrderByIdAsync(int id)
        {
            var purchaseOrder = await _repo.GetPurchaseOrderByIdAsync(id);
            if (purchaseOrder == null)
                return new CommonResponseType<GetPurchaseOrderDto>("Order with the given Id not found", StatusCodes.Status404NotFound);

            return new CommonResponseType<GetPurchaseOrderDto>(_mapper.Map<GetPurchaseOrderDto>(purchaseOrder), StatusCodes.Status200OK);
        }

        public CommonResponseType<PurchaseOrderDto> Validate(PurchaseOrderDto dto)
        {
            if (!_repo.CustomerExists(dto.CustomerId))
                return new CommonResponseType<PurchaseOrderDto>("Customer does not exist", StatusCodes.Status424FailedDependency);

            var productIds = dto.Items.Select(i => i.ProductId).Distinct();
            var missingIds = _repo.ProductsExists(productIds);

            if (missingIds.Count != 0)
                return new CommonResponseType<PurchaseOrderDto>($"Product(s) not found: {string.Join(", ", missingIds)}", StatusCodes.Status424FailedDependency);

            return new CommonResponseType<PurchaseOrderDto>();
        }

        public async Task<CommonResponseType<GetPurchaseOrderDto>> CreatePurchaseOrderAsync(PurchaseOrderDto dto)
        {
            var validateResponse = Validate(dto);
            if (!validateResponse.IsSuccessStatusCode())
                return new CommonResponseType<GetPurchaseOrderDto>(validateResponse.Message, validateResponse.StatusCode);

            var order = _mapper.Map<PurchaseOrder>(dto);
            _repo.CreatePurchaseOrderAsync(order);
            await _repo.SaveAsync();
            return new CommonResponseType<GetPurchaseOrderDto>(_mapper.Map<GetPurchaseOrderDto>(order), StatusCodes.Status201Created);
        }

        public async Task<CommonResponseType<GetPurchaseOrderDto>> UpdatePurchaseOrderAsync(int id, PurchaseOrderDto dto)
        {
            var existing = await _repo.GetPurchaseOrderByIdAsync(id);
            if (existing == null)
                return new CommonResponseType<GetPurchaseOrderDto>("Order with the given Id not found", StatusCodes.Status404NotFound);

            // Clear existing items and add new
            existing.Items.Clear();
            foreach (var itemDto in dto.Items)
            {
                var item = _mapper.Map<PurchaseOrderItem>(itemDto);
                existing.Items.Add(item);
            }

            existing.ProcessingDate = dto.ProcessingDate;
            existing.CustomerId = dto.CustomerId;

            await _repo.SaveAsync();
            return new CommonResponseType<GetPurchaseOrderDto>(_mapper.Map<GetPurchaseOrderDto>(existing), StatusCodes.Status200OK);
        }

        public async Task<CommonResponseType<PurchaseOrderDto>> DeletePurchaseOrderAsync(int id)
        {
            var existing = await _repo.GetPurchaseOrderByIdAsync(id);
            if (existing == null) new CommonResponseType<GetProductDto>("Order with the given Id not found", StatusCodes.Status404NotFound); ;

            _repo.DeletePurchaseOrderAsync(existing);
            await _repo.SaveAsync();
            return new CommonResponseType<PurchaseOrderDto>();
        }
    }
}
