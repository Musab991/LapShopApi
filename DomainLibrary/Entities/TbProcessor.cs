using System;
using System.Collections.Generic;

namespace DomainLibrary.Entities;

public partial class TbProcessor
{
    public int ProcessorId { get; set; }

    public string ProcessorName { get; set; } = null!;

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
