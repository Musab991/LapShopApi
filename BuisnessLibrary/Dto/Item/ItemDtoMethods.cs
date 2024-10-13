using DomainLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Dto.Item
{
    public static class ItemDtoMethods
    {
        public static  IEnumerable<ItemDto> convertItemsToListOfItemsDto(IEnumerable<TbItem> listItems)
        {
            if (listItems.Any())
            {


                return listItems.Select(item => new ItemDto
                {
                    CategoryId = item.CategoryId,
                    CurrentState = item.CurrentState,
                    GpuName = item.Gpu.Gpuname,
                    HardDiskName = item.HardDisk.HardDiskName,
                    ImageName = item.ImageName,
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    ItemTypeId = item.ItemTypeId,
                    OsName = item.Os.OsName,
                    ProcessorName = item.Processor.ProcessorName,
                    PurchasePrice = item.PurchasePrice,
                    Ram = Convert.ToInt32(item.Ram.Ramsize),
                    SalesPrice = Convert.ToInt32(item.SalesPrice),
                    ScreenResolution = item.ScreenResolution.ScreenResolutionName,
                    ScreenSize = item.ScreenSize,
                    Weight = item.Weight,
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
                    GpuName = item.Gpu.Gpuname,
                    HardDiskName = item.HardDisk.HardDiskName,
                    ImageName = item.ImageName,
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    ItemTypeId = item.ItemTypeId,
                    OsName = item.Os.OsName,
                    ProcessorName = item.Processor.ProcessorName,
                    PurchasePrice = item.PurchasePrice,
                    Ram = Convert.ToInt32(item.Ram.Ramsize),
                    SalesPrice = Convert.ToInt32(item.SalesPrice),
                    ScreenResolution = item.ScreenResolution.ScreenResolutionName,
                    ScreenSize = item.ScreenSize,
                    Weight = item.Weight,
                    listImages = item.TbItemImages.Select(iti => iti.ImageName).ToList()
                };

                return itemDto;
            }
            return null;

        }
    }
}
