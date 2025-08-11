using AS_Assessment.Data;
using AS_Assessment.Models;
using AS_Assessment.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AS_Assessment.Repositories.Implementation
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetAvailableCategoriesAsync()
        {
            return await _dbSet
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetDeletedCategoriesAsync()
        {
            return await _dbSet
                .Where(c => c.IsDeleted)
                .ToListAsync();
        }
    }
}
