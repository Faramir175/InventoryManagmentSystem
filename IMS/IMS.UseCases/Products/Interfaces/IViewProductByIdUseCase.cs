using IMS.Core;

namespace IMS.UseCases.Products.Interfaces
{
    public interface IViewProductByIdUseCase
    {
        Task<Product> ExecuteAsync(Guid invId);
    }
}