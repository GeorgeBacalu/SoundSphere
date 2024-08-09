namespace SoundSphere.Database.Entities
{
    public class Playlist : BaseEntity
    {
        public string Title { get; set; } = null!;
        public User User { get; set; } = null!;
        public List<Song> Songs { get; set; } = new()!;
    }
}