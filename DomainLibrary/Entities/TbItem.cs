using DomainLibrary.Entities.Contract;
using System;
using System.Collections.Generic;

namespace DomainLibrary.Entities;

public partial class TbItem: ISoftDeletable
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public decimal SalesPrice { get; set; }

    public decimal PurchasePrice { get; set; }

    public int CategoryId { get; set; }

    public string? ImageName { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public int CurrentState { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? Description { get; set; }

    public int ItemTypeId { get; set; }

    public string ScreenSize { get; set; } = null!;

    public string? Weight { get; set; }

    public int OsId { get; set; }

    public int GpuId { get; set; }

    public int ProcessorId { get; set; }

    public int HardDiskId { get; set; }

    public DateTime? DateDeleted { get; set; }

    public bool IsDeleted { get; set; }

    public int ScreenResolutionId { get; set; }

    public int? Ramid { get; set; }

    public virtual TbCategory Category { get; set; } = null!;

    public virtual TbGpu Gpu { get; set; } = null!;

    public virtual TbHardDisk HardDisk { get; set; } = null!;

    public virtual TbItemType ItemType { get; set; } = null!;

    public virtual TbO Os { get; set; } = null!;

    public virtual TbProcessor Processor { get; set; } = null!;

    public virtual TbRam? Ram { get; set; }

    public virtual TbScreenResolution ScreenResolution { get; set; } = null!;

    public virtual ICollection<TbItemDiscount> TbItemDiscounts { get; set; } = new List<TbItemDiscount>();

    public virtual ICollection<TbItemImage> TbItemImages { get; set; } = new List<TbItemImage>();

    public virtual ICollection<TbPurchaseInvoiceItem> TbPurchaseInvoiceItems { get; set; } = new List<TbPurchaseInvoiceItem>();

    public virtual ICollection<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; } = new List<TbSalesInvoiceItem>();

    public virtual ICollection<TbCustomer> Customers { get; set; } = new List<TbCustomer>();

}
