using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLibrary.Entities;

public partial class TbItemImage
{
    [Key]

    public int ImageId { get; set; }

    public string ImageName { get; set; } = null!;

    public int ItemId { get; set; }

    public virtual TbItem Item { get; set; } = null!;
}
