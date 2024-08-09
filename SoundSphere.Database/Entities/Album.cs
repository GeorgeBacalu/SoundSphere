namespace SoundSphere.Database.Entities
{
    public class Album : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateOnly ReleaseDate { get; set; }
        public List<AlbumPair> SimilarAlbums { get; set; } = new()!;
    }
}