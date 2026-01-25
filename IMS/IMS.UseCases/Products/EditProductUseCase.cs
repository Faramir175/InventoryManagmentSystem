using IMS.Core;
using IMS.UseCases.Interfaces;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Inventories
{
    public class EditProductUseCase : IEditProductUseCase
    {
        private readonly IProductRepository _productRep;
        public EditProductUseCase(IProductRepository productRep)
        {
            _productRep = productRep;
        }

        public async Task ExecuteAsync(Product product)
        {
            await _productRep.EditProductAsync(product);
        }
    }
}
