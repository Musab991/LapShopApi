﻿using System;
using System.Collections.Generic;

namespace DomainLibrary.Entities;

public partial class TbItemType
{
    public int ItemTypeId { get; set; }

    public string ItemTypeName { get; set; } = null!;

    public string? ImageName { get; set; }

    public int CurrentState { get; set; }

    public DateOnly CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateOnly? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
