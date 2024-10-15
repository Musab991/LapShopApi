using BuisnessLibrary.Dto.Item;
using BuisnessLibrary.Dto.Category; 
using BuisnessLibrary.Dto.ItemType;
using BuisnessLibrary.Dto.Os;

namespace LapShop.Api.AutoMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping for Item
            CreateMap<ItemAddDto, TbItem>()
                .ForMember(dest => dest.ItemId, opt => opt.Ignore())
                .ForMember(dest => dest.TbItemImages, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now)) // Set CreatedDate to now
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.DateDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

            CreateMap<ItemUpdateDto, TbItem>()
                .ForMember(dest => dest.TbItemImages, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DateDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

            // Mapping for category

            CreateMap<CategoryAddDto, TbCategory>()
                  .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
                  .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now)) // Set CreatedDate to now
                  .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                  .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());

            CreateMap<CategoryUpdateDto, TbCategory>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now)) // Set UpdatedDate to now
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            
            // Mapping for ItemType

            CreateMap<ItemTypeAddDto, TbItemType>()
                  .ForMember(dest => dest.ItemTypeId, opt => opt.Ignore())
                  .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now)) // Set CreatedDate to now
                  .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                  .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());

            CreateMap<ItemTypeUpdateDto, TbItemType>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now)) // Set UpdatedDate to now
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
        
            // Mapping for Os

            CreateMap<OsAddDto, TbO>()
                  .ForMember(dest => dest.OsId, opt => opt.Ignore())
                  .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now)) // Set CreatedDate to now
                  .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                  .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());

            CreateMap<OsUpdateDto, TbO>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now)) // Set UpdatedDate to now
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

        }
    }
}
