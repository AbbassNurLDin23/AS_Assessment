using AS_Assessment.DTOs;
using AS_Assessment.Enums;
using AS_Assessment.Models;
using AS_Assessment.Repositories.Interface;
using AS_Assessment.Services.Interface;
using AutoMapper;

namespace AS_Assessment.Services.Implementation
{
    public class InventoryItemService : IInventoryItemService
    {
        private readonly IInventoryItemRepository _inventoryRepo;
        private readonly IMapper _mapper;

        public InventoryItemService(IInventoryItemRepository inventoryRepo, IMapper mapper)
        {
            _inventoryRepo = inventoryRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryItemReadDto>> GetAllWithCategoryAsync()
        {
            var items = await _inventoryRepo.GetAllWithCategoryAsync();
            return _mapper.Map<IEnumerable<InventoryItemReadDto>>(items);
        }

        public async Task<IEnumerable<InventoryItemReadDto>> GetByCategoryAsync(string categoryId)
        {
            var items = await _inventoryRepo.GetByCategoryAsync(categoryId);
            return _mapper.Map<IEnumerable<InventoryItemReadDto>>(items);
        }

        public async Task<IEnumerable<InventoryItemReadDto>> GetByUserAsync(string userId)
        {
            var items = await _inventoryRepo.GetByUserAsync(userId);
            return _mapper.Map<IEnumerable<InventoryItemReadDto>>(items);
        }

        public async Task<IEnumerable<InventoryItemReadDto>> GetByUserAndCategoryAsync(string userId, string categoryId)
        {
            var items = await _inventoryRepo.GetByUserAndCategoryAsync(userId, categoryId);
            return _mapper.Map<IEnumerable<InventoryItemReadDto>>(items);
        }

        public async Task<IEnumerable<InventoryItemReadDto>> GetByStockStatusAsync(StockStatus status)
        {
            var items = await _inventoryRepo.GetByStockStatusAsync(status);
            return _mapper.Map<IEnumerable<InventoryItemReadDto>>(items);
        }

        public async Task<IEnumerable<InventoryItemReadDto>> GetByStockStatusAndUserAsync(StockStatus status, string userId)
        {
            var items = await _inventoryRepo.GetByStockStatusAndUserAsync(status, userId);
            return _mapper.Map<IEnumerable<InventoryItemReadDto>>(items);
        }

        public async Task<InventoryItemReadDto> GetByIdAsync(string id)
        {
            var item = await _inventoryRepo.GetByIdWithCategoryAsync(id);
            return _mapper.Map<InventoryItemReadDto>(item);
        }

        public async Task<InventoryItemReadDto> CreateAsync(InventoryItemCreateDto dto)
        {
            var entity = _mapper.Map<InventoryItem>(dto);
            await _inventoryRepo.AddAsync(entity);
            return _mapper.Map<InventoryItemReadDto>(entity);
        }

        public async Task<InventoryItemReadDto> UpdateAsync(InventoryItemUpdateDto dto)
        {
            var existing = await _inventoryRepo.GetByIdWithCategoryAsync(dto.Id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            await _inventoryRepo.UpdateAsync(existing);
            return _mapper.Map<InventoryItemReadDto>(existing);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existing = await _inventoryRepo.GetByIdWithCategoryAsync(id);
            if (existing == null) return false;

            await _inventoryRepo.RemoveAsync(existing);
            return true;
        }
    }
}
