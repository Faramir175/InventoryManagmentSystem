using IMS.Core;
using IMS.Core.Validations;
using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class SellViewModel
    {
        [Required]
        public string SalesOrderNumber { get; set; } = string.Empty;
        [GuidNotEmpty(ErrorMessage = "Please select a valid inventory item.")]
        public Guid ProductId { get; set; }
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantity has to be greater or equal to 1.")]
        [Sell_EnsureEnoughProductQuantity]
        public int Quantity { get; set; }
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "Price has to be greater or equal to 0.")]
        public decimal Price { get; set; }
        public Product? Product { get; set; }

    }
}
