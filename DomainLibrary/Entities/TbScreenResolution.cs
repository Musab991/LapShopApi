using System;
using System.Collections.Generic;

namespace DomainLibrary.Entities;

public partial class TbScreenResolution
{
    public int ScreenResolutionId { get; set; }

    public string ScreenResolutionName { get; set; } = null!;

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
