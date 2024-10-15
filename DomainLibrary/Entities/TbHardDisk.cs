using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLibrary.Entities;

public partial class TbHardDisk
{
    [Key]

    public int HardDiskId { get; set; }

    public string HardDiskName { get; set; } = null!;

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
