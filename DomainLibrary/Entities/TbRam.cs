using System;
using System.Collections.Generic;

namespace DomainLibrary.Entities;

public partial class TbRam
{
    public int Ramid { get; set; }

    public int? Ramsize { get; set; }

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
