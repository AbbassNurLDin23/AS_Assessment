using AS_Assessment.Enums;
using AS_Assessment.Models;

namespace AS_Assessment.Repositories.Interface
{
    public interface IInventoryItemRepository : IGenericRepository<InventoryItem>
    {
        Task<IEnumerable<InventoryItem>> GetAllWithCategoryAsync();
        Task<IEnumerable<InventoryItem>> GetByCategoryAsync(string categoryId);
        Task<IEnumerable<InventoryItem>> GetByUserAsync(string userId);
        Task<IEnumerable<InventoryItem>> GetByUserAndCategoryAsync(string userId, string categoryId);
        Task<IEnumerable<InventoryItem>> GetByStockStatusAsync(StockStatus status);
        Task<IEnumerable<InventoryItem>> GetByStockStatusAndUserAsync(StockStatus status, string userId);
        Task<InventoryItem> GetByIdWithCategoryAsync(string id);
    }
}
