using System.Text.Json.Serialization;

namespace SoundSphere.Database.Entities
{
    public class Artist : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string? Bio { get; set; }
        [JsonIgnore] public List<Song>? Songs { get; set; } = new()!;
        public List<ArtistPair> SimilarArtists { get; set; } = new()!;
    }
}