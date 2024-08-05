using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Dtos.Common
{
    public class ArtistDto : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string? Bio { get; set; }
        public List<Guid> SimilarArtistsIds { get; set; } = new()!;
    }
}