using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLibrary.Entities;

public partial class TbGpu
{
    [Key]
    public int GpuId { get; set; }

    public string GpuName { get; set; } = null!;

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
