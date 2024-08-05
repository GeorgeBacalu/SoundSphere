using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Dtos.Common
{
    public class UserDto : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateOnly Birthday { get; set; }
        public string ImageUrl { get; set; } = null!;
        public Role Role { get; set; }
    }
}