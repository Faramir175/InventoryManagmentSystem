using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IMS.Core.Validations
{
    public class Product_EnsurePriceIsGreaterThanInventoriesCost : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var product = validationContext.ObjectInstance as Product;
            if (product != null)
            {
                if (!ValidatePrice(product))
                {
                    return new ValidationResult($"The product's price is less than the inventories cost: {
                        CalculateTotalInventoriesCost(product).ToString("c", new System.Globalization.CultureInfo("en-US"))}",
                        new List<string>() { validationContext.MemberName});
                }
            }
            return ValidationResult.Success;
        }

        private decimal CalculateTotalInventoriesCost(Product product)
        {
            if (product == null || product.ProductInventories == null)
            {
                return 0;
            }
            return product.ProductInventories.Sum(pi => pi.Inventory?.Price * pi.Quantity ?? 0);
        }

        private bool ValidatePrice(Product product)
        {
            if (product.ProductInventories == null || product.ProductInventories.Count < 0) return true;

            if(CalculateTotalInventoriesCost(product) > product.Price) return false;

            return true;
        }
    }
}
