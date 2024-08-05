using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Dtos.Common
{
    public class SongDto : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public Genre Genre { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public int DurationSeconds { get; set; }
        public Guid AlbumId { get; set; }
        public List<Guid> ArtistsIds { get; set; } = new()!;
        public List<Guid> SimilarSongsIds { get; set; } = new()!;
    }
}