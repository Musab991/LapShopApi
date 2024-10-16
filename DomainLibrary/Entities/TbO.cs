﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLibrary.Entities;

public partial class TbO
{
    [Key]

    public int OsId { get; set; }

    public string OsName { get; set; } = null!;

    public string ImageName { get; set; } = null!;

    public bool ShowInHomePage { get; set; }

    public bool CurrentState { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
