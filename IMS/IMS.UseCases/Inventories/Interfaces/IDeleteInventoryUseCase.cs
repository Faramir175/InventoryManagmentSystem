using IMS.Core;

namespace IMS.UseCases.Inventories.Interfaces
{
    public interface IDeleteInventoryUseCase
    {
        Task ExecuteAsync(Guid inventory);
    }
}