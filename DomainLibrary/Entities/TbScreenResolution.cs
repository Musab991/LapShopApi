using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLibrary.Entities;

public partial class TbScreenResolution
{
    [Key]

    public int ScreenResolutionId { get; set; }

    public string ScreenResolutionName { get; set; } = null!;

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
