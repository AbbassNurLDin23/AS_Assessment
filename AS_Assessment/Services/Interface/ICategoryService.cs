using AS_Assessment.DTOs;

namespace AS_Assessment.Services.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDTO>> GetAllAsync();
        Task<CategoryResponseDTO?> GetByIdAsync(string id);
        Task<CategoryResponseDTO> AddAsync(CreateCategoryDTO dto);
        Task<CategoryResponseDTO?> UpdateAsync(string id, UpdateCategoryDTO dto);
        Task<bool> SoftDeleteAsync(string id);
    }
}
