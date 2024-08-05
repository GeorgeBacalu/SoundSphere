using SoundSphere.Database.Attributes;
using SoundSphere.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Dtos.Common
{
    public class UserDto : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(75, ErrorMessage = "Name can't be longer than 75 characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(00|\+?40|0)(7\d{2}|\d{2}[13]|[2-37]\d|8[02-9]|9[0-2])\s?\d{3}\s?\d{3}$", ErrorMessage = "Invalid mobile format")]
        public string Phone { get; set; } = null!; // Phone format: 00/+40/0 + phone prefix + optional space + 3 digits (first part) + optional space + 3 digits (second part)

        [Required(ErrorMessage = "Address is required")]
        [StringLength(150, ErrorMessage = "Address can't be longer than 150 characters")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Birthday is required")]
        [Date(ErrorMessage = "Birthday can't be in the future")]
        public DateOnly Birthday { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Invalid URL format")]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = "Role is required")]
        public Role Role { get; set; }
    }
}