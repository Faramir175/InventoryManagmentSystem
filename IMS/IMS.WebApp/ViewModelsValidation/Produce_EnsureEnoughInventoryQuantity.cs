using IMS.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IMS.Core.Validations
{
    public class Produce_EnsureEnoughInventoryQuantity : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var produceViewModel = validationContext.ObjectInstance as ProduceViewModel;
            if (produceViewModel is not null)
            {
                if (produceViewModel.Product != null && produceViewModel.Product.ProductInventories != null)
                {
                    foreach (var pi in produceViewModel.Product.ProductInventories)
                    {
                        if (pi.Inventory != null &&
                            pi.Quantity * produceViewModel.Quantity > pi.Inventory.Quantity)
                        {
                            return new ValidationResult($"The inventory ({pi.Inventory.Name}) is not enough to produce {produceViewModel.Quantity} products",
                                new[] { validationContext.MemberName });
                        }
                    }
                }
            }

            return ValidationResult.Success;

        }
    }
}