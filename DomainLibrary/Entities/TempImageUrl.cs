using System;
using System.Collections.Generic;

namespace DomainLibrary.Entities;

public partial class TempImageUrl
{
    public int Id { get; set; }

    public string? ImageUrl { get; set; }
}
