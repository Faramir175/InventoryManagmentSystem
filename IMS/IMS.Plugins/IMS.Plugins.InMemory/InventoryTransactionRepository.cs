using IMS.Core;
using IMS.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.Plugins.InMemory
{
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        public List<InventoryTransaction> _inventoryTransactions = new List<InventoryTransaction>();

        public Task ProduceAsync(string productionNumber, Inventory inventory, int quantity, string doneBy)
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
                TransactionDate = DateTime.UtcNow
            });
            return Task.CompletedTask;
        }

        public void PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, decimal price)
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
        }
    }
}
