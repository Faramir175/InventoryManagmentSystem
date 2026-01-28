using IMS.Core;
using IMS.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.Plugins.EFCoreSql
{
    public class InventoryTransactionEFCoreRepository : IInventoryTransactionRepository
    {
        private readonly DbContextFactory<IMSContext> contextFactory;

        public InventoryTransactionEFCoreRepository(DbContextFactory<IMSContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        public async Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionsAsync(string invName, 
            DateTime? dateForm, DateTime? dateTo, InventoryTransactionType? transactionType)
        {
            using var db = contextFactory.CreateDbContext();

            var query = from it in db.InventoryTransactions
                        join inv in db.Inventories on it.InventoryId equals inv.Id
                        where
                            (string.IsNullOrWhiteSpace(invName) || inv.Name.ToLower().IndexOf(invName.ToLower()) >= 0)
                            &&
                            (!dateForm.HasValue || it.TransactionDate >= dateForm.Value.Date) &&
                            (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date) &&
                            (!transactionType.HasValue || it.ActivityType == transactionType)
                        select it;
            return await query.Include(i => i.Inventory).ToListAsync();
        }

        public async Task ProduceAsync(string productionNumber, Inventory inventory, int quantity, string doneBy, decimal price)
        {
            using var db = contextFactory.CreateDbContext();
            db.InventoryTransactions?.Add(new InventoryTransaction
            {
                Id = Guid.NewGuid(),
                InventoryId = inventory.Id,
                QuantityBefore = quantity,
                QuantityAfter = inventory.Quantity - quantity,
                ActivityType = InventoryTransactionType.ProduceProduct,
                DoneBy = doneBy,
                ProductionNumber = productionNumber,
                TransactionDate = DateTime.UtcNow,
                UnitPrice = price
            });
            await db.SaveChangesAsync();
        }

        public async Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, decimal price)
        {
            using var db = contextFactory.CreateDbContext();
            db.InventoryTransactions.Add(new InventoryTransaction
            {
                Id = Guid.NewGuid(),
                InventoryId = inventory.Id,
                QuantityBefore = quantity,
                QuantityAfter = inventory.Quantity + quantity,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                DoneBy = doneBy,
                UnitPrice = price,
                PONumber = poNumber,
                TransactionDate = DateTime.UtcNow
            });
            await db.SaveChangesAsync();

        }
    }
}