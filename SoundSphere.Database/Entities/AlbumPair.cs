namespace SoundSphere.Database.Entities
{
    public class AlbumPair
    {
        public Guid AlbumId { get; set; }
        public Guid SimilarAlbumId { get; set; }
        public Album? Album { get; set; }
        public Album? SimilarAlbum { get; set; }
    }
}