using IMS.Core;
using IMS.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.Plugins.InMemory
{
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly IInventoryRepository inventoryRepository;
        public List<InventoryTransaction> _inventoryTransactions = new List<InventoryTransaction>();
        public InventoryTransactionRepository(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionsAsync(string invName, DateTime? dateForm, DateTime? dateTo, InventoryTransactionType? transactionType)
        {
            var inventories = (await inventoryRepository.GetInventoriesByNameAsync(string.Empty)).ToList();

            var query = from it in this._inventoryTransactions
                        join inv in inventories on it.InventoryId equals inv.Id
                        where
                            (string.IsNullOrWhiteSpace(invName) || inv.Name.ToLower().IndexOf(invName.ToLower()) >= 0)
                            &&
                            (!dateForm.HasValue || it.TransactionDate >= dateForm.Value.Date) &&
                            (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date) &&
                            (!transactionType.HasValue || it.ActivityType == transactionType)
                        select new InventoryTransaction
                        {
                            Id = it.Id,
                            InventoryId = it.InventoryId,
                            QuantityBefore = it.QuantityBefore,
                            QuantityAfter = it.QuantityAfter,
                            ActivityType = it.ActivityType,
                            UnitPrice = it.UnitPrice,
                            PONumber = it.PONumber,
                            ProductionNumber = it.ProductionNumber,
                            DoneBy = it.DoneBy,
                            TransactionDate = it.TransactionDate,
                            Inventory = inv
                        };
            return query;
        }

        public Task ProduceAsync(string productionNumber, Inventory inventory, int quantity, string doneBy, decimal price)
        {
            _inventoryTransactions.Add(new InventoryTransaction
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
            return Task.CompletedTask;
        }

        public Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, decimal price)
        {
            _inventoryTransactions.Add(new InventoryTransaction
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
            return Task.CompletedTask;
        }
    }
}
