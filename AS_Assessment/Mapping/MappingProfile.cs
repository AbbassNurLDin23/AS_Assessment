using AS_Assessment.DTOs;
using AS_Assessment.Models;
using AutoMapper;

namespace AS_Assessment.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InventoryItem, InventoryItemReadDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<InventoryItemCreateDto, InventoryItem>();
            CreateMap<InventoryItemUpdateDto, InventoryItem>();

            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();
            CreateMap<Category, CategoryResponseDTO>();
        }
    }
}
