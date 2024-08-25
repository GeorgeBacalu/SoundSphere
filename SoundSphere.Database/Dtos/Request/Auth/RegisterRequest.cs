using SoundSphere.Database.Attributes;
using SoundSphere.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Dtos.Request.Auth
{
    public record RegisterRequest(
        [Required(ErrorMessage = "Name is required")]
        [StringLength(75, ErrorMessage = "Name can't be longer than 75 characters")]
        string Name,

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        string Email,

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+-=()])(\S){8,30}$", ErrorMessage = "Invalid password format")]
        string Password, // Password rules: at least one digit + at least one lowercase letter + at least one uppercase letter + at least one special character + no whitespace + between 8 and 30 characters

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(00|\+?40|0)(7\d{2}|\d{2}[13]|[2-37]\d|8[02-9]|9[0-2])\s?\d{3}\s?\d{3}$", ErrorMessage = "Invalid phone number format")]
        string Phone, // Phone format: 00/+40/0 + phone prefix + optional space + 3 digits (first part) + optional space + 3 digits (second part)

        [Required(ErrorMessage = "Address is required")]
        [StringLength(150, ErrorMessage = "Address can't be longer than 150 characters")]
        string Address,

        [Required(ErrorMessage = "Birthday is required")]
        [Date(ErrorMessage = "Birthday can't be in the future")]
        DateOnly Birthday,

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Invalid URL format")]
        string ImageUrl,

        [Required(ErrorMessage = "Role is required")]
        Role Role);
}