using BuisnessLibrary.Dto.Item;
using DomainLibrary.Entities;

namespace BuisnessLibrary.Bl.Repository.Interface
{
    public interface ISalesInvoiceItemRepository : IGenericRepository<TbSalesInvoiceItem>
    {
        /// <summary>
        /// receive all items for an invoice and add to invoice of items table
        /// </summary>
        /// <param name="invoiceId">invoice id  that contains these items</param>
        /// <param name="listOfItems">items that relate to this inovice</param>
        public Task SyncSalesInvoiceItemsWithDatabase(int invoiceId,List<SalesInvoiceItemDto> listOfItems);
    }
   
   
}
