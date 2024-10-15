using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLibrary.Entities;

public partial class TbProcessor
{
    [Key]

    public int ProcessorId { get; set; }

    public string ProcessorName { get; set; } = null!;

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
