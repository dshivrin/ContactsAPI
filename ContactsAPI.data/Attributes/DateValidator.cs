using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Attributes
{
    public class DateValidator: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var stringDate = value?.ToString();

            //this case is basically impossible, because datetime objects has min value of 01/01/0001. (hence the next case)
            //but since the parameter is nullable I must check it
            if (string.IsNullOrEmpty(stringDate))
                return new ValidationResult("Date of Birth is required.");

            var date = DateTime.Parse(stringDate);

            //min value means that the client sent a null / empty string.
            if (date == DateTime.MinValue)
                return new ValidationResult("Date of Birth is required.");

            //birth date cannot be in the future
            if (date > DateTime.Now)
                return new ValidationResult("Time travelers are not allowed here");

            return ValidationResult.Success;
        }
    }
}
