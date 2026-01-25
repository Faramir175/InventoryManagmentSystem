using IMS.Core;
using IMS.UseCases.Interfaces;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Inventories
{
    public class ViewProductByIdUseCase : IViewProductByIdUseCase
    {
        private readonly IProductRepository _productRep;
        public ViewProductByIdUseCase(IProductRepository productRep)
        {
            _productRep = productRep;
        }

        public async Task<Product> ExecuteAsync(Guid invId)
        {
            return await _productRep.GetProductByIdAsync(invId);
        }
    }
}
