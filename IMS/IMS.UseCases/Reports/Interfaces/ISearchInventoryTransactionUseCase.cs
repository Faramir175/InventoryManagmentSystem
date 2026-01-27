using IMS.Core;

namespace IMS.UseCases.Reports.Interfaces
{
    public interface ISearchInventoryTransactionUseCase
    {
        Task<IEnumerable<InventoryTransaction>> ExecuteAsync(string invName, DateTime? dateForm, DateTime? dateTo, InventoryTransactionType? transactionType);
    }
}