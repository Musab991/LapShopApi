using DomainLibrary.Entities;

namespace BuisnessLibrary.Dto.ItemType
{
    public static class ItemTypeMapper
    {
        public static IEnumerable<ItemTypeDto> convertItemTypesToListOfItemTypesDto(IEnumerable<TbItemType> listItemTypes)
        {
            if (listItemTypes.Any())
            {


                return listItemTypes.Select(itType => new ItemTypeDto
                {
                    ItemTypeId = itType.ItemTypeId,
                    CurrentState = itType.CurrentState,
                    ItemTypeName = itType.ItemTypeName,
                    ImageName = itType.ImageName
                });
            }
            return null;

        }
        public static ItemTypeDto convertItemTypeToItemTypeDto(TbItemType category)
        {
            if (category != null)
            {

                ItemTypeDto categoryDto = new ItemTypeDto()
                {
                    ItemTypeId = category.ItemTypeId,
                    CurrentState = category.CurrentState,
                    ItemTypeName = category.ItemTypeName,
                    ImageName = category.ImageName
                };

                return categoryDto;
            }
            return null;

        }
    }

}
