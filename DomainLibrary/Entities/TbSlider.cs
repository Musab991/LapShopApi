using System;
using System.Collections.Generic;

namespace DomainLibrary.Entities;

public partial class TbSlider
{
    public int SliderId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string ImageName { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public bool CurrentState { get; set; }

    public string? UpdatedBy { get; set; }
}
