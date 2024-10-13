using System;
using System.Collections.Generic;

namespace DomainLibrary.Entities;

public partial class VwItem
{
    public string ItemName { get; set; } = null!;

    public decimal PurchasePrice { get; set; }

    public decimal SalesPrice { get; set; }

    public int CategoryId { get; set; }

    public string? ImageName { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public int CurrentState { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? Description { get; set; }

    public int HardDiskId { get; set; }

    public int ItemTypeId { get; set; }

    public int ProcessorId { get; set; }

    public int? Ramid { get; set; }

    public int ScreenResolutionId { get; set; }

    public string ScreenSize { get; set; } = null!;

    public string? Weight { get; set; }

    public int OsId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string ItemTypeName { get; set; } = null!;

    public string OsName { get; set; } = null!;

    public int ItemId { get; set; }

    public int GpuId { get; set; }

    public string Gpuname { get; set; } = null!;

    public string Processor { get; set; } = null!;

    public string HardDisk { get; set; } = null!;

    public string ScreenResolution { get; set; } = null!;

    public int? Ramsize { get; set; }

    public bool IsDeleted { get; set; }
}
