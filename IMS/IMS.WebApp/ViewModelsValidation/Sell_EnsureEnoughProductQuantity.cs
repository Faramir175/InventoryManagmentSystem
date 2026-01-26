using IMS.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IMS.Core.Validations
{
    public class Sell_EnsureEnoughProductQuantity : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var sellViewModel = validationContext.ObjectInstance as SellViewModel;
            if (sellViewModel is not null)
            {
                if (sellViewModel.Product != null)
                {
                    if(sellViewModel.Product.Quantity < sellViewModel.Quantity)
                    {
                        return new ValidationResult($"There isn't enogh product ({sellViewModel.Product.Name}). " +
                            $"There is only ({sellViewModel.Product.Quantity}) in the warehouse ", new[] {validationContext.MemberName});
                    }
                }
            }

            return ValidationResult.Success;

        }
    }
}