using System.Text.Json.Serialization;

namespace SoundSphere.Database.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateOnly Birthday { get; set; }
        public string ImageUrl { get; set; } = null!;
        public Role Role { get; set; }
        [JsonIgnore] public List<UserSong>? UserSongs { get; set; } = new()!;
        [JsonIgnore] public List<UserArtist>? UserArtists { get; set; } = new()!;
    }

    public enum Role { InvalidRole, Admin, Moderator, Listener }
}