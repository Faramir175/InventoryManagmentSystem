using IMS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.Plugins.EFCoreSql
{
    public class IMSContext : DbContext
    {
        public IMSContext(DbContextOptions<IMSContext> options) : base(options)
        {
            
        }
        public DbSet<Inventory>? Inventories { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<InventoryTransaction>? InventoryTransactions { get; set; }
        public DbSet<ProductionTransaction>? ProductionTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInventory>()
                .HasKey(pi => new { pi.ProductId, pi.InventoryId });
            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductInventories)
                .HasForeignKey(pi => pi.ProductId);
            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Inventory)
                .WithMany(p => p.ProductInventories)
                .HasForeignKey(pi => pi.InventoryId);

            //seeding data
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { Id = Guid.Parse("74ab7b1f-16ec-426f-9bed-6d35da181a06"), Name = "Bike Seat", Quantity = 10, Price = 2 },
                new Inventory { Id = Guid.Parse("64ab7b1f-16ec-426f-9bed-6d35da181a06"), Name = "Bike Body", Quantity = 10, Price = 15 },
                new Inventory { Id = Guid.Parse("54ab7b1f-16ec-426f-9bed-6d35da181a06"), Name = "Bike Wheels", Quantity = 20, Price = 8 },
                new Inventory { Id = Guid.Parse("44ab7b1f-16ec-426f-9bed-6d35da181a06"), Name = "Bike Pedels", Quantity = 20, Price = 1 }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = Guid.Parse("74ab7b1f-16ec-426f-9bed-6d35da181a01"), Name = "Bike", Quantity = 10, Price = 150 },
                new Product { Id = Guid.Parse("74ab7b1f-16ec-426f-9bed-6d35da181a02"), Name = "Car", Quantity = 10, Price = 1750 }
            );
            // #1 
            modelBuilder.Entity<ProductInventory>().HasData(
            new ProductInventory { 
                ProductId = Guid.Parse("74ab7b1f-16ec-426f-9bed-6d35da181a01"), 
                InventoryId = Guid.Parse("74ab7b1f-16ec-426f-9bed-6d35da181a06"),
                Quantity = 1 }, // seat
            new ProductInventory { 
                ProductId = Guid.Parse("74ab7b1f-16ec-426f-9bed-6d35da181a01"), 
                InventoryId = Guid.Parse("64ab7b1f-16ec-426f-9bed-6d35da181a06"), 
                Quantity = 1 }, // body
            new ProductInventory { 
                ProductId = Guid.Parse("74ab7b1f-16ec-426f-9bed-6d35da181a01"), 
                InventoryId = Guid.Parse("54ab7b1f-16ec-426f-9bed-6d35da181a06"), 
                Quantity = 2 }, //wheels
            new ProductInventory { 
                ProductId = Guid.Parse("74ab7b1f-16ec-426f-9bed-6d35da181a01"), 
                InventoryId = Guid.Parse("44ab7b1f-16ec-426f-9bed-6d35da181a06"), 
                Quantity = 2 } //pedal
            );
        }
    }
}
