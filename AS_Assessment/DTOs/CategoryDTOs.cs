using System.ComponentModel.DataAnnotations;

namespace AS_Assessment.DTOs
{
    public class CreateCategoryDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }

    public class UpdateCategoryDTO
    {
        [MaxLength(100)]
        public string? Name { get; set; }
    }

    public class CategoryResponseDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
