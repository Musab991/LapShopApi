using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Dto
{
    public class ItemDto
    {

        public int ItemId {  get; set; }

        public string ItemName { get; set; } = null!;

        public decimal SalesPrice { get; set; }

        public decimal PurchasePrice { get; set; }
        public string? ImageName { get; set; }
        public int CategoryId { get; set; }

        public int CurrentState { get; set; }

        public int ItemTypeId { get; set; }
        public string ScreenSize { get; set; } = null!;
        public string? Weight { get; set; }

        public int OsName { get; set; }

        public int GpuName { get; set; }

        public int ProcessorName { get; set; }

        public int HardDiskName { get; set; }
        public int ScreenResolution { get; set; }
        public int Ram { get; set; }

        List<string>listImages { get; set; }


    }
}
