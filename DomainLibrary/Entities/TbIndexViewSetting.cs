using System;
using System.Collections.Generic;

namespace DomainLibrary.Entities;

public partial class TbIndexViewSetting
{
    public int Id { get; set; }

    public string Title1SectionTspace { get; set; } = null!;

    public string TitleInner1 { get; set; } = null!;

    public string Productpara { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public bool CurrentState { get; set; }

    public string? UpdatedBy { get; set; }
}
