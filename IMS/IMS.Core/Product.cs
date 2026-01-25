using IMS.Core.Validations;
using System.ComponentModel.DataAnnotations;

namespace IMS.Core
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 0")]
        public int  Quantity{ get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Price must be greater or equal to 0")]
        public decimal Price { get; set; }
        [Product_EnsurePriceIsGreaterThanInventoriesCost]
        public List<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

        public void AddInventory(Inventory inventory)
        {

            if(!this.ProductInventories.Any(pi => pi.Inventory is not null && pi.Inventory.Name.Equals(inventory.Name)))
            {
                ProductInventories.Add(new ProductInventory
                {
                    ProductId = this.Id,
                    InventoryId = inventory.Id,
                    Product = this,
                    Inventory = inventory,
                    Quantity = 1
                });
            }
        }

        public void RemoveInventory(ProductInventory prodInventory)
        {
            this.ProductInventories?.Remove(prodInventory);
        }
    }
}
