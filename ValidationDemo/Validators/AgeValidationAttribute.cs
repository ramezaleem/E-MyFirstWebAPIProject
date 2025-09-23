using System.ComponentModel.DataAnnotations;

namespace ValidationDemo.Validators;

public class AgeValidationAttribute : ValidationAttribute
{
    private readonly int _minimumAge;
    private readonly int _maximumAge;

    public AgeValidationAttribute ( int minimumAge = 18, int maximumAge = 99 )
    {
        _minimumAge = minimumAge;
        _maximumAge = maximumAge;
        ErrorMessage = $"Age must be between {_minimumAge} and {_maximumAge} years.";
    }

    protected override ValidationResult? IsValid ( object? value, ValidationContext validationContext )
    {
        if (value == null)
        {
            return new ValidationResult("Date of Birth is required.");
        }

        if (!(value is DateTime dateOfBirth))
        {
            return new ValidationResult("Invalid Date of Birth format.");
        }

        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;

        if (age < _minimumAge || age > _maximumAge)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }

}
