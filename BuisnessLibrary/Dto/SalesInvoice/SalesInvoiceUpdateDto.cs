
using BuisnessLibrary.Dto.Item;
using DomainLibrary.Entities;

namespace BuisnessLibrary.Dto.SalesInvoice
{
    public class SalesInvoiceUpdateDto
    {
        [Required]
        public int InvoiceId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public double Qty { get; set; }
        [Required]
        public decimal InvoicePrice { get; set; }
        public string? Notes { get; set; }
        [Required]
        public List<SalesInvoiceItemDto> Items { get; set; } = new List<SalesInvoiceItemDto>();

    }
}
