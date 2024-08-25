using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using static SoundSphere.Test.Mocks.SongMock;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Mocks
{
    public class PlaylistMock
    {
        private PlaylistMock() { }

        public static readonly List<Playlist> _playlists = [new() 
        {
            Id = Guid.Parse("239d050b-b59c-47e0-9e1a-ab5faf6f903e"),
            Title = "Echoes of Euphoria",
            User = _users[0],
            Songs = [_songs[0], _songs[1]],
            CreatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("67b394ad-aeba-4804-be29-71fc4ebd37c8"),
            Title = "Midnight Vibes",
            User = _users[0],
            Songs = [_songs[2], _songs[3]],
            CreatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("25fc284d-9ad3-462e-8947-9a46f1bb7bde"),
            Title = "Retro Grooves",
            User = _users[0],
            Songs = [_songs[4], _songs[5]],
            CreatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("db73836b-b090-4b9e-979c-f02e712a4941"),
            Title = "Chillwave Escapes",
            User = _users[0],
            Songs = [_songs[6], _songs[7]],
            CreatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("883b32f5-6f45-4e36-aae4-271b40322445"),
            Title = "Urban Beats",
            User = _users[0],
            Songs = [_songs[8], _songs[9]],
            CreatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a9f4b39f-c273-41e1-a82f-544e4c287192"),
            Title = "Indie Dreams",
            User = _users[1],
            Songs = [_songs[10], _songs[11]],
            CreatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("49ea6e91-4033-49bd-9275-24292ee36bb8"),
            Title = "Soulful Sessions",
            User = _users[1],
            Songs = [_songs[12], _songs[13]],
            CreatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f1d2f01e-dcb1-46e5-bb3c-a6a0cbd280f3"),
            Title = "Acoustic Mornings",
            User = _users[1],
            Songs = [_songs[14], _songs[15]],
            CreatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("20a85f02-c737-4f5e-ab72-e1512e1177d1"),
            Title = "Electronic Odyssey",
            User = _users[1],
            Songs = [_songs[16], _songs[17]],
            CreatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c012c843-ca72-4121-b818-4b38d03be24b"),
            Title = "Rock Revival",
            User = _users[1],
            Songs = [_songs[18], _songs[19]],
            CreatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 10, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("c6c1b55d-b30f-49e1-af8d-beeff7b76b24"),
            Title = "Jazz Journeys",
            User = _users[2],
            Songs = [_songs[20], _songs[21]],
            CreatedAt = new DateTime(2024, 8, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("55fca191-e4fc-43da-a576-1a867256e65d"),
            Title = "HipHop Highways",
            User = _users[2],
            Songs = [_songs[22], _songs[23]],
            CreatedAt = new DateTime(2024, 8, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("514b9ab3-0a63-4219-8989-9b3dd6422005"),
            Title = "Classical Serenity",
            User = _users[2],
            Songs = [_songs[24], _songs[25]],
            CreatedAt = new DateTime(2024, 8, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e8560429-a622-473a-9646-bfcaac9cc934"),
            Title = "Pop Spectrum",
            User = _users[2],
            Songs = [_songs[26], _songs[27]],
            CreatedAt = new DateTime(2024, 8, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("677abd5c-33e0-4553-9be7-f8be825e7afe"),
            Title = "Alt-Rock Anthems",
            User = _users[2],
            Songs = [_songs[28], _songs[29]],
            CreatedAt = new DateTime(2024, 8, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("1b6de32d-f60e-45ee-9d96-dcec12fe6525"),
            Title = "Folklore Fields",
            User = _users[3],
            Songs = [_songs[30], _songs[31]],
            CreatedAt = new DateTime(2024, 8, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("4c8747b0-e5a6-48e4-af5c-bdebbe041daa"),
            Title = "R&B Reflections",
            User = _users[3],
            Songs = [_songs[32], _songs[33]],
            CreatedAt = new DateTime(2024, 8, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("ab9ee809-25a5-4c16-8f04-1c9ee63c7784"),
            Title = "Techno Pulse",
            User = _users[3],
            Songs = [_songs[34], _songs[35]],
            CreatedAt = new DateTime(2024, 8, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 18, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("01de5998-2c9f-40c5-8766-0d17103da4a0"),
            Title = "Country Roads",
            User = _users[3],
            Songs = [_songs[36], _songs[37]],
            CreatedAt = new DateTime(2024, 8, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 19, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("309ba8a0-3a2a-44fd-b359-1058defb3c75"),
            Title = "Reggae Rhythms",
            User = _users[3],
            Songs = [_songs[38], _songs[39]],
            CreatedAt = new DateTime(2024, 8, 20, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 20, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 20, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("ca6f3c65-8051-48a5-8c36-adbfc1a6ac1c"),
            Title = "Latin Fire",
            User = _users[4],
            Songs = [_songs[40], _songs[41]],
            CreatedAt = new DateTime(2024, 8, 21, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 21, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("80759f9e-c756-4ae1-abcc-c5b300c4890f"),
            Title = "Dance Floor Fillers",
            User = _users[4],
            Songs = [_songs[42], _songs[43]],
            CreatedAt = new DateTime(2024, 8, 22, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 22, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("07919c9c-2642-4a31-8200-e978052c4d7e"),
            Title = "Cinematic Scores",
            User = _users[4],
            Songs = [_songs[44], _songs[45]],
            CreatedAt = new DateTime(2024, 8, 23, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 23, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("cd410a0f-e3dc-4cd9-99b8-ecbd540bb19c"),
            Title = "Punk Power",
            User = _users[4],
            Songs = [_songs[46], _songs[47]],
            CreatedAt = new DateTime(2024, 8, 24, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 24, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a3d00a7e-fc57-4666-b45a-0977ed204df5"),
            Title = "Ambient Horizons",
            User = _users[4],
            Songs = [_songs[48], _songs[49]],
            CreatedAt = new DateTime(2024, 8, 25, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 25, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e0b1dfed-0a04-439b-8d85-c0f0f1d34b69"),
            Title = "Synthwave Dreams",
            User = _users[5],
            Songs = [_songs[50], _songs[51]],
            CreatedAt = new DateTime(2024, 8, 26, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 26, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("6f3d74c7-50f4-4a28-a17c-1cb1f549ce59"),
            Title = "Energetic Beats",
            User = _users[5],
            Songs = [_songs[52], _songs[53]],
            CreatedAt = new DateTime(2024, 8, 27, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 27, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("5b82d124-78a2-4638-b865-2ef7e7d6a0d2"),
            Title = "Electronic Chill",
            User = _users[5],
            Songs = [_songs[54], _songs[55]],
            CreatedAt = new DateTime(2024, 8, 28, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 28, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f5729d38-03c3-4a27-8d63-e43a4e5276c3"),
            Title = "Folk Inspirations",
            User = _users[5],
            Songs = [_songs[56], _songs[57]],
            CreatedAt = new DateTime(2024, 8, 29, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 29, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("6e4c1e87-e4ff-4b43-9ad7-94f6243b058a"),
            Title = "Pop Hits of the 90s",
            User = _users[5],
            Songs = [_songs[58], _songs[59]],
            CreatedAt = new DateTime(2024, 8, 30, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 30, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 30, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("9f64cf15-5a3e-4a73-80c3-74851d18a2f5"),
            Title = "Country Classics",
            User = _users[6],
            Songs = [_songs[60], _songs[61]],
            CreatedAt = new DateTime(2024, 8, 31, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 31, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b3a3c9ea-c78e-44c0-9274-4b3c8f825ae4"),
            Title = "Soul Groove",
            User = _users[6],
            Songs = [_songs[62], _songs[63]],
            CreatedAt = new DateTime(2024, 9, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("5fa03b7b-16b7-4b3b-b26d-42688d809a1e"),
            Title = "Jazz Fusion",
            User = _users[6],
            Songs = [_songs[64], _songs[65]],
            CreatedAt = new DateTime(2024, 9, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("84b4f63d-dbb6-4d2f-b28b-fbcff91b4c4f"),
            Title = "Lofi Lounge",
            User = _users[6],
            Songs = [_songs[66], _songs[67]],
            CreatedAt = new DateTime(2024, 9, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7d2a7e6b-dc63-4b67-8e9a-53f020f6c2c0"),
            Title = "Classic Rock Hits",
            User = _users[6],
            Songs = [_songs[68], _songs[69]],
            CreatedAt = new DateTime(2024, 9, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f54937f3-3a34-4a5e-92af-fd13f3e46b22"),
            Title = "Indie Essentials",
            User = _users[7],
            Songs = [_songs[70], _songs[71]],
            CreatedAt = new DateTime(2024, 9, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("9ac9c8a7-2ff6-4187-91bb-c6cfcf26ab1b"),
            Title = "Ambient Dreams",
            User = _users[7],
            Songs = [_songs[72], _songs[73]],
            CreatedAt = new DateTime(2024, 9, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("02c1834c-82fd-49dc-8b14-9d80b28cbb34"),
            Title = "Pop Magic",
            User = _users[7],
            Songs = [_songs[74], _songs[75]],
            CreatedAt = new DateTime(2024, 9, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("fbb4d8cb-36b7-464b-9250-b75c1c42e56f"),
            Title = "Electronic Beats",
            User = _users[7],
            Songs = [_songs[76], _songs[77]],
            CreatedAt = new DateTime(2024, 9, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("bcb58a94-2d6a-4f5a-a9ac-5675b756fbfa"),
            Title = "Folk Fusion",
            User = _users[7],
            Songs = [_songs[78], _songs[79]],
            CreatedAt = new DateTime(2024, 9, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 9, 0, 0, 0),
            DeletedAt = new DateTime(2024, 9, 9, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("3ef67e29-7a5d-4b5c-9e68-b2e23d7343e6"),
            Title = "Dance Party Hits",
            User = _users[8],
            Songs = [_songs[80], _songs[81]],
            CreatedAt = new DateTime(2024, 9, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2fc6f4b6-5a9a-4f23-87d1-9a4d82e3b8d2"),
            Title = "Synth Pop Classics",
            User = _users[8],
            Songs = [_songs[82], _songs[83]],
            CreatedAt = new DateTime(2024, 9, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b33cfe86-4d4b-40b8-85c3-cf80e14f8d36"),
            Title = "Alternative Essentials",
            User = _users[8],
            Songs = [_songs[84], _songs[85]],
            CreatedAt = new DateTime(2024, 9, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("4f4dbb4e-6260-429d-bc1c-9a0ef13d8d90"),
            Title = "Classic Country",
            User = _users[8],
            Songs = [_songs[86], _songs[87]],
            CreatedAt = new DateTime(2024, 9, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2e00e477-10f0-4c87-b94e-7a89369c01d0"),
            Title = "R&B Hits",
            User = _users[8],
            Songs = [_songs[88], _songs[89]],
            CreatedAt = new DateTime(2024, 9, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d44a6e92-4b19-4975-b0f3-1f75d9f7333e"),
            Title = "Tech House Grooves",
            User = _users[9],
            Songs = [_songs[90], _songs[91]],
            CreatedAt = new DateTime(2024, 9, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("21e84a8f-b8d7-489b-bd1f-4b8c8e14cb4c"),
            Title = "Reggae Vibes",
            User = _users[9],
            Songs = [_songs[92], _songs[93]],
            CreatedAt = new DateTime(2024, 9, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7e9cba7c-1a0a-4c9b-87e3-64fc8739fcfd"),
            Title = "Latin Grooves",
            User = _users[9],
            Songs = [_songs[94], _songs[95]],
            CreatedAt = new DateTime(2024, 9, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b4e51dc4-25e4-4d29-b0ee-ef758488dd4b"),
            Title = "Cinematic Soundscapes",
            User = _users[9],
            Songs = [_songs[96], _songs[97]],
            CreatedAt = new DateTime(2024, 9, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 18, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("5d8e8e1c-2799-4e34-8a09-b13b7595a497"),
            Title = "Classic Jazz",
            User = _users[9],
            Songs = [_songs[98], _songs[99]],
            CreatedAt = new DateTime(2024, 9, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 19, 0, 0, 0),
            DeletedAt = new DateTime(2024, 9, 19, 12, 0, 0)
        }];

        public static readonly Playlist _newPlaylist = new()
        {
            Title = "Chill Vibes",
            User = _users[0],
            Songs = [_songs[0], _songs[1]]
        };

        public static readonly List<PlaylistDto> _playlistDtos = _playlists.Select(ToDto).ToList();

        public static readonly PlaylistDto _newPlaylistDto = ToDto(_newPlaylist);

        public static readonly List<Playlist> _playlistsPagination = _playlists.Where(playlist => playlist.DeletedAt == null).Take(10).ToList();

        public static readonly List<PlaylistDto> _playlistDtosPagination = _playlistsPagination.Select(ToDto).ToList();

        public static readonly PlaylistPaginationRequest _playlistPayload = new(SortCriteria: null, SearchCriteria: null, Title: null, UserName: null, SongTitle: null, DateRange: null);

        private static PlaylistDto ToDto(Playlist playlist) => new()
        {
            Id = playlist.Id,
            Title = playlist.Title,
            UserId = playlist.User.Id,
            SongsIds = playlist.Songs.Select(song => song.Id).ToList(),
            CreatedAt = playlist.CreatedAt,
            UpdatedAt = playlist.UpdatedAt,
            DeletedAt = playlist.DeletedAt
        };
    }
}