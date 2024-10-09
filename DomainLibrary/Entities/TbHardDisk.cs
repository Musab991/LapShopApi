using System;
using System.Collections.Generic;

namespace DomainLibrary.Entities;

public partial class TbHardDisk
{
    public int HardDiskId { get; set; }

    public string HardDiskName { get; set; } = null!;

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
