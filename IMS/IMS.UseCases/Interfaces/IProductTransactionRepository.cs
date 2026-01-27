using IMS.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Interfaces
{
    public interface IProductTransactionRepository
    {
        Task<IEnumerable<ProductionTransaction>> GetProductTransactionsAsync(string invName, DateTime? dateForm, DateTime? dateTo, ProductTransactionType? transactionType);
        Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy);
        Task SellProductAsync(string salesOrderNumber, Product product, int quantity, decimal price, string doneBy);
    }
}
