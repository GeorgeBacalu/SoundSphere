using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Dtos.Common
{
    public class PlaylistDto : BaseEntity
    {
        public string Title { get; set; } = null!;
        public Guid UserId { get; set; }
        public List<Guid> SongsIds { get; set; } = new()!;
    }
}