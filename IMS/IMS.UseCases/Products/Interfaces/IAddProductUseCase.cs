using IMS.Core;

namespace IMS.UseCases.Products.Interfaces
{
    public interface IAddProductUseCase
    {
        Task ExecuteAsync(Product product);
    }
}