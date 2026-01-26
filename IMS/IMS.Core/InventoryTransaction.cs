using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IMS.Core
{
    public class InventoryTransaction
    {
        public Guid Id { get; set; }
        [Required]
        public Guid InventoryId { get; set; }
        [Required]
        public int QuantityBefore { get; set; }
        [Required]
        public int QuantityAfter { get; set; }
        public InventoryTransactionType ActivityType { get; set; }
        public decimal UnitPrice { get; set; }
        public string PONumber { get; set; } = String.Empty;
        [Required]    
        public string DoneBy { get; set; } = String.Empty;
        [Required]
        public DateTime TransactionDate { get; set; }

        public Inventory Inventory { get; set; }

    }
}
