using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Attributes
{
    public class SocialNumberValidator : ValidationAttribute 
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is null)
                return new ValidationResult("Social Number is required.");

            var number = int.Parse(value?.ToString());

            //Default value means user input was null
            if(number == 0)
                return new ValidationResult("Social Number is required.");

            if(!IsValidIsraeliID(number.ToString()))
                return new ValidationResult("Invalid Social Number.");

            return ValidationResult.Success;
        }

        //This is Luhn algorithm, see https://en.wikipedia.org/wiki/Luhn_algorithm
        //Basiccly I check if the checksum is mod 10, and contains 9 digits
        private static bool IsValidIsraeliID(string israeliID)
        {
            if (israeliID.Length != 9)
                return false;

            long sum = 0;

            for (int i = 0; i < israeliID.Length; i++)
            {
                var digit = israeliID[israeliID.Length - 1 - i] - '0';
                sum += (i % 2 != 0) ? GetDouble(digit) : digit;
            }

            return sum % 10 == 0;

            int GetDouble(long i)
            {
                switch (i)
                {
                    case 0: return 0;
                    case 1: return 2;
                    case 2: return 4;
                    case 3: return 6;
                    case 4: return 8;
                    case 5: return 1;
                    case 6: return 3;
                    case 7: return 5;
                    case 8: return 7;
                    case 9: return 9;
                    default: return 0;
                }
            }
        }
    }
}
