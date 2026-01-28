using IMS.Core;
using IMS.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.Plugins.EFCoreSql
{
    public class ProductTransactionEFCoreRepository : IProductTransactionRepository
    {
        private readonly DbContextFactory<IMSContext> contextFactory;
        private readonly IProductRepository _productRepository;
        private readonly IInventoryTransactionRepository _inventoryTransactionRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public ProductTransactionEFCoreRepository(DbContextFactory<IMSContext> contextFactory, 
            IProductRepository productRepository, IInventoryTransactionRepository inventoryTransactionRepository,
            IInventoryRepository inventoryRepository)
        {
            this.contextFactory = contextFactory;
            _productRepository = productRepository;
            _inventoryTransactionRepository = inventoryTransactionRepository;
            _inventoryRepository = inventoryRepository;
        }
        public async Task<IEnumerable<ProductionTransaction>> GetProductTransactionsAsync(string invName, DateTime? dateForm, DateTime? dateTo, ProductTransactionType? transactionType)
        {
            using var db = contextFactory.CreateDbContext();

            var query = from it in db.ProductionTransactions
                        join inv in db.Products on it.ProductId equals inv.Id
                        where
                            (string.IsNullOrWhiteSpace(invName) || inv.Name.ToLower().IndexOf(invName.ToLower()) >= 0)
                            &&
                            (!dateForm.HasValue || it.TransactionDate >= dateForm.Value.Date) &&
                            (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date) &&
                            (!transactionType.HasValue || it.ActivityType == transactionType)
                        select it;
            return await query.Include(p => p.Product).ToListAsync();
        }

        public async Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
            using var db = contextFactory.CreateDbContext();

            var prod = await _productRepository.GetProductByIdAsync(product.Id);
            if (product is not null)
            {
                foreach (var pi in prod.ProductInventories)
                {
                    if (pi.Inventory is not null)
                    {
                        // add inventory transaction for each inventory used in production
                        await _inventoryTransactionRepository.ProduceAsync(productionNumber, pi.Inventory, pi.Quantity * quantity, doneBy, -1);

                        var inv = await _inventoryRepository.GetInventoryByIdAsync(pi.InventoryId);
                        inv.Quantity -= pi.Quantity * quantity;

                        // decrease inventory quantity
                        await _inventoryRepository.EditInventoryAsync(inv);
                    }
                }
            }
            // add product transaction for production
            db.ProductionTransactions.Add(new ProductionTransaction
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

            await db.SaveChangesAsync();
        }

        public async Task SellProductAsync(string salesOrderNumber, Product product, int quantity, decimal price, string doneBy)
        {
            using var db = contextFactory.CreateDbContext();

            db.ProductionTransactions?.Add(new ProductionTransaction
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
            await db.SaveChangesAsync();
        }
    }
}
