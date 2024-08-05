namespace SoundSphere.Database.Entities
{
    public class ArtistPair
    {
        public Guid ArtistId { get; set; }
        public Guid SimilarArtistId { get; set; }
        public Artist? Artist { get; set; }
        public Artist? SimilarArtist { get; set; }
    }
}