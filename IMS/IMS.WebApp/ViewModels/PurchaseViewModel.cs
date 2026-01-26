using IMS.Core.Validations;
using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class PurchaseViewModel
    {
        [Required]
        public string POnumber { get; set; } = string.Empty;
        [GuidNotEmpty(ErrorMessage = "Please select a valid inventory item.")]
        public Guid InventoryId { get; set; }
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantity has to be greater or equal to 1.")]
        public int Quantity { get; set; }
        public decimal InventoryPrice { get; set; }
    }
}
