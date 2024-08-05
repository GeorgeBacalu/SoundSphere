using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using static SoundSphere.Test.Mocks.AlbumMock;
using static SoundSphere.Test.Mocks.ArtistMock;

namespace SoundSphere.Test.Mocks
{
    public class SongMock
    {
        private SongMock() { }

        public static List<Song> GetSongs() => GetSongs1().Concat(GetSongs2()).ToList();

        public static List<SongDto> GetSongDtos() => GetSongs().Select(ToDto).ToList();

        public static List<Song> GetSongs1() => [GetSong1(), GetSong2()];

        public static List<Song> GetSongs2() => [GetSong3(), GetSong4()];

        public static List<SongDto> GetSongDtos1() => GetSongs1().Select(ToDto).ToList();

        public static List<SongDto> GetSongDtos2() => GetSongs2().Select(ToDto).ToList();

        public static Song GetSong1() => new()
        {
            Id = Guid.Parse("64f534f8-f2d4-4402-95a3-54de48b678a8"),
            Title = "song_title1",
            ImageUrl = "https://song_image1.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2020, 1, 1),
            DurationSeconds = 180,
            Album = GetAlbum1(),
            Artists = [GetArtist1()],
            SimilarSongs = [new() { SongId = Guid.Parse("64f534f8-f2d4-4402-95a3-54de48b678a8"), SimilarSongId = Guid.Parse("278cfa5a-6f44-420e-9930-07da6c43a6ad") }],
            CreatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            DeletedAt = null
        };

        public static Song GetSong2() => new()
        {
            Id = Guid.Parse("278cfa5a-6f44-420e-9930-07da6c43a6ad"),
            Title = "song_title2",
            ImageUrl = "https://song_image2.jpg",
            Genre = Genre.Rock,
            ReleaseDate = new DateOnly(2020, 1, 2),
            DurationSeconds = 185,
            Album = GetAlbum1(),
            Artists = [GetArtist1()],
            SimilarSongs = [new() { SongId = Guid.Parse("278cfa5a-6f44-420e-9930-07da6c43a6ad"), SimilarSongId = Guid.Parse("7ef7351b-912e-4a64-ba6d-cfdfcb7d56af") }],
            CreatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            DeletedAt = null
        };

        public static Song GetSong3() => new()
        {
            Id = Guid.Parse("7ef7351b-912e-4a64-ba6d-cfdfcb7d56af"),
            Title = "song_title3",
            ImageUrl = "https://song_image3.jpg",
            Genre = Genre.Rnb,
            ReleaseDate = new DateOnly(2020, 1, 3),
            DurationSeconds = 190,
            Album = GetAlbum2(),
            Artists = [GetArtist2()],
            SimilarSongs = [new() { SongId = Guid.Parse("7ef7351b-912e-4a64-ba6d-cfdfcb7d56af"), SimilarSongId = Guid.Parse("03b3fb9f-38af-4074-8ab5-b9644ab44397") }],
            CreatedAt = new DateTime(2024, 7, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 3, 0, 0, 0),
            DeletedAt = null
        };

        public static Song GetSong4() => new()
        {
            Id = Guid.Parse("03b3fb9f-38af-4074-8ab5-b9644ab44397"),
            Title = "song_title4",
            ImageUrl = "https://song_image4.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2020, 1, 4),
            DurationSeconds = 195,
            Album = GetAlbum2(),
            Artists = [GetArtist2()],
            SimilarSongs = [],
            CreatedAt = new DateTime(2024, 7, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 4, 0, 0, 0),
            DeletedAt = null
        };

        public static Song GetNewSong() => new()
        {
            Title = "new_song_title",
            ImageUrl = "https://new_song_image.jpg",
            Genre = Genre.Dance,
            ReleaseDate = new DateOnly(2020, 1, 5),
            DurationSeconds = 200,
            Album = GetAlbum1(),
            Artists = [GetArtist1()],
            SimilarSongs = []
        };

        public static SongDto GetSongDto1() => ToDto(GetSong1());

        public static SongDto GetSongDto2() => ToDto(GetSong2());

        public static SongDto GetSongDto3() => ToDto(GetSong3());

        public static SongDto GetSongDto4() => ToDto(GetSong4());

        public static SongDto GetNewSongDto() => ToDto(GetNewSong());

        public static SongDto ToDto(Song song) => new()
        {
            Id = song.Id,
            Title = song.Title,
            ImageUrl = song.ImageUrl,
            Genre = song.Genre,
            ReleaseDate = song.ReleaseDate,
            DurationSeconds = song.DurationSeconds,
            AlbumId = song.Album.Id,
            ArtistsIds = song.Artists.Select(artist => artist.Id).ToList(),
            SimilarSongsIds = song.SimilarSongs.Select(songLink => songLink.SimilarSongId).ToList(),
            CreatedAt = song.CreatedAt,
            UpdatedAt = song.UpdatedAt,
            DeletedAt = song.DeletedAt
        };
    }
}