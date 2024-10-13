using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Dto.Item
{
    public class ItemDto
    {

        public int ItemId { get; set; }

        public string ItemName { get; set; } = null!;

        public decimal SalesPrice { get; set; }

        public decimal PurchasePrice { get; set; }
        public string? ImageName { get; set; }
        public int CategoryId { get; set; }

        public int CurrentState { get; set; }

        public int ItemTypeId { get; set; }
        public string ScreenSize { get; set; } = null!;
        public string? Weight { get; set; }

        public string OsName { get; set; } = null!;

        public string GpuName { get; set; } = null!;

        public string ProcessorName { get; set; } = null!;

        public string HardDiskName { get; set; } = null!;
        public string ScreenResolution { get; set; } = null!;
        public int Ram { get; set; }

        public List<string> listImages { get; set; } = new List<string>();


    }


}
