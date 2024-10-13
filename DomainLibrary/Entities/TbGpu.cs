using System;
using System.Collections.Generic;

namespace DomainLibrary.Entities;

public partial class TbGpu
{
    public int GpuId { get; set; }

    public string Gpuname { get; set; } = null!;

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
