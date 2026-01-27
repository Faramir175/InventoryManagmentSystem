using IMS.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Interfaces
{
    public interface IInventoryTransactionRepository
    {
        void PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, decimal price);
        Task ProduceAsync(string productionNumber, Inventory inventory, int quantity, string doneBy, decimal price);
        Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionsAsync(string invName, DateTime? dateForm, DateTime? dateTo, InventoryTransactionType? transactionType);
    }
}
