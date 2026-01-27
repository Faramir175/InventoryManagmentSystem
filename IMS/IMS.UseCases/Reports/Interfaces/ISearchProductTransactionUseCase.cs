using IMS.Core;

namespace IMS.UseCases.Reports.Interfaces
{
    public interface ISearchProductTransactionUseCase
    {
        Task<IEnumerable<ProductionTransaction>> ExecuteAsync(string invName, DateTime? dateForm, DateTime? dateTo, ProductTransactionType? transactionType);
    }
}