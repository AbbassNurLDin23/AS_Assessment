using AS_Assessment.DTOs;
using AS_Assessment.Enums;

namespace AS_Assessment.Services.Interface
{
    public interface IInventoryItemService
    {
        Task<IEnumerable<InventoryItemReadDto>> GetAllWithCategoryAsync();
        Task<IEnumerable<InventoryItemReadDto>> GetByCategoryAsync(string categoryId);
        Task<IEnumerable<InventoryItemReadDto>> GetByUserAsync(string userId);
        Task<IEnumerable<InventoryItemReadDto>> GetByUserAndCategoryAsync(string userId, string categoryId);
        Task<IEnumerable<InventoryItemReadDto>> GetByStockStatusAsync(StockStatus status);
        Task<IEnumerable<InventoryItemReadDto>> GetByStockStatusAndUserAsync(StockStatus status, string userId);
        Task<InventoryItemReadDto> GetByIdAsync(string id);
        Task<InventoryItemReadDto> CreateAsync(InventoryItemCreateDto dto);
        Task<InventoryItemReadDto> UpdateAsync(InventoryItemUpdateDto dto);
        Task<bool> DeleteAsync(string id);
    }
}
