using IMS.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Interfaces
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task DeleteProductByIdAsync(Guid invId);
        Task EditProductAsync(Product product);
        Task<Product?> GetProductByIdAsync(Guid invId);
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    }
}
