using System.ComponentModel.DataAnnotations;

namespace ExpTracker.Custom_Validations
{
    public class NotFutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not DateOnly date)
                return true;

            return date <= DateOnly.FromDateTime(DateTime.Today);
        }
    }
}
