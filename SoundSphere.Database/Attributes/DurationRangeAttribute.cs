using SoundSphere.Database.Dtos.Request.Models;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Attributes
{
    public class DurationRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var durationRange = value as DurationRange;
            return durationRange?.MinSeconds == null || durationRange?.MinSeconds < durationRange?.MaxSeconds ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? "Min duration must be shorter than max duration");
        }
    }
}