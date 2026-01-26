using IMS.Core;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Activities
{
    public class ProduceProductUseCase : IProduceProductUseCase
    {
        private readonly IProductTransactionRepository _productTransactionRepository;
        private readonly IProductRepository _productRepository;
        public ProduceProductUseCase(IProductTransactionRepository productTransactionRepository, IProductRepository productRepository)
        {
            _productTransactionRepository = productTransactionRepository;
            _productRepository = productRepository;
        }
        public async Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
            await _productTransactionRepository.ProduceAsync(productionNumber, product, quantity, doneBy);


            product.Quantity += quantity;
            await _productRepository.EditProductAsync(product);
        }
    }
}
