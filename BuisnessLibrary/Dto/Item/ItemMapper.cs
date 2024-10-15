using DomainLibrary.Entities;

namespace BuisnessLibrary.Dto.Item
{
    public static class ItemMapper
    {
        public static IEnumerable<ItemDto> convertItemsToListOfItemsDto(IEnumerable<TbItem> listItems)
        {
            if (listItems.Any())
            {


                return listItems.Select(item => new ItemDto
                {
                    CategoryId = item.CategoryId,
                    CurrentState = item.CurrentState,
                    GpuName = item.Gpu.GpuName,
                    HardDiskName = item.HardDisk.HardDiskName,
                    ImageName = item.ImageName,
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    ItemTypeId = item.ItemTypeId,
                    OsName = item.Os.OsName,
                    ProcessorName = item.Processor.ProcessorName,
                    PurchasePrice = item.PurchasePrice,
                    Ram = Convert.ToInt32(item.Ram.RamSize),
                    SalesPrice = Convert.ToInt32(item.SalesPrice),
                    ScreenResolution = item.ScreenResolution.ScreenResolutionName,
                    ScreenSize = item.ScreenSize,
                    Weight = item.Weight,
                    Description=item.Description,
                    listImages = item.TbItemImages.Select(iti => iti.ImageName).ToList()
                });
            }
            return null;

        }
        public static ItemDto convertItemToItemDto(TbItem item)
        {
            if (item != null)
            {

                ItemDto itemDto = new ItemDto()
                {
                    CategoryId = item.CategoryId,
                    CurrentState = item.CurrentState,
                    GpuName = item.Gpu.GpuName,
                    HardDiskName = item.HardDisk.HardDiskName,
                    ImageName = item.ImageName,
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    ItemTypeId = item.ItemTypeId,
                    OsName = item.Os.OsName,
                    ProcessorName = item.Processor.ProcessorName,
                    PurchasePrice = item.PurchasePrice,
                    Ram = Convert.ToInt32(item.Ram.RamSize),
                    SalesPrice = Convert.ToInt32(item.SalesPrice),
                    ScreenResolution = item.ScreenResolution.ScreenResolutionName,
                    ScreenSize = item.ScreenSize,
                    Weight = item.Weight,
                    Description=item.Description,
                    listImages = item.TbItemImages.Select(iti => iti.ImageName).ToList()
                };

                return itemDto;
            }
            return null;

        }
    }
}
