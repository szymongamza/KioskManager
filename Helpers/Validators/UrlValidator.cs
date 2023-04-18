﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace KioskManager.Helpers.Validators
{
    public class UrlValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var x = value.ToString();
                if (Regex.IsMatch(x, @"^http:\/\/\w+(\.\w+)*(:[0-9]+)?\/?(\/[.\w]*)*$", RegexOptions.IgnoreCase))
                {
                    return ValidationResult.Success;
                }
                else if (Regex.IsMatch(x, @"^https:\/\/\w+(\.\w+)*(:[0-9]+)?\/?(\/[.\w]*)*$", RegexOptions.IgnoreCase))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            else
            {
                return new ValidationResult("Please enter some value");
            }
        }
    }
}
