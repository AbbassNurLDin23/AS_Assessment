    using AS_Assessment.Data;
using AS_Assessment.Enums;
using AS_Assessment.Models;
using AS_Assessment.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AS_Assessment.Repositories.Implementation
{
    public class InventoryItemRepository : GenericRepository<InventoryItem>, IInventoryItemRepository
    {
        public InventoryItemRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<InventoryItem>> GetAllWithCategoryAsync()
        {
            return await _dbSet.Include(i => i.Category)
                               .Where(i => i.Category != null && !i.Category.IsDeleted)
                               .ToListAsync();
        }

        public async Task<IEnumerable<InventoryItem>> GetByCategoryAsync(string categoryId)
        {
            return await _dbSet.Include(i => i.Category)
                               .Where(i => i.CategoryId == categoryId && i.Category != null && !i.Category.IsDeleted)
                               .ToListAsync();
        }

        public async Task<IEnumerable<InventoryItem>> GetByUserAsync(string userId)
        {
            return await _dbSet.Include(i => i.Category)
                               .Where(i => i.UserId == userId && i.Category != null && !i.Category.IsDeleted)
                               .ToListAsync();
        }

        public async Task<IEnumerable<InventoryItem>> GetByUserAndCategoryAsync(string userId, string categoryId)
        {
            return await _dbSet.Include(i => i.Category)
                               .Where(i => i.UserId == userId && i.CategoryId == categoryId && i.Category != null && !i.Category.IsDeleted)
                               .ToListAsync();
        }

        public async Task<IEnumerable<InventoryItem>> GetByStockStatusAsync(StockStatus status)
        {
            return await _dbSet.Include(i => i.Category)
                               .Where(i => i.StockStatus == status && i.Category != null && !i.Category.IsDeleted)
                               .ToListAsync();
        }

        public async Task<IEnumerable<InventoryItem>> GetByStockStatusAndUserAsync(StockStatus status, string userId)
        {
            return await _dbSet.Include(i => i.Category)
                               .Where(i => i.StockStatus == status && i.UserId == userId && i.Category != null && !i.Category.IsDeleted)
                               .ToListAsync();
        }

        public async Task<InventoryItem> GetByIdWithCategoryAsync(string id)
        {
            return await _dbSet.Include(i => i.Category)
                               .FirstOrDefaultAsync(i => i.Id == id && i.Category != null && !i.Category.IsDeleted);
        }
    }
}
