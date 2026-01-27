using IMS.Core;
using IMS.UseCases.Interfaces;
using IMS.UseCases.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Reports
{
    public class SearchProductTransactionUseCase : ISearchProductTransactionUseCase
    {
        private readonly IProductTransactionRepository productTransactionRepository;

        public SearchProductTransactionUseCase(IProductTransactionRepository productTransactionRepository)
        {
            this.productTransactionRepository = productTransactionRepository;
        }
        public async Task<IEnumerable<ProductionTransaction>> ExecuteAsync(string invName, DateTime? dateForm, DateTime? dateTo,
            ProductTransactionType? transactionType)
        {
            if (dateTo is not null) dateTo = dateTo.Value.AddDays(1);
            return await productTransactionRepository.GetProductTransactionsAsync(invName, dateForm, dateTo, transactionType);
        }
    }
}
