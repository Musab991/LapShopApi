using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLibrary.Entities;

public partial class TbSalesInvoiceItem
{
    [Key]

    public int InvoiceItemId { get; set; }

    public int ItemId { get; set; }

    public int InvoiceId { get; set; }

    public int Qty { get; set; }

    public decimal InvoicePrice { get; set; }

    public string? Notes { get; set; }

    public virtual TbSalesInvoice Invoice { get; set; } = null!;

    public virtual TbItem Item { get; set; } = null!;
}
