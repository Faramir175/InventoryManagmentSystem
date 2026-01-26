using IMS.Core;
using IMS.Core.Validations;
using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class ProduceViewModel
    {
        [Required]
        public string ProductionNumber { get; set; } = string.Empty;
        [GuidNotEmpty(ErrorMessage = "Please select a valid product.")]
        public Guid ProductId { get; set; }
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantity has to be greater or equal to 1.")]
        [Produce_EnsureEnoughInventoryQuantity]
        public int Quantity { get; set; }

        public Product? Product { get; set; }
    }
}
