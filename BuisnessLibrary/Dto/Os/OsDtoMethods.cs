using DomainLibrary.Entities;

namespace BuisnessLibrary.Dto.Os
{
    public static class OsDtoMethods
    {
        public static IEnumerable<OsDto> convertOssToListOfOssDto(IEnumerable<TbO> listOs)
        {
            if (listOs.Any())
            {


                return listOs.Select(os => new OsDto
                {
                    OsId = os.OsId,
                    CurrentState = os.CurrentState,
                    OsName = os.OsName,
                    ImageName = os.ImageName
                });
            }
            return null;

        }
        public static OsDto convertOsToOsDto(TbO category)
        {
            if (category != null)
            {

                OsDto categoryDto = new OsDto()
                {
                    OsId = category.OsId,
                    CurrentState = category.CurrentState,
                    OsName = category.OsName,
                    ImageName = category.ImageName
                };

                return categoryDto;
            }
            return null;

        }
    }

}
