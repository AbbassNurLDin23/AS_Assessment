using AS_Assessment.Models;

namespace AS_Assessment.Repositories.Interface
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAvailableCategoriesAsync();
        Task<IEnumerable<Category>> GetDeletedCategoriesAsync();
    }
}
