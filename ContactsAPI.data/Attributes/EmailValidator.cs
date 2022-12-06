using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ContactsAPI.Attributes
{
    public class EmailValidator : ValidationAttribute
    {

        private static readonly string EmailValidation_Regex = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
        private static Regex EmailValidation_Regex_Compiled = null;
        public EmailValidator()
        {
            EmailValidation_Regex_Compiled = new Regex(EmailValidation_Regex, RegexOptions.IgnoreCase);
        }
         
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var email = value?.ToString();

            //email is not a required field
            if (string.IsNullOrEmpty(email)) {
                return ValidationResult.Success;
            }

            if(!EmailValidation_Regex_Compiled.IsMatch(email))
                return new ValidationResult("Invalid Email Address");

            return ValidationResult.Success;
        }

    }
}
