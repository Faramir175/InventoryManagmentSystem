using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace IMS.Core
{
    public class ProductInventory
    {
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }
        public Guid InventoryId { get; set; }
        [JsonIgnore]
        public Inventory? Inventory { get; set; }
        public int Quantity { get; set; }
    }
}
