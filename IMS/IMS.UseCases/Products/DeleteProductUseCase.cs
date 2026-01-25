using IMS.Core;
using IMS.UseCases.Interfaces;
using IMS.UseCases.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Inventories
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductRepository _productRep;
        public DeleteProductUseCase(IProductRepository productRep)
        {
            _productRep = productRep;
        }
        public async Task ExecuteAsync(Guid invId)
        {
            await _productRep.DeleteProductByIdAsync(invId);
        }
    }
}
