using IMS.Core;

namespace IMS.UseCases.Products.Interfaces
{
    public interface IEditProductUseCase
    {
        Task ExecuteAsync(Product product);
    }
}