using IMS.Core;
using IMS.Plugins.EFCoreSql.Migrations;
using IMS.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.Plugins.EFCoreSql
{
    public class InventoryEFCoreRepository : IInventoryRepository
    {
        private readonly IDbContextFactory<IMSContext> contextFactory;

        public InventoryEFCoreRepository(IDbContextFactory<IMSContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            using var db = contextFactory.CreateDbContext();
            db.Inventories?.Add(inventory);
            await db.SaveChangesAsync();
        }

        public async Task DeleteInventoryByIdAsync(Guid invId)
        {
            using var db = contextFactory.CreateDbContext();
            var inventory = db.Inventories!.Find(invId);
            if (inventory == null) return;
            db.Inventories?.Remove(inventory);
            await db.SaveChangesAsync();
        }

        public async Task EditInventoryAsync(Inventory inventory)
        {
            using var db = contextFactory.CreateDbContext();
            var inv = await db.Inventories.FindAsync(inventory.Id);
            if(inv != null)
            {
                inv.Name = inventory.Name;
                inv.Quantity = inventory.Quantity;
                inv.Price = inventory.Price;
                await db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            using var db = contextFactory.CreateDbContext();
            return await db.Inventories.Where(i => i.Name.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();
        }

        public async Task<Inventory?> GetInventoryByIdAsync(Guid invId)
        {
            using var db = contextFactory.CreateDbContext();
            var inventory = await db.Inventories!.FindAsync(invId);

            return inventory;
        }
    }
}
