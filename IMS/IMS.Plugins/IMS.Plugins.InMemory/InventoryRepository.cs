using IMS.Core;
using IMS.UseCases.Interfaces;
using System.Xml.Linq;

namespace IMS.Plugins.InMemory
{
    public class InventoryRepository : IInventoryRepository
    {
        private List<Inventory> _inventories;
        public InventoryRepository()
        {
            _inventories = new List<Inventory>()
            {
                new Inventory{Id = Guid.Parse("74ab7b1f-16ec-426f-9bed-6d35da181a06"), Name = "Bike Seat",Quantity= 10,Price= 2},
                new Inventory{Id = Guid.Parse("64ab7b1f-16ec-426f-9bed-6d35da181a06"), Name = "Bike Body",Quantity= 10,Price= 15},
                new Inventory{Id = Guid.Parse("54ab7b1f-16ec-426f-9bed-6d35da181a06"), Name = "Bike Wheels",Quantity= 20,Price= 8},
                new Inventory{Id = Guid.Parse("44ab7b1f-16ec-426f-9bed-6d35da181a06"), Name = "Bike Pedels",Quantity= 20,Price= 1}
            };
        }

        public Task AddInventoryAsync(Inventory inventory)
        {
            if(_inventories.Any(i => i.Name.Equals(inventory.Name,StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }
            inventory.Id = Guid.NewGuid();
            _inventories.Add(inventory);
            return Task.CompletedTask;
        }

        public Task DeleteInventoryByIdAsync(Guid invId)
        {
            var inventory = _inventories.FirstOrDefault(i => i.Id == invId);
            if (inventory != null)
            {
                _inventories.Remove(inventory);
            }
            return Task.CompletedTask;
        }

        public Task EditInventoryAsync(Inventory inventory)
        {
            if(_inventories.Any(i => i.Id != inventory.Id && i.Name.Equals(inventory.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }

            var existingInventory = _inventories.FirstOrDefault(i => i.Id.Equals(inventory.Id));

            if (existingInventory is not null)
            {
                existingInventory.Name = inventory.Name;
                existingInventory.Quantity = inventory.Quantity;
                existingInventory.Price = inventory.Price;
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return await Task.FromResult(_inventories);
            }
            return _inventories.Where(i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Inventory?> GetInventoryByIdAsync(Guid invId)
        {
            return await Task.FromResult(_inventories.FirstOrDefault(i => i.Id.Equals(invId)));
        }
    }
}