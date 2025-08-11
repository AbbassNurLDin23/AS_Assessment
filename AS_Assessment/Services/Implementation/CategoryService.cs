using AS_Assessment.DTOs;
using AS_Assessment.Models;
using AS_Assessment.Repositories.Interface;
using AS_Assessment.Services.Interface;
using AutoMapper;

namespace AS_Assessment.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponseDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAvailableCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryResponseDTO>>(categories);
        }

        public async Task<CategoryResponseDTO?> GetByIdAsync(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null || category.IsDeleted) return null;

            return _mapper.Map<CategoryResponseDTO>(category);
        }

        public async Task<CategoryResponseDTO> AddAsync(CreateCategoryDTO dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _categoryRepository.AddAsync(category);

            return _mapper.Map<CategoryResponseDTO>(category);
        }

        public async Task<CategoryResponseDTO?> UpdateAsync(string id, UpdateCategoryDTO dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null || category.IsDeleted) return null;

            if (!string.IsNullOrEmpty(dto.Name))
                category.Name = dto.Name;

            await _categoryRepository.UpdateAsync(category);
            return _mapper.Map<CategoryResponseDTO>(category);
        }

        public async Task<bool> SoftDeleteAsync(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return false;

            category.IsDeleted = true;
            await _categoryRepository.UpdateAsync(category);
            return true;
        }
    }
}
