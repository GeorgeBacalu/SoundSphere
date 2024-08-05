using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Dtos.Common
{
    public class AlbumDto : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateOnly ReleaseDate { get; set; }
        public List<Guid> SimilarAlbumsIds { get; set; } = new()!;
    }
}