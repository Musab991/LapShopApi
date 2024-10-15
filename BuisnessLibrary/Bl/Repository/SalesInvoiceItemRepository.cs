using BuisnessLibrary.Bl.Repository.Interface;
using BuisnessLibrary.Dto.Item;
using DomainLibrary.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq;

namespace BuisnessLibrary.Bl.Repository
{
    public class SalesInvoiceItemRepository : GenericRepository<TbSalesInvoiceItem>, ISalesInvoiceItemRepository
    {
        private readonly AppDbContext _context;

        public SalesInvoiceItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task SyncSalesInvoiceItemsWithDatabase(int invoiceId, List<SalesInvoiceItemDto> listOfItems)
        {
            // Step 1: Get all item IDs from the provided list
            var listItemsIds = listOfItems.Select(i => i.ItemId).ToList();

            // Step 2: Get all items from the database for this invoice
            var databaseItems = await FindAsync(i => i.InvoiceId == invoiceId, null);
            var databaseItemsIds = databaseItems.Select(i => i.ItemId).ToList();

            // Step 3: Determine which items to keep, delete, or add
            
            // Items that exist in both database and the list
            var itemsToKeep = databaseItems.Where(item => listItemsIds.Contains(item.ItemId)).ToList();
            
            // Items in the database but not in the list
            var itemsToDelete = databaseItems.Where(item => !listItemsIds.Contains(item.ItemId)).ToList();

            // Items in the list but not in the database
            var itemsToAdd = listOfItems.Where(itemDto => !databaseItemsIds.Contains(itemDto.ItemId))
                .Select(it => new TbSalesInvoiceItem()
                {
                    InvoiceId = invoiceId,
                    ItemId = it.ItemId,
                    InvoicePrice = it.Price,
                    Notes = it.Notes,
                    Qty = it.Quantity
                }).ToList();

            // Step 4: Update the database context
           
            // Delete items
            if (itemsToDelete.Any())
            {
                _context.RemoveRange(itemsToDelete);
            }
         
            // Add new items
            if (itemsToAdd.Any())
            {
                await _context.AddRangeAsync(itemsToAdd);
            }

        }

    }
}
