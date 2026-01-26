using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IMS.Core.Validations
{
    public class GuidNotEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is Guid guid && guid == Guid.Empty)
            {
                return new ValidationResult(ErrorMessage ?? "Selection is required.");
            }
            return ValidationResult.Success;
        }
    }
}
