using SoundSphere.Database.Dtos.Request.Models;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Attributes
{
    public class DateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dateRange = value as DateRange;
            return dateRange?.StartDate == null || dateRange?.StartDate < dateRange?.EndDate ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? "Start date must be earlier than end date");
        }
    }
}