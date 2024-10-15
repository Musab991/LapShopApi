
using BuisnessLibrary.Dto.Item;

namespace BuisnessLibrary.Dto.SalesInvoice
{
    public class SalesInvoiceDto
    {
        public int InvoiceId { get; set; }
        public decimal TotalInvoicePrice{ get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Notes { get; set; }
        //public int CustomerId { get; set; }
        public string  CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CurrentState { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Items that belong to the invoice
        public List<SalesInvoiceItemDto> Items { get; set; } = new List<SalesInvoiceItemDto>();
    }




}
