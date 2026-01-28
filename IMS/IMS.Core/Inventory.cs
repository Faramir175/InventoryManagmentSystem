using System.ComponentModel.DataAnnotations;

namespace IMS.Core
{
    public class Inventory
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 0")]
        public int  Quantity{ get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Price must be greater or equal to 0")]
        public decimal Price { get; set; }
        public List<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

    }
}
