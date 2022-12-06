using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ContactsAPI.Data.Attributes
{
    public class PhoneValidator : ValidationAttribute
    {
        //mobile and stationary phone numbers
        private static readonly string PhoneValidation_Regex = @"^\+?(972|0)(\-)?0?(([23489]{1}\d{7})|[5]{1}\d{8})$";
        private static Regex PhoneValidation_Regex_Compiled = null;

        public PhoneValidator()
        {
            PhoneValidation_Regex_Compiled = new Regex(PhoneValidation_Regex, RegexOptions.IgnoreCase);
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var str = value?.ToString();

            if (string.IsNullOrEmpty(str))
                return ValidationResult.Success;

            if(!PhoneValidation_Regex_Compiled.IsMatch(str))
                return new ValidationResult("Invalid Phone Number");

            return ValidationResult.Success;


        }
    }
}
