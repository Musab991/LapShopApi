using BuisnessLibrary.Dto.Item;
using DomainLibrary.Entities;
using Microsoft.AspNetCore.Components.Web.Virtualization;


namespace BuisnessLibrary.Dto.SalesInvoice
{
    public static class InvoiceMapper
    {
        //this mapped on invoice now map multi invoices
        public static SalesInvoiceDto ConvertToDto(TbSalesInvoice invoice)
        {
            // Check if there are any items in the invoice
            if (invoice.TbSalesInvoiceItems is null || !invoice.TbSalesInvoiceItems.Any())
            {
                // Optionally return a default DTO or handle the case as needed
                return new SalesInvoiceDto();
            }
            var invoiceItems = invoice.TbSalesInvoiceItems.Select(i => ConvertToItemDto(i)).ToList();

            var invoiceDto = new SalesInvoiceDto
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceDate = invoice.InvoiceDate,
                DeliveryDate = invoice.DelivryDate,
                Notes = invoice.Notes,
                CreatedBy = invoice.CreatedBy,
                CreatedDate = invoice.InvoiceDate,
                CurrentState = invoice.CurrentState,
                UpdatedBy = invoice.UpdatedBy,
                UpdatedDate = invoice.UpdatedDate,
                Items = invoiceItems,
                TotalInvoicePrice = invoiceItems.Sum(i => i.Total) // Use LINQ to calculate total
            };
           

            return invoiceDto;
        }
        public static IEnumerable<SalesInvoiceDto> ConvertToDto(IEnumerable<TbSalesInvoice>listInvoices)
        {
            // Check if there are any items in the invoice
            if (listInvoices is null || !listInvoices.Any())
            {
                // Optionally return a default DTO or handle the case as needed
                return new List<SalesInvoiceDto>();
            }

            var invoices = listInvoices.Select(voic => new SalesInvoiceDto()
            {
                CreatedBy = voic.CreatedBy,
                CreatedDate = voic.CreatedDate,
                CurrentState = voic.CurrentState,
                DeliveryDate = voic.DelivryDate,
                InvoiceDate = voic.InvoiceDate,
                InvoiceId = voic.InvoiceId,
                Notes = voic.Notes,
                Items = voic.TbSalesInvoiceItems.Select(i => ConvertToItemDto(i)).ToList(),
                TotalInvoicePrice = voic.TbSalesInvoiceItems.Sum(i=>ConvertToItemDto(i).Total),
                UpdatedBy = voic.UpdatedBy,
                UpdatedDate = voic.UpdatedDate

            });


            return invoices;

           

        }

        private static SalesInvoiceItemDto ConvertToItemDto(TbSalesInvoiceItem item)
        {
            var total = item.Qty * item.InvoicePrice; // Calculate total for the item

            return new SalesInvoiceItemDto
            {
                ItemId = item.ItemId,
                Quantity = item.Qty,
                Price = item.InvoicePrice,
                Notes = item.Notes,
                ItemName = item.Item.ItemName, 
                Total = total // Assign the calculated total
            };
        }
    
    }
    
}
