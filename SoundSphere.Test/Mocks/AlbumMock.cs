using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;

namespace SoundSphere.Test.Mocks
{
    public class AlbumMock
    {
        private AlbumMock() { }

        public static List<Album> GetAlbums() => [GetAlbum1(), GetAlbum2()];

        public static List<AlbumDto> GetAlbumDtos() => GetAlbums().Select(ToDto).ToList();

        public static Album GetAlbum1() => new()
        {
            Id = Guid.Parse("6ee76a77-2be4-42e3-8417-e60d282cffcb"),
            Title = "album_title1",
            ImageUrl = "https://album_image1.jpg",
            ReleaseDate = new DateOnly(2020, 1, 1),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("6ee76a77-2be4-42e3-8417-e60d282cffcb"), SimilarAlbumId = Guid.Parse("b58f5f3f-d5e8-49f3-8b12-cfe33f762b4f") }],
            CreatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            DeletedAt = null
        };

        public static Album GetAlbum2() => new()
        {
            Id = Guid.Parse("b58f5f3f-d5e8-49f3-8b12-cfe33f762b4f"),
            Title = "album_title2",
            ImageUrl = "https://album_image2.jpg",
            ReleaseDate = new DateOnly(2020, 1, 2),
            SimilarAlbums = [],
            CreatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            DeletedAt = null
        };

        public static Album GetNewAlbum() => new()
        {
            Title = "new_album_title",
            ImageUrl = "https://new_album_image.jpg",
            ReleaseDate = new DateOnly(2020, 1, 3),
            SimilarAlbums = []
        };

        public static AlbumDto GetAlbumDto1() => ToDto(GetAlbum1());

        public static AlbumDto GetAlbumDto2() => ToDto(GetAlbum2());

        public static AlbumDto GetNewAlbumDto() => ToDto(GetNewAlbum());

        public static AlbumDto ToDto(Album album) => new()
        {
            Id = album.Id,
            Title = album.Title,
            ImageUrl = album.ImageUrl,
            ReleaseDate = album.ReleaseDate,
            SimilarAlbumsIds = album.SimilarAlbums.Select(albumLink => albumLink.SimilarAlbumId).ToList(),
            CreatedAt = album.CreatedAt,
            UpdatedAt = album.UpdatedAt,
            DeletedAt = album.DeletedAt
        };
    }
}