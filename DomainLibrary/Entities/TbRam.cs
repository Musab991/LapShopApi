using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLibrary.Entities;

public partial class TbRam
{
    [Key]

    public int RamId { get; set; }

    public int? RamSize { get; set; }

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
