using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IMS.Core
{
    public class ProductionTransaction
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int QuantityBefore { get; set; }
        [Required]
        public int QuantityAfter { get; set; }
        public ProductTransactionType ActivityType { get; set; }
        public decimal? UnitPrice { get; set; }
        public string SONumber { get; set; } = String.Empty;
        public string ProductionNumber { get; set; } = String.Empty;
        [Required]
        public string DoneBy { get; set; } = String.Empty;
        [Required]
        public DateTime TransactionDate { get; set; }

        public Product? Product { get; set; }

    }
}
