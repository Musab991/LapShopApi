namespace BuisnessLibrary.Dto.Item
{
    public class SalesInvoiceItemDto
    {
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Notes { get; set; }
        public decimal Total { get; set; }


    }
}
