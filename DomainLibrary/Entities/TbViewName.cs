using System;
using System.Collections.Generic;

namespace DomainLibrary.Entities;

public partial class TbViewName
{
    public int Id { get; set; }

    public string FormName { get; set; } = null!;

    public string ControllerName { get; set; } = null!;
}
