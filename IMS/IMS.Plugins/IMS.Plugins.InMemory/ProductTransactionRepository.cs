using IMS.Core;
using IMS.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.Plugins.InMemory
{
    public class ProductTransactionRepository : IProductTransactionRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryTransactionRepository _inventoryTransactionRepository;
        private readonly IInventoryRepository _inventoryRepository;

        private List<ProductionTransaction> _productionTransactions = new List<ProductionTransaction>();

        public ProductTransactionRepository(IProductRepository productRepository, IInventoryTransactionRepository inventoryTransactionRepository,
            IInventoryRepository inventoryRepository)
        {
            _productRepository = productRepository;
            _inventoryTransactionRepository = inventoryTransactionRepository;
            _inventoryRepository = inventoryRepository;
        }
        public async Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
            var prod = await _productRepository.GetProductByIdAsync(product.Id);
            if (product is not null)
            {
                foreach(var pi in prod.ProductInventories)
                {
                    if(pi.Inventory is not null)
                    {
                        // add inventory transaction for each inventory used in production
                        _inventoryTransactionRepository.ProduceAsync(productionNumber, pi.Inventory, pi.Quantity * quantity, doneBy);

                        var inv = await _inventoryRepository.GetInventoryByIdAsync(pi.InventoryId);
                        inv.Quantity -= pi.Quantity * quantity;

                        // decrease inventory quantity
                        await _inventoryRepository.EditInventoryAsync(inv);
                    }
                }
            }
            // add product transaction for production
            _productionTransactions.Add(new ProductionTransaction
            {
                ProductionNumber = productionNumber,
                ProductId = product.Id,
                Product = product,
                ActivityType = ProductTransactionType.ProduceProduct,
                QuantityBefore = quantity,
                QuantityAfter = product.Quantity + quantity,
                DoneBy = doneBy,
                TransactionDate = DateTime.UtcNow
            });
        }

        public Task SellProductAsync(string salesOrderNumber, Product product, int quantity, decimal price, string doneBy)
        {
            _productionTransactions.Add(new ProductionTransaction
            {
                ProductId = product.Id,
                Product = product,
                ActivityType = ProductTransactionType.SellProduct,
                QuantityBefore = product.Quantity,
                QuantityAfter = product.Quantity - quantity,
                DoneBy = doneBy,
                TransactionDate = DateTime.UtcNow,
                SONumber = salesOrderNumber,
                UnitPrice = price
            });
            return Task.CompletedTask;
        }
    }
}
