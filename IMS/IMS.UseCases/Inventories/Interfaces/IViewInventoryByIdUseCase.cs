using IMS.Core;

namespace IMS.UseCases.Inventories.Interfaces
{
    public interface IViewInventoryByIdUseCase
    {
        Task<Inventory> ExecuteAsync(Guid invId);
    }
}