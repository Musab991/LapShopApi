using System.ComponentModel.DataAnnotations;

namespace BuisnessLibrary.Dto.Item
{
    public class ItemAddDto
    {
        [Required]
        public string ItemName { get; set; } = null!;
        [Required]
        public decimal SalesPrice { get; set; }

        public decimal PurchasePrice { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public string? ImageName { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string CreatedBy { get; set; } = null!;
        [Required]
        public int CurrentState { get; set; }
       
        public string? Description { get; set; }
        [Required]
        public int ItemTypeId { get; set; }
        [Required]
        public string ScreenSize { get; set; } = null!;

        [Required]

        public string? Weight { get; set; }
        [Required]

        public int OsId { get; set; }
        [Required]

        public int GpuId { get; set; }
        [Required]

        public int ProcessorId { get; set; }
        [Required]

        public int HardDiskId { get; set; }
        [Required]

        public int ScreenResolutionId { get; set; }
        [Required]
        public int? RamId { get; set; }

        [Required]
        public List<string> ListImages { get; set; } = new List<string>();

    }
}
