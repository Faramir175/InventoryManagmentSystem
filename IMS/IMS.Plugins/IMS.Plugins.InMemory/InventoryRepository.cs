using IMS.Core;
using IMS.UseCases.Interfaces;

namespace IMS.Plugins.InMemory
{
    public class InventoryRepository : IInventoryRepository
    {
        private List<Inventory> _inventories;
        public InventoryRepository()
        {
            _inventories = new List<Inventory>()
            {
                new Inventory{Id = Guid.Parse("74ab7b1f-16ec-426f-9bed-6d35da181a06"), Name = "Bike Sear",Quantity= 10,Price= 2},
                new Inventory{Id = Guid.Parse("64ab7b1f-16ec-426f-9bed-6d35da181a06"), Name = "Bike Body",Quantity= 10,Price= 15},
                new Inventory{Id = Guid.Parse("54ab7b1f-16ec-426f-9bed-6d35da181a06"), Name = "Bike Wheels",Quantity= 20,Price= 8},
                new Inventory{Id = Guid.Parse("44ab7b1f-16ec-426f-9bed-6d35da181a06"), Name = "Bike Pedels",Quantity= 20,Price= 1}
            };
        }
        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return await Task.FromResult(_inventories);
            }
            return _inventories.Where(i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
