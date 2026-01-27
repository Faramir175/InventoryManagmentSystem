using IMS.Core;
using IMS.UseCases.Interfaces;
using IMS.UseCases.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Reports
{
    public class SearchInventoryTransactionUseCase : ISearchInventoryTransactionUseCase
    {
        private readonly IInventoryTransactionRepository inventoryTransactionRepository;

        public SearchInventoryTransactionUseCase(IInventoryTransactionRepository inventoryTransactionRepository)
        {
            this.inventoryTransactionRepository = inventoryTransactionRepository;
        }
        public async Task<IEnumerable<InventoryTransaction>> ExecuteAsync(string invName, DateTime? dateForm, DateTime? dateTo,
            InventoryTransactionType? transactionType)
        {
            if(dateTo is not null) dateTo = dateTo.Value.AddDays(1);
            return await inventoryTransactionRepository.GetInventoryTransactionsAsync(invName, dateForm, dateTo, transactionType);
        }
    }
}
