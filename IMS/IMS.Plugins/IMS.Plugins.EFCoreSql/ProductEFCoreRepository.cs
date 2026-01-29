using IMS.Core;
using IMS.Plugins.EFCoreSql.Migrations;
using IMS.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.Plugins.EFCoreSql
{
    public class ProductEFCoreRepository : IProductRepository
    {
        private readonly IDbContextFactory<IMSContext> contextFactory;

        public ProductEFCoreRepository(IDbContextFactory<IMSContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        public async Task AddProductAsync(Product product)
        {
            using var db = contextFactory.CreateDbContext();
            db.Products?.Add(product);
            FlagInventoryUnchanged(product, db);
            await db.SaveChangesAsync();
        }

        public async Task DeleteProductByIdAsync(Guid prodId)
        {
            using var db = contextFactory.CreateDbContext();
            var product = db.Products!.Find(prodId);
            if (product == null) return;
            db.Products?.Remove(product);
            await db.SaveChangesAsync();
        }

        public async Task EditProductAsync(Product product)
        {
            using var db = contextFactory.CreateDbContext();
            var prod = await db.Products.Include(p => p.ProductInventories)!
                .FirstOrDefaultAsync(pi => pi.Id == product.Id);
            if (prod != null)
            {
                prod.Name = product.Name;
                prod.Quantity = product.Quantity;
                prod.Price = product.Price;
                prod.ProductInventories = product.ProductInventories;
                FlagInventoryUnchanged(product, db);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Product?> GetProductByIdAsync(Guid prodId)
        {
            using var db = contextFactory.CreateDbContext();
            var product = await db.Products.Include(p => p.ProductInventories)!
                .ThenInclude(i => i.Inventory)
                .FirstOrDefaultAsync(pi => pi.Id == prodId);

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            using var db = contextFactory.CreateDbContext();
            return await db.Products.Where(i => i.Name.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();
        }

        private void FlagInventoryUnchanged(Product product, IMSContext db)
        {
            if(product?.ProductInventories != null && product.ProductInventories.Count>0)
            {
                foreach(var prodInv in product.ProductInventories)
                {
                    if(prodInv.Inventory != null)
                    {
                        db.Entry(prodInv.Inventory).State = EntityState.Unchanged;
                    }
                }
            }
        }
    }
}
