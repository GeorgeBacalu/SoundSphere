using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using static SoundSphere.Test.Mocks.AlbumMock;
using static SoundSphere.Test.Mocks.ArtistMock;

namespace SoundSphere.Test.Mocks
{
    public class SongMock
    {
        private SongMock() { }

        public static readonly List<Song> _songs = [new()
        {
            Id = Guid.Parse("64f534f8-f2d4-4402-95a3-54de48b678a8"),
            Title = "Echo of Silence",
            ImageUrl = "https://echo_of_silence.jpg",
            Genre = Genre.Rnb,
            ReleaseDate = new DateOnly(2016, 11, 26),
            DurationSeconds = 221,
            Album = _albums[18],
            Artists = [_artists[0]],
            SimilarSongs = [new() { SongId = Guid.Parse("64f534f8-f2d4-4402-95a3-54de48b678a8"), SimilarSongId = Guid.Parse("278cfa5a-6f44-420e-9930-07da6c43a6ad") },
                            new() { SongId = Guid.Parse("64f534f8-f2d4-4402-95a3-54de48b678a8"), SimilarSongId = Guid.Parse("7ef7351b-912e-4a64-ba6d-cfdfcb7d56af") }],
            CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 1, 0, 0, 0),
            DeletedAt = null
        }, new() 
        {
            Id = Guid.Parse("278cfa5a-6f44-420e-9930-07da6c43a6ad"),
            Title = "Light Show",
            ImageUrl = "https://light_show.jpg",
            Genre = Genre.Rnb,
            ReleaseDate = new DateOnly(2016, 11, 28),
            DurationSeconds = 199,
            Album = _albums[18],
            Artists = [_artists[0]],
            SimilarSongs = [new() { SongId = Guid.Parse("278cfa5a-6f44-420e-9930-07da6c43a6ad"), SimilarSongId = Guid.Parse("7ef7351b-912e-4a64-ba6d-cfdfcb7d56af") },
                            new() { SongId = Guid.Parse("278cfa5a-6f44-420e-9930-07da6c43a6ad"), SimilarSongId = Guid.Parse("03b3fb9f-38af-4074-8ab5-b9644ab44397") }],
            CreatedAt = new DateTime(2024, 6, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7ef7351b-912e-4a64-ba6d-cfdfcb7d56af"),
            Title = "Twilight Zone",
            ImageUrl = "https://twilight_zone.jpg",
            Genre = Genre.Rnb,
            ReleaseDate = new DateOnly(2016, 11, 25),
            DurationSeconds = 205,
            Album = _albums[18],
            Artists = [_artists[1]],
            SimilarSongs = [new() { SongId = Guid.Parse("7ef7351b-912e-4a64-ba6d-cfdfcb7d56af"), SimilarSongId = Guid.Parse("03b3fb9f-38af-4074-8ab5-b9644ab44397") },
                            new() { SongId = Guid.Parse("7ef7351b-912e-4a64-ba6d-cfdfcb7d56af"), SimilarSongId = Guid.Parse("3aff8c17-3c98-44ed-a849-1976d2c4a91a") }],
            CreatedAt = new DateTime(2024, 6, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("03b3fb9f-38af-4074-8ab5-b9644ab44397"),
            Title = "Dark Paradise",
            ImageUrl = "https://dark_paradise.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2012, 1, 27),
            DurationSeconds = 240,
            Album = _albums[2],
            Artists = [_artists[1]],
            SimilarSongs = [new() { SongId = Guid.Parse("03b3fb9f-38af-4074-8ab5-b9644ab44397"), SimilarSongId = Guid.Parse("3aff8c17-3c98-44ed-a849-1976d2c4a91a") },
                            new() { SongId = Guid.Parse("03b3fb9f-38af-4074-8ab5-b9644ab44397"), SimilarSongId = Guid.Parse("e7b0024e-cc97-46a8-bd3a-450607eebe3c") }],
            CreatedAt = new DateTime(2024, 6, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3aff8c17-3c98-44ed-a849-1976d2c4a91a"),
            Title = "Lost in Beauty",
            ImageUrl = "https://lost_in_beauty.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2012, 1, 27),
            DurationSeconds = 218,
            Album = _albums[2],
            Artists = [_artists[2]],
            SimilarSongs = [new() { SongId = Guid.Parse("3aff8c17-3c98-44ed-a849-1976d2c4a91a"), SimilarSongId = Guid.Parse("e7b0024e-cc97-46a8-bd3a-450607eebe3c") },
                            new() { SongId = Guid.Parse("3aff8c17-3c98-44ed-a849-1976d2c4a91a"), SimilarSongId = Guid.Parse("8a5f664a-c72d-46b2-b12b-b38e4a5ec67f") }],
            CreatedAt = new DateTime(2024, 6, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e7b0024e-cc97-46a8-bd3a-450607eebe3c"),
            Title = "Summer Time Sadness",
            ImageUrl = "https://summer_time_sadness.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2012, 2, 1),
            DurationSeconds = 268,
            Album = _albums[2],
            Artists = [_artists[2]],
            SimilarSongs = [new() { SongId = Guid.Parse("e7b0024e-cc97-46a8-bd3a-450607eebe3c"), SimilarSongId = Guid.Parse("8a5f664a-c72d-46b2-b12b-b38e4a5ec67f") },
                            new() { SongId = Guid.Parse("e7b0024e-cc97-46a8-bd3a-450607eebe3c"), SimilarSongId = Guid.Parse("83b64a87-6bc5-4b61-9121-505f37b81682") }],
            CreatedAt = new DateTime(2024, 6, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("8a5f664a-c72d-46b2-b12b-b38e4a5ec67f"),
            Title = "Blue Jeans",
            ImageUrl = "https://blue_jeans.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2012, 2, 3),
            DurationSeconds = 245,
            Album = _albums[2],
            Artists = [_artists[3]],
            SimilarSongs = [new() { SongId = Guid.Parse("8a5f664a-c72d-46b2-b12b-b38e4a5ec67f"), SimilarSongId = Guid.Parse("83b64a87-6bc5-4b61-9121-505f37b81682") },
                            new() { SongId = Guid.Parse("8a5f664a-c72d-46b2-b12b-b38e4a5ec67f"), SimilarSongId = Guid.Parse("23abfe5e-e938-4bf5-93d4-202e2fa3aa3e") }],
            CreatedAt = new DateTime(2024, 6, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("83b64a87-6bc5-4b61-9121-505f37b81682"),
            Title = "Radio",
            ImageUrl = "https://radio.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2012, 2, 5),
            DurationSeconds = 235,
            Album = _albums[2],
            Artists = [_artists[3]],
            SimilarSongs = [new() { SongId = Guid.Parse("83b64a87-6bc5-4b61-9121-505f37b81682"), SimilarSongId = Guid.Parse("23abfe5e-e938-4bf5-93d4-202e2fa3aa3e") },
                            new() { SongId = Guid.Parse("83b64a87-6bc5-4b61-9121-505f37b81682"), SimilarSongId = Guid.Parse("48f88f8f-c393-4bda-9812-a748486f404e") }],
            CreatedAt = new DateTime(2024, 6, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("23abfe5e-e938-4bf5-93d4-202e2fa3aa3e"),
            Title = "Carmen",
            ImageUrl = "https://carmen.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2012, 2, 7),
            DurationSeconds = 249,
            Album = _albums[2],
            Artists = [_artists[4]],
            SimilarSongs = [new() { SongId = Guid.Parse("23abfe5e-e938-4bf5-93d4-202e2fa3aa3e"), SimilarSongId = Guid.Parse("48f88f8f-c393-4bda-9812-a748486f404e") },
                            new() { SongId = Guid.Parse("23abfe5e-e938-4bf5-93d4-202e2fa3aa3e"), SimilarSongId = Guid.Parse("eb6c8e4c-502e-45b4-9a69-387e33cdadb1") }],
            CreatedAt = new DateTime(2024, 6, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("48f88f8f-c393-4bda-9812-a748486f404e"),
            Title = "Night Visions",
            ImageUrl = "https://night_visions.jpg",
            Genre = Genre.Electronic,
            ReleaseDate = new DateOnly(2013, 5, 17),
            DurationSeconds = 237,
            Album = _albums[3],
            Artists = [_artists[4]],
            SimilarSongs = [new() { SongId = Guid.Parse("48f88f8f-c393-4bda-9812-a748486f404e"), SimilarSongId = Guid.Parse("eb6c8e4c-502e-45b4-9a69-387e33cdadb1") },
                            new() { SongId = Guid.Parse("48f88f8f-c393-4bda-9812-a748486f404e"), SimilarSongId = Guid.Parse("68b0682b-9ac7-42a2-873a-8e9874e12953") }],
            CreatedAt = new DateTime(2024, 6, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("eb6c8e4c-502e-45b4-9a69-387e33cdadb1"),
            Title = "Synth Dreams",
            ImageUrl = "https://synth_dreams.jpg",
            Genre = Genre.Electronic,
            ReleaseDate = new DateOnly(2013, 5, 17),
            DurationSeconds = 289,
            Album = _albums[3],
            Artists = [_artists[5]],
            SimilarSongs = [new() { SongId = Guid.Parse("eb6c8e4c-502e-45b4-9a69-387e33cdadb1"), SimilarSongId = Guid.Parse("68b0682b-9ac7-42a2-873a-8e9874e12953") },
                            new() { SongId = Guid.Parse("eb6c8e4c-502e-45b4-9a69-387e33cdadb1"), SimilarSongId = Guid.Parse("1e43835a-4902-4d12-abaf-0bc8f2dae2aa") }],
            CreatedAt = new DateTime(2024, 6, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("68b0682b-9ac7-42a2-873a-8e9874e12953"),
            Title = "Echoes of Tomorrow",
            ImageUrl = "https://echoes_of_tomorrow.jpg",
            Genre = Genre.Electronic,
            ReleaseDate = new DateOnly(2013, 5, 17),
            DurationSeconds = 205,
            Album = _albums[3],
            Artists = [_artists[5]],
            SimilarSongs = [new() { SongId = Guid.Parse("68b0682b-9ac7-42a2-873a-8e9874e12953"), SimilarSongId = Guid.Parse("1e43835a-4902-4d12-abaf-0bc8f2dae2aa") },
                            new() { SongId = Guid.Parse("68b0682b-9ac7-42a2-873a-8e9874e12953"), SimilarSongId = Guid.Parse("19dc1564-8c00-4377-95db-16535a80610a") }],
            CreatedAt = new DateTime(2024, 6, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("1e43835a-4902-4d12-abaf-0bc8f2dae2aa"),
            Title = "Touch of the Light",
            ImageUrl = "https://touch_of_the_light.jpg",
            Genre = Genre.Electronic,
            ReleaseDate = new DateOnly(2013, 5, 21),
            DurationSeconds = 237,
            Album = _albums[3],
            Artists = [_artists[6]],
            SimilarSongs = [new() { SongId = Guid.Parse("1e43835a-4902-4d12-abaf-0bc8f2dae2aa"), SimilarSongId = Guid.Parse("19dc1564-8c00-4377-95db-16535a80610a") },
                            new() { SongId = Guid.Parse("1e43835a-4902-4d12-abaf-0bc8f2dae2aa"), SimilarSongId = Guid.Parse("b26a4472-db66-4bec-926d-bb53f31083c2") }],
            CreatedAt = new DateTime(2024, 6, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("19dc1564-8c00-4377-95db-16535a80610a"),
            Title = "Digital Love",
            ImageUrl = "https://digital_love.jpg",
            Genre = Genre.Electronic,
            ReleaseDate = new DateOnly(2013, 5, 23),
            DurationSeconds = 225,
            Album = _albums[3],
            Artists = [_artists[6]],
            SimilarSongs = [new() { SongId = Guid.Parse("19dc1564-8c00-4377-95db-16535a80610a"), SimilarSongId = Guid.Parse("b26a4472-db66-4bec-926d-bb53f31083c2") },
                            new() { SongId = Guid.Parse("19dc1564-8c00-4377-95db-16535a80610a"), SimilarSongId = Guid.Parse("e91a010d-0fa4-4801-bee5-1974e87ab3d2") }],
            CreatedAt = new DateTime(2024, 6, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b26a4472-db66-4bec-926d-bb53f31083c2"),
            Title = "Instant Crush",
            ImageUrl = "https://instant_crush.jpg",
            Genre = Genre.Electronic,
            ReleaseDate = new DateOnly(2013, 5, 25),
            DurationSeconds = 237,
            Album = _albums[3],
            Artists = [_artists[7]],
            SimilarSongs = [new() { SongId = Guid.Parse("b26a4472-db66-4bec-926d-bb53f31083c2"), SimilarSongId = Guid.Parse("e91a010d-0fa4-4801-bee5-1974e87ab3d2") },
                            new() { SongId = Guid.Parse("b26a4472-db66-4bec-926d-bb53f31083c2"), SimilarSongId = Guid.Parse("469b6456-1157-43da-a275-88c983fcee9d") }],
            CreatedAt = new DateTime(2024, 6, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e91a010d-0fa4-4801-bee5-1974e87ab3d2"),
            Title = "Lose Yourself to Dance",
            ImageUrl = "https://lose_yourself_to_dance.jpg",
            Genre = Genre.Electronic,
            ReleaseDate = new DateOnly(2013, 5, 27),
            DurationSeconds = 254,
            Album = _albums[3],
            Artists = [_artists[7]],
            SimilarSongs = [new() { SongId = Guid.Parse("e91a010d-0fa4-4801-bee5-1974e87ab3d2"), SimilarSongId = Guid.Parse("469b6456-1157-43da-a275-88c983fcee9d") },
                            new() { SongId = Guid.Parse("e91a010d-0fa4-4801-bee5-1974e87ab3d2"), SimilarSongId = Guid.Parse("c23a762d-0f9f-43b9-8a6a-7a34421ee042") }],
            CreatedAt = new DateTime(2024, 6, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("469b6456-1157-43da-a275-88c983fcee9d"),
            Title = "Streets on Fire",
            ImageUrl = "https://streets_on_fire.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2012, 10, 22),
            DurationSeconds = 222,
            Album = _albums[4],
            Artists = [_artists[8]],
            SimilarSongs = [new() { SongId = Guid.Parse("469b6456-1157-43da-a275-88c983fcee9d"), SimilarSongId = Guid.Parse("c23a762d-0f9f-43b9-8a6a-7a34421ee042") },
                            new() { SongId = Guid.Parse("469b6456-1157-43da-a275-88c983fcee9d"), SimilarSongId = Guid.Parse("80b291d4-5306-4879-8aa1-fec2ea4b6516") }],
            CreatedAt = new DateTime(2024, 6, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c23a762d-0f9f-43b9-8a6a-7a34421ee042"),
            Title = "The Prelude",
            ImageUrl = "https://the_prelude.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2012, 10, 22),
            DurationSeconds = 210,
            Album = _albums[4],
            Artists = [_artists[8]],
            SimilarSongs = [new() { SongId = Guid.Parse("c23a762d-0f9f-43b9-8a6a-7a34421ee042"), SimilarSongId = Guid.Parse("80b291d4-5306-4879-8aa1-fec2ea4b6516") },
                            new() { SongId = Guid.Parse("c23a762d-0f9f-43b9-8a6a-7a34421ee042"), SimilarSongId = Guid.Parse("81acd1bf-e6f3-44c4-ad24-c47dc22adc60") }],
            CreatedAt = new DateTime(2024, 6, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 18, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("80b291d4-5306-4879-8aa1-fec2ea4b6516"),
            Title = "Dawn Breaks",
            ImageUrl = "https://dawn_breaks.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2012, 10, 22),
            DurationSeconds = 237,
            Album = _albums[4],
            Artists = [_artists[9]],
            SimilarSongs = [new() { SongId = Guid.Parse("80b291d4-5306-4879-8aa1-fec2ea4b6516"), SimilarSongId = Guid.Parse("81acd1bf-e6f3-44c4-ad24-c47dc22adc60") },
                            new() { SongId = Guid.Parse("80b291d4-5306-4879-8aa1-fec2ea4b6516"), SimilarSongId = Guid.Parse("0e13b758-e1dc-4f5a-a481-de9ab43934f1") }],
            CreatedAt = new DateTime(2024, 6, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 19, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("81acd1bf-e6f3-44c4-ad24-c47dc22adc60"),
            Title = "City Streets",
            ImageUrl = "https://city_streets.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2012, 10, 25),
            DurationSeconds = 215,
            Album = _albums[4],
            Artists = [_artists[9]],
            SimilarSongs = [new() { SongId = Guid.Parse("81acd1bf-e6f3-44c4-ad24-c47dc22adc60"), SimilarSongId = Guid.Parse("0e13b758-e1dc-4f5a-a481-de9ab43934f1") },
                            new() { SongId = Guid.Parse("81acd1bf-e6f3-44c4-ad24-c47dc22adc60"), SimilarSongId = Guid.Parse("9acfdf82-5ffd-474d-8303-b22a2a9ce0f8") }],
            CreatedAt = new DateTime(2024, 6, 20, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 20, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("0e13b758-e1dc-4f5a-a481-de9ab43934f1"),
            Title = "Back Home",
            ImageUrl = "https://back_home.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2012, 10, 27),
            DurationSeconds = 231,
            Album = _albums[4],
            Artists = [_artists[10]],
            SimilarSongs = [new() { SongId = Guid.Parse("0e13b758-e1dc-4f5a-a481-de9ab43934f1"), SimilarSongId = Guid.Parse("9acfdf82-5ffd-474d-8303-b22a2a9ce0f8") },
                            new() { SongId = Guid.Parse("0e13b758-e1dc-4f5a-a481-de9ab43934f1"), SimilarSongId = Guid.Parse("586efb75-57ab-43ca-9b85-3bdeeae3ae19") }],
            CreatedAt = new DateTime(2024, 6, 21, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 21, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("9acfdf82-5ffd-474d-8303-b22a2a9ce0f8"),
            Title = "Compton",
            ImageUrl = "https://compton.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2012, 10, 29),
            DurationSeconds = 210,
            Album = _albums[4],
            Artists = [_artists[10]],
            SimilarSongs = [new() { SongId = Guid.Parse("9acfdf82-5ffd-474d-8303-b22a2a9ce0f8"), SimilarSongId = Guid.Parse("586efb75-57ab-43ca-9b85-3bdeeae3ae19") },
                            new() { SongId = Guid.Parse("9acfdf82-5ffd-474d-8303-b22a2a9ce0f8"), SimilarSongId = Guid.Parse("e4be062f-b594-4580-b514-d9d6cdaf2933") }],
            CreatedAt = new DateTime(2024, 6, 22, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 22, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("586efb75-57ab-43ca-9b85-3bdeeae3ae19"),
            Title = "Swimming Pools (Drank)",
            ImageUrl = "https://swimming_pools.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2012, 10, 31),
            DurationSeconds = 241,
            Album = _albums[4],
            Artists = [_artists[11]],
            SimilarSongs = [new() { SongId = Guid.Parse("586efb75-57ab-43ca-9b85-3bdeeae3ae19"), SimilarSongId = Guid.Parse("e4be062f-b594-4580-b514-d9d6cdaf2933") },
                            new() { SongId = Guid.Parse("586efb75-57ab-43ca-9b85-3bdeeae3ae19"), SimilarSongId = Guid.Parse("0500418e-bfad-4cd1-860d-994cbdc9e2df") }],
            CreatedAt = new DateTime(2024, 6, 23, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 23, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e4be062f-b594-4580-b514-d9d6cdaf2933"),
            Title = "Endless Sun",
            ImageUrl = "https://endless_sun.jpg",
            Genre = Genre.Rnb,
            ReleaseDate = new DateOnly(2012, 7, 10),
            DurationSeconds = 248,
            Album = _albums[5],
            Artists = [_artists[11]],
            SimilarSongs = [new() { SongId = Guid.Parse("e4be062f-b594-4580-b514-d9d6cdaf2933"), SimilarSongId = Guid.Parse("0500418e-bfad-4cd1-860d-994cbdc9e2df") },
                            new() { SongId = Guid.Parse("e4be062f-b594-4580-b514-d9d6cdaf2933"), SimilarSongId = Guid.Parse("bc81df0c-573b-4269-ac2c-2b2667967dbb") }],
            CreatedAt = new DateTime(2024, 6, 24, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 24, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("0500418e-bfad-4cd1-860d-994cbdc9e2df"),
            Title = "Silent Waves",
            ImageUrl = "https://silent_waves.jpg",
            Genre = Genre.Rnb,
            ReleaseDate = new DateOnly(2012, 7, 10),
            DurationSeconds = 264,
            Album = _albums[5],
            Artists = [_artists[12]],
            SimilarSongs = [new() { SongId = Guid.Parse("0500418e-bfad-4cd1-860d-994cbdc9e2df"), SimilarSongId = Guid.Parse("bc81df0c-573b-4269-ac2c-2b2667967dbb") },
                            new() { SongId = Guid.Parse("0500418e-bfad-4cd1-860d-994cbdc9e2df"), SimilarSongId = Guid.Parse("866d5272-594d-423d-b702-cfccbe7a8e44") }],
            CreatedAt = new DateTime(2024, 6, 25, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 25, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("bc81df0c-573b-4269-ac2c-2b2667967dbb"),
            Title = "Ocean Avenue",
            ImageUrl = "https://ocean_avenue.jpg",
            Genre = Genre.Rnb,
            ReleaseDate = new DateOnly(2012, 7, 10),
            DurationSeconds = 212,
            Album = _albums[5],
            Artists = [_artists[12]],
            SimilarSongs = [new() { SongId = Guid.Parse("bc81df0c-573b-4269-ac2c-2b2667967dbb"), SimilarSongId = Guid.Parse("866d5272-594d-423d-b702-cfccbe7a8e44") },
                            new() { SongId = Guid.Parse("bc81df0c-573b-4269-ac2c-2b2667967dbb"), SimilarSongId = Guid.Parse("5231566f-373d-46a2-af0f-62e1a9ea643b") }],
            CreatedAt = new DateTime(2024, 6, 26, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 26, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("866d5272-594d-423d-b702-cfccbe7a8e44"),
            Title = "Pyramid Nights",
            ImageUrl = "https://pyramid_nights.jpg",
            Genre = Genre.Rnb,
            ReleaseDate = new DateOnly(2012, 7, 12),
            DurationSeconds = 212,
            Album = _albums[5],
            Artists = [_artists[13]],
            SimilarSongs = [new() { SongId = Guid.Parse("866d5272-594d-423d-b702-cfccbe7a8e44"), SimilarSongId = Guid.Parse("5231566f-373d-46a2-af0f-62e1a9ea643b") },
                            new() { SongId = Guid.Parse("866d5272-594d-423d-b702-cfccbe7a8e44"), SimilarSongId = Guid.Parse("70eb9a4b-be40-4b01-8534-796bb5f02d90") }],
            CreatedAt = new DateTime(2024, 6, 27, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 27, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("5231566f-373d-46a2-af0f-62e1a9ea643b"),
            Title = "Sweet Life",
            ImageUrl = "https://sweet_life.jpg",
            Genre = Genre.Rnb,
            ReleaseDate = new DateOnly(2012, 7, 14),
            DurationSeconds = 288,
            Album = _albums[5],
            Artists = [_artists[13]],
            SimilarSongs = [new() { SongId = Guid.Parse("5231566f-373d-46a2-af0f-62e1a9ea643b"), SimilarSongId = Guid.Parse("70eb9a4b-be40-4b01-8534-796bb5f02d90") },
                            new() { SongId = Guid.Parse("5231566f-373d-46a2-af0f-62e1a9ea643b"), SimilarSongId = Guid.Parse("bb5f149f-8e45-455c-91d1-97639e96f671") }],
            CreatedAt = new DateTime(2024, 6, 28, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 28, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("70eb9a4b-be40-4b01-8534-796bb5f02d90"),
            Title = "Lost",
            ImageUrl = "https://lost.jpg",
            Genre = Genre.Rnb,
            ReleaseDate = new DateOnly(2012, 7, 16),
            DurationSeconds = 234,
            Album = _albums[5],
            Artists = [_artists[14]],
            SimilarSongs = [new() { SongId = Guid.Parse("70eb9a4b-be40-4b01-8534-796bb5f02d90"), SimilarSongId = Guid.Parse("bb5f149f-8e45-455c-91d1-97639e96f671") },
                            new() { SongId = Guid.Parse("70eb9a4b-be40-4b01-8534-796bb5f02d90"), SimilarSongId = Guid.Parse("4a88b128-2471-4f25-b51a-136363ddbe7d") }],
            CreatedAt = new DateTime(2024, 6, 29, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 29, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("bb5f149f-8e45-455c-91d1-97639e96f671"),
            Title = "Super Rich Kids",
            ImageUrl = "https://super_rich_kids.jpg",
            Genre = Genre.Rnb,
            ReleaseDate = new DateOnly(2012, 7, 18),
            DurationSeconds = 203,
            Album = _albums[5],
            Artists = [_artists[14]],
            SimilarSongs = [new() { SongId = Guid.Parse("bb5f149f-8e45-455c-91d1-97639e96f671"), SimilarSongId = Guid.Parse("4a88b128-2471-4f25-b51a-136363ddbe7d") },
                            new() { SongId = Guid.Parse("bb5f149f-8e45-455c-91d1-97639e96f671"), SimilarSongId = Guid.Parse("84b69406-d92e-4feb-a313-f1f373f1958c") }],
            CreatedAt = new DateTime(2024, 6, 30, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 6, 30, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("4a88b128-2471-4f25-b51a-136363ddbe7d"),
            Title = "Light House",
            ImageUrl = "https://light_house.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2014, 6, 5),
            DurationSeconds = 242,
            Album = _albums[6],
            Artists = [_artists[15]],
            SimilarSongs = [new() { SongId = Guid.Parse("4a88b128-2471-4f25-b51a-136363ddbe7d"), SimilarSongId = Guid.Parse("84b69406-d92e-4feb-a313-f1f373f1958c") },
                            new() { SongId = Guid.Parse("4a88b128-2471-4f25-b51a-136363ddbe7d"), SimilarSongId = Guid.Parse("ded148fc-19d8-478a-9060-0b4543727d37") }],
            CreatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("84b69406-d92e-4feb-a313-f1f373f1958c"),
            Title = "Moonlight Shadow",
            ImageUrl = "https://moonlight_shadow.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2014, 6, 7),
            DurationSeconds = 258,
            Album = _albums[6],
            Artists = [_artists[15]],
            SimilarSongs = [new() { SongId = Guid.Parse("84b69406-d92e-4feb-a313-f1f373f1958c"), SimilarSongId = Guid.Parse("ded148fc-19d8-478a-9060-0b4543727d37") },
                            new() { SongId = Guid.Parse("84b69406-d92e-4feb-a313-f1f373f1958c"), SimilarSongId = Guid.Parse("92388b4d-6ff6-4500-9b02-24d4227f3a28") }],
            CreatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("ded148fc-19d8-478a-9060-0b4543727d37"),
            Title = "Stay With Me",
            ImageUrl = "https://stay_with_me.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2014, 6, 9),
            DurationSeconds = 182,
            Album = _albums[6],
            Artists = [_artists[16]],
            SimilarSongs = [new() { SongId = Guid.Parse("ded148fc-19d8-478a-9060-0b4543727d37"), SimilarSongId = Guid.Parse("92388b4d-6ff6-4500-9b02-24d4227f3a28") },
                            new() { SongId = Guid.Parse("ded148fc-19d8-478a-9060-0b4543727d37"), SimilarSongId = Guid.Parse("e1817fb5-5fb6-44aa-abbf-eda52cc578d7") }],
            CreatedAt = new DateTime(2024, 7, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("92388b4d-6ff6-4500-9b02-24d4227f3a28"),
            Title = "Leave Your Lover",
            ImageUrl = "https://leave_your_lover.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2014, 6, 11),
            DurationSeconds = 206,
            Album = _albums[6],
            Artists = [_artists[16]],
            SimilarSongs = [new() { SongId = Guid.Parse("92388b4d-6ff6-4500-9b02-24d4227f3a28"), SimilarSongId = Guid.Parse("e1817fb5-5fb6-44aa-abbf-eda52cc578d7") },
                            new() { SongId = Guid.Parse("92388b4d-6ff6-4500-9b02-24d4227f3a28"), SimilarSongId = Guid.Parse("9a5f706f-ae38-418d-b911-77559d20e076") }],
            CreatedAt = new DateTime(2024, 7, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e1817fb5-5fb6-44aa-abbf-eda52cc578d7"),
            Title = "Eclipse Phase",
            ImageUrl = "https://eclipse_phase.jpg",
            Genre = Genre.Rock,
            ReleaseDate = new DateOnly(1973, 4, 1),
            DurationSeconds = 212,
            Album = _albums[7],
            Artists = [_artists[17]],
            SimilarSongs = [new() { SongId = Guid.Parse("e1817fb5-5fb6-44aa-abbf-eda52cc578d7"), SimilarSongId = Guid.Parse("9a5f706f-ae38-418d-b911-77559d20e076") },
                            new() { SongId = Guid.Parse("e1817fb5-5fb6-44aa-abbf-eda52cc578d7"), SimilarSongId = Guid.Parse("01db55cc-7d06-4778-b9c1-7ccdc3e3cd13") }],
            CreatedAt = new DateTime(2024, 7, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("9a5f706f-ae38-418d-b911-77559d20e076"),
            Title = "Moons Dark Side",
            ImageUrl = "https://moons_dark_side.jpg",
            Genre = Genre.Rock,
            ReleaseDate = new DateOnly(1973, 4, 3),
            DurationSeconds = 295,
            Album = _albums[7],
            Artists = [_artists[17]],
            SimilarSongs = [new() { SongId = Guid.Parse("9a5f706f-ae38-418d-b911-77559d20e076"), SimilarSongId = Guid.Parse("01db55cc-7d06-4778-b9c1-7ccdc3e3cd13") },
                            new() { SongId = Guid.Parse("9a5f706f-ae38-418d-b911-77559d20e076"), SimilarSongId = Guid.Parse("f80a900f-e0a5-4cc3-8adf-27c6309b81ca") }],
            CreatedAt = new DateTime(2024, 7, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("01db55cc-7d06-4778-b9c1-7ccdc3e3cd13"),
            Title = "Time",
            ImageUrl = "https://time.jpg",
            Genre = Genre.Rock,
            ReleaseDate = new DateOnly(1973, 4, 5),
            DurationSeconds = 208,
            Album = _albums[7],
            Artists = [_artists[18]],
            SimilarSongs = [new() { SongId = Guid.Parse("01db55cc-7d06-4778-b9c1-7ccdc3e3cd13"), SimilarSongId = Guid.Parse("f80a900f-e0a5-4cc3-8adf-27c6309b81ca") },
                            new() { SongId = Guid.Parse("01db55cc-7d06-4778-b9c1-7ccdc3e3cd13"), SimilarSongId = Guid.Parse("d6a31188-c1b8-4976-b372-aced401f2347") }],
            CreatedAt = new DateTime(2024, 7, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f80a900f-e0a5-4cc3-8adf-27c6309b81ca"),
            Title = "The Great Gig in the Sky",
            ImageUrl = "https://great_gig_in_the_sky.jpg",
            Genre = Genre.Rock,
            ReleaseDate = new DateOnly(1973, 4, 7),
            DurationSeconds = 276,
            Album = _albums[7],
            Artists = [_artists[18]],
            SimilarSongs = [new() { SongId = Guid.Parse("f80a900f-e0a5-4cc3-8adf-27c6309b81ca"), SimilarSongId = Guid.Parse("d6a31188-c1b8-4976-b372-aced401f2347") },
                            new() { SongId = Guid.Parse("f80a900f-e0a5-4cc3-8adf-27c6309b81ca"), SimilarSongId = Guid.Parse("8f0191bc-a242-4cb0-a9ba-38f48e823e54") }],
            CreatedAt = new DateTime(2024, 7, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d6a31188-c1b8-4976-b372-aced401f2347"),
            Title = "Stardust Memories",
            ImageUrl = "https://stardust_memories.jpg",
            Genre = Genre.Rock,
            ReleaseDate = new DateOnly(2016, 1, 10),
            DurationSeconds = 289,
            Album = _albums[9],
            Artists = [_artists[19]],
            SimilarSongs = [new() { SongId = Guid.Parse("d6a31188-c1b8-4976-b372-aced401f2347"), SimilarSongId = Guid.Parse("8f0191bc-a242-4cb0-a9ba-38f48e823e54") },
                            new() { SongId = Guid.Parse("d6a31188-c1b8-4976-b372-aced401f2347"), SimilarSongId = Guid.Parse("c80e5abb-3759-49eb-8cbe-c2b7ff742072") }],
            CreatedAt = new DateTime(2024, 7, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("8f0191bc-a242-4cb0-a9ba-38f48e823e54"),
            Title = "Black Skies",
            ImageUrl = "https://black_skies.jpg",
            Genre = Genre.Rock,
            ReleaseDate = new DateOnly(2016, 1, 12),
            DurationSeconds = 205,
            Album = _albums[9],
            Artists = [_artists[19]],
            SimilarSongs = [new() { SongId = Guid.Parse("8f0191bc-a242-4cb0-a9ba-38f48e823e54"), SimilarSongId = Guid.Parse("c80e5abb-3759-49eb-8cbe-c2b7ff742072") },
                            new() { SongId = Guid.Parse("8f0191bc-a242-4cb0-a9ba-38f48e823e54"), SimilarSongId = Guid.Parse("f1647832-8eb1-460d-a5ae-c9fac5e2cd5d") }],
            CreatedAt = new DateTime(2024, 7, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c80e5abb-3759-49eb-8cbe-c2b7ff742072"),
            Title = "Lazarus",
            ImageUrl = "https://lazarus.jpg",
            Genre = Genre.Rock,
            ReleaseDate = new DateOnly(2016, 1, 14),
            DurationSeconds = 264,
            Album = _albums[9],
            Artists = [_artists[20]],
            SimilarSongs = [new() { SongId = Guid.Parse("c80e5abb-3759-49eb-8cbe-c2b7ff742072"), SimilarSongId = Guid.Parse("f1647832-8eb1-460d-a5ae-c9fac5e2cd5d") },
                            new() { SongId = Guid.Parse("c80e5abb-3759-49eb-8cbe-c2b7ff742072"), SimilarSongId = Guid.Parse("e3d3e750-7179-4189-9c19-fd546c4493c5") }],
            CreatedAt = new DateTime(2024, 7, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f1647832-8eb1-460d-a5ae-c9fac5e2cd5d"),
            Title = "I Can't Give Everything Away",
            ImageUrl = "https://i_cant_give_everything_away.jpg",
            Genre = Genre.Rock,
            ReleaseDate = new DateOnly(2016, 1, 16),
            DurationSeconds = 249,
            Album = _albums[9],
            Artists = [_artists[20]],
            SimilarSongs = [new() { SongId = Guid.Parse("f1647832-8eb1-460d-a5ae-c9fac5e2cd5d"), SimilarSongId = Guid.Parse("e3d3e750-7179-4189-9c19-fd546c4493c5") },
                            new() { SongId = Guid.Parse("f1647832-8eb1-460d-a5ae-c9fac5e2cd5d"), SimilarSongId = Guid.Parse("2a14abb5-4eea-46e2-bb70-2cf907acbf09") }],
            CreatedAt = new DateTime(2024, 7, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e3d3e750-7179-4189-9c19-fd546c4493c5"),
            Title = "New Romantics",
            ImageUrl = "https://new_romantics.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2017, 11, 16),
            DurationSeconds = 234,
            Album = _albums[49],
            Artists = [_artists[21]],
            SimilarSongs = [new() { SongId = Guid.Parse("e3d3e750-7179-4189-9c19-fd546c4493c5"), SimilarSongId = Guid.Parse("2a14abb5-4eea-46e2-bb70-2cf907acbf09") },
                            new() { SongId = Guid.Parse("e3d3e750-7179-4189-9c19-fd546c4493c5"), SimilarSongId = Guid.Parse("8f764924-0f7a-49e0-a39d-d29f9c3b1161") }],
            CreatedAt = new DateTime(2024, 7, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2a14abb5-4eea-46e2-bb70-2cf907acbf09"),
            Title = "End Game",
            ImageUrl = "https://end_game.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2017, 11, 18),
            DurationSeconds = 245,
            Album = _albums[49],
            Artists = [_artists[21]],
            SimilarSongs = [new() { SongId = Guid.Parse("2a14abb5-4eea-46e2-bb70-2cf907acbf09"), SimilarSongId = Guid.Parse("8f764924-0f7a-49e0-a39d-d29f9c3b1161") },
                            new() { SongId = Guid.Parse("2a14abb5-4eea-46e2-bb70-2cf907acbf09"), SimilarSongId = Guid.Parse("2dec0615-0284-4260-a0f8-44baa2954bc4") }],
            CreatedAt = new DateTime(2024, 7, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("8f764924-0f7a-49e0-a39d-d29f9c3b1161"),
            Title = "Delicate",
            ImageUrl = "https://delicate.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2017, 11, 20),
            DurationSeconds = 228,
            Album = _albums[49],
            Artists = [_artists[22]],
            SimilarSongs = [new() { SongId = Guid.Parse("8f764924-0f7a-49e0-a39d-d29f9c3b1161"), SimilarSongId = Guid.Parse("2dec0615-0284-4260-a0f8-44baa2954bc4") },
                            new() { SongId = Guid.Parse("8f764924-0f7a-49e0-a39d-d29f9c3b1161"), SimilarSongId = Guid.Parse("55bce552-42e3-4ae9-96d2-6df192f2ac50") }],
            CreatedAt = new DateTime(2024, 7, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2dec0615-0284-4260-a0f8-44baa2954bc4"),
            Title = "Echoes of Glory",
            ImageUrl = "https://echoes_of_glory.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2017, 11, 12),
            DurationSeconds = 228,
            Album = _albums[49],
            Artists = [_artists[22]],
            SimilarSongs = [new() { SongId = Guid.Parse("2dec0615-0284-4260-a0f8-44baa2954bc4"), SimilarSongId = Guid.Parse("55bce552-42e3-4ae9-96d2-6df192f2ac50") },
                            new() { SongId = Guid.Parse("2dec0615-0284-4260-a0f8-44baa2954bc4"), SimilarSongId = Guid.Parse("7aaaf887-d5e5-47b6-b14b-6dfb7e423ea8") }],
            CreatedAt = new DateTime(2024, 7, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("55bce552-42e3-4ae9-96d2-6df192f2ac50"),
            Title = "Silent Screams",
            ImageUrl = "https://silent_screams.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2017, 11, 14),
            DurationSeconds = 214,
            Album = _albums[49],
            Artists = [_artists[23]],
            SimilarSongs = [new() { SongId = Guid.Parse("55bce552-42e3-4ae9-96d2-6df192f2ac50"), SimilarSongId = Guid.Parse("7aaaf887-d5e5-47b6-b14b-6dfb7e423ea8") },
                            new() { SongId = Guid.Parse("55bce552-42e3-4ae9-96d2-6df192f2ac50"), SimilarSongId = Guid.Parse("f7a5b648-8efe-41a8-8407-a9a76d8eb6fc") }],
            CreatedAt = new DateTime(2024, 7, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7aaaf887-d5e5-47b6-b14b-6dfb7e423ea8"),
            Title = "Watermelon Sugar",
            ImageUrl = "https://watermelon_sugar.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2019, 12, 15),
            DurationSeconds = 174,
            Album = _albums[10],
            Artists = [_artists[23]],
            SimilarSongs = [new() { SongId = Guid.Parse("7aaaf887-d5e5-47b6-b14b-6dfb7e423ea8"), SimilarSongId = Guid.Parse("f7a5b648-8efe-41a8-8407-a9a76d8eb6fc") },
                            new() { SongId = Guid.Parse("7aaaf887-d5e5-47b6-b14b-6dfb7e423ea8"), SimilarSongId = Guid.Parse("25d27dd1-8add-421e-93e0-ba7d964ff990") }],
            CreatedAt = new DateTime(2024, 7, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 18, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f7a5b648-8efe-41a8-8407-a9a76d8eb6fc"),
            Title = "Golden Days",
            ImageUrl = "https://golden_days.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2019, 12, 17),
            DurationSeconds = 210,
            Album = _albums[10],
            Artists = [_artists[24]],
            SimilarSongs = [new() { SongId = Guid.Parse("f7a5b648-8efe-41a8-8407-a9a76d8eb6fc"), SimilarSongId = Guid.Parse("25d27dd1-8add-421e-93e0-ba7d964ff990") },
                            new() { SongId = Guid.Parse("f7a5b648-8efe-41a8-8407-a9a76d8eb6fc"), SimilarSongId = Guid.Parse("93218e81-668d-4697-ab5b-e9e04cc9732d") }],
            CreatedAt = new DateTime(2024, 7, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 19, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("25d27dd1-8add-421e-93e0-ba7d964ff990"),
            Title = "Hold Up",
            ImageUrl = "https://holdup.jpg",
            Genre = Genre.Rnb,
            ReleaseDate = new DateOnly(2019, 12, 18),
            DurationSeconds = 251,
            Album = _albums[10],
            Artists = [_artists[24]],
            SimilarSongs = [new() { SongId = Guid.Parse("25d27dd1-8add-421e-93e0-ba7d964ff990"), SimilarSongId = Guid.Parse("93218e81-668d-4697-ab5b-e9e04cc9732d") },
                            new() { SongId = Guid.Parse("25d27dd1-8add-421e-93e0-ba7d964ff990"), SimilarSongId = Guid.Parse("3e5f72f3-c2f9-4771-99c8-5a5ced274ed7") }],
            CreatedAt = new DateTime(2024, 7, 20, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 20, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("93218e81-668d-4697-ab5b-e9e04cc9732d"),
            Title = "Freedom",
            ImageUrl = "https://freedom.jpg",
            Genre = Genre.Rnb,
            ReleaseDate = new DateOnly(2019, 12, 18),
            DurationSeconds = 274,
            Album = _albums[10],
            Artists = [_artists[25]],
            SimilarSongs = [new() { SongId = Guid.Parse("93218e81-668d-4697-ab5b-e9e04cc9732d"), SimilarSongId = Guid.Parse("3e5f72f3-c2f9-4771-99c8-5a5ced274ed7") },
                            new() { SongId = Guid.Parse("93218e81-668d-4697-ab5b-e9e04cc9732d"), SimilarSongId = Guid.Parse("c00dd550-41aa-41e3-890e-abf3c937e62f") }],
            CreatedAt = new DateTime(2024, 7, 21, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 21, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3e5f72f3-c2f9-4771-99c8-5a5ced274ed7"),
            Title = "Falling",
            ImageUrl = "https://falling.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2019, 12, 19),
            DurationSeconds = 240,
            Album = _albums[10],
            Artists = [_artists[25]],
            SimilarSongs = [new() { SongId = Guid.Parse("3e5f72f3-c2f9-4771-99c8-5a5ced274ed7"), SimilarSongId = Guid.Parse("c00dd550-41aa-41e3-890e-abf3c937e62f") },
                            new() { SongId = Guid.Parse("3e5f72f3-c2f9-4771-99c8-5a5ced274ed7"), SimilarSongId = Guid.Parse("bd5e6040-ba62-4a5d-aeda-ba81f6f46eea") }],
            CreatedAt = new DateTime(2024, 7, 22, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 22, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c00dd550-41aa-41e3-890e-abf3c937e62f"),
            Title = "She",
            ImageUrl = "https://she.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2019, 12, 21),
            DurationSeconds = 295,
            Album = _albums[10],
            Artists = [_artists[26]],
            SimilarSongs = [new() { SongId = Guid.Parse("c00dd550-41aa-41e3-890e-abf3c937e62f"), SimilarSongId = Guid.Parse("bd5e6040-ba62-4a5d-aeda-ba81f6f46eea") },
                            new() { SongId = Guid.Parse("c00dd550-41aa-41e3-890e-abf3c937e62f"), SimilarSongId = Guid.Parse("448f0c35-54b1-4bd3-9a37-24b350b50d0f") }],
            CreatedAt = new DateTime(2024, 7, 23, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 23, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("bd5e6040-ba62-4a5d-aeda-ba81f6f46eea"),
            Title = "Rolling in the Deep",
            ImageUrl = "https://rolling_in_the_deep.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2011, 1, 29),
            DurationSeconds = 228,
            Album = _albums[16],
            Artists = [_artists[26]],
            SimilarSongs = [new() { SongId = Guid.Parse("bd5e6040-ba62-4a5d-aeda-ba81f6f46eea"), SimilarSongId = Guid.Parse("448f0c35-54b1-4bd3-9a37-24b350b50d0f") },
                            new() { SongId = Guid.Parse("bd5e6040-ba62-4a5d-aeda-ba81f6f46eea"), SimilarSongId = Guid.Parse("835cb2dd-fb45-4605-9445-a2d14d2fba7b") }],
            CreatedAt = new DateTime(2024, 7, 24, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 24, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("448f0c35-54b1-4bd3-9a37-24b350b50d0f"),
            Title = "Someone Like You",
            ImageUrl = "https://someone_like_you.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2011, 1, 31),
            DurationSeconds = 286,
            Album = _albums[16],
            Artists = [_artists[27]],
            SimilarSongs = [new() { SongId = Guid.Parse("448f0c35-54b1-4bd3-9a37-24b350b50d0f"), SimilarSongId = Guid.Parse("835cb2dd-fb45-4605-9445-a2d14d2fba7b") },
                            new() { SongId = Guid.Parse("448f0c35-54b1-4bd3-9a37-24b350b50d0f"), SimilarSongId = Guid.Parse("d38a98c4-5cc7-45a2-93c0-4d03ff9cb496") }],
            CreatedAt = new DateTime(2024, 7, 25, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 25, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("835cb2dd-fb45-4605-9445-a2d14d2fba7b"),
            Title = "Set Fire to the Rain",
            ImageUrl = "https://set_fire_to_the_rain.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2011, 1, 28),
            DurationSeconds = 242,
            Album = _albums[16],
            Artists = [_artists[27]],
            SimilarSongs = [new() { SongId = Guid.Parse("835cb2dd-fb45-4605-9445-a2d14d2fba7b"), SimilarSongId = Guid.Parse("d38a98c4-5cc7-45a2-93c0-4d03ff9cb496") },
                            new() { SongId = Guid.Parse("835cb2dd-fb45-4605-9445-a2d14d2fba7b"), SimilarSongId = Guid.Parse("34531668-8c68-4ebd-a6a7-36645e9bac97") }],
            CreatedAt = new DateTime(2024, 7, 26, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 26, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d38a98c4-5cc7-45a2-93c0-4d03ff9cb496"),
            Title = "Rumour Has It",
            ImageUrl = "https://rumour_has_it.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2011, 1, 30),
            DurationSeconds = 223,
            Album = _albums[16],
            Artists = [_artists[28]],
            SimilarSongs = [new() { SongId = Guid.Parse("d38a98c4-5cc7-45a2-93c0-4d03ff9cb496"), SimilarSongId = Guid.Parse("34531668-8c68-4ebd-a6a7-36645e9bac97") },
                            new() { SongId = Guid.Parse("d38a98c4-5cc7-45a2-93c0-4d03ff9cb496"), SimilarSongId = Guid.Parse("f2c126f4-0b32-46b1-a293-e4d53fd70d0f") }],
            CreatedAt = new DateTime(2024, 7, 27, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 27, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("34531668-8c68-4ebd-a6a7-36645e9bac97"),
            Title = "Started From the Bottom",
            ImageUrl = "https://started_from_the_bottom.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2013, 9, 26),
            DurationSeconds = 197,
            Album = _albums[26],
            Artists = [_artists[28]],
            SimilarSongs = [new() { SongId = Guid.Parse("34531668-8c68-4ebd-a6a7-36645e9bac97"), SimilarSongId = Guid.Parse("f2c126f4-0b32-46b1-a293-e4d53fd70d0f") },
                            new() { SongId = Guid.Parse("34531668-8c68-4ebd-a6a7-36645e9bac97"), SimilarSongId = Guid.Parse("095f7293-c7b5-4c29-9112-4aa24a8c063a") }],
            CreatedAt = new DateTime(2024, 7, 28, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 28, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f2c126f4-0b32-46b1-a293-e4d53fd70d0f"),
            Title = "Hold On, We’re Going Home",
            ImageUrl = "https://hold_on_were_going_home.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2013, 9, 28),
            DurationSeconds = 231,
            Album = _albums[26],
            Artists = [_artists[29]],
            SimilarSongs = [new() { SongId = Guid.Parse("f2c126f4-0b32-46b1-a293-e4d53fd70d0f"), SimilarSongId = Guid.Parse("095f7293-c7b5-4c29-9112-4aa24a8c063a") },
                            new() { SongId = Guid.Parse("f2c126f4-0b32-46b1-a293-e4d53fd70d0f"), SimilarSongId = Guid.Parse("1a4c8801-de6e-4787-b940-69ec4b9e8ad1") }],
            CreatedAt = new DateTime(2024, 7, 29, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 29, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("095f7293-c7b5-4c29-9112-4aa24a8c063a"),
            Title = "Too Much",
            ImageUrl = "https://too_much.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2013, 9, 30),
            DurationSeconds = 250,
            Album = _albums[26],
            Artists = [_artists[29]],
            SimilarSongs = [new() { SongId = Guid.Parse("095f7293-c7b5-4c29-9112-4aa24a8c063a"), SimilarSongId = Guid.Parse("1a4c8801-de6e-4787-b940-69ec4b9e8ad1") },
                            new() { SongId = Guid.Parse("095f7293-c7b5-4c29-9112-4aa24a8c063a"), SimilarSongId = Guid.Parse("a1f09d89-9f92-4531-ae83-7d595da08138") }],
            CreatedAt = new DateTime(2024, 7, 30, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 30, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("1a4c8801-de6e-4787-b940-69ec4b9e8ad1"),
            Title = "Wu-Tang Forever",
            ImageUrl = "https://wutang_forever.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2013, 10, 2),
            DurationSeconds = 210,
            Album = _albums[26],
            Artists = [_artists[30]],
            SimilarSongs = [new() { SongId = Guid.Parse("1a4c8801-de6e-4787-b940-69ec4b9e8ad1"), SimilarSongId = Guid.Parse("a1f09d89-9f92-4531-ae83-7d595da08138") },
                            new() { SongId = Guid.Parse("1a4c8801-de6e-4787-b940-69ec4b9e8ad1"), SimilarSongId = Guid.Parse("923182ab-0058-40ea-a878-f6ba3dad4f74") }],
            CreatedAt = new DateTime(2024, 7, 31, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 31, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a1f09d89-9f92-4531-ae83-7d595da08138"),
            Title = "Work",
            ImageUrl = "https://work.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2016, 1, 30),
            DurationSeconds = 213,
            Album = _albums[27],
            Artists = [_artists[30]],
            SimilarSongs = [new() { SongId = Guid.Parse("a1f09d89-9f92-4531-ae83-7d595da08138"), SimilarSongId = Guid.Parse("923182ab-0058-40ea-a878-f6ba3dad4f74") },
                            new() { SongId = Guid.Parse("a1f09d89-9f92-4531-ae83-7d595da08138"), SimilarSongId = Guid.Parse("3c15b1af-8b00-4356-ae83-873a553d99c6") }],
            CreatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("923182ab-0058-40ea-a878-f6ba3dad4f74"),
            Title = "Desperado",
            ImageUrl = "https://desperado.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2016, 2, 1),
            DurationSeconds = 192,
            Album = _albums[27],
            Artists = [_artists[31]],
            SimilarSongs = [new() { SongId = Guid.Parse("923182ab-0058-40ea-a878-f6ba3dad4f74"), SimilarSongId = Guid.Parse("3c15b1af-8b00-4356-ae83-873a553d99c6") },
                            new() { SongId = Guid.Parse("923182ab-0058-40ea-a878-f6ba3dad4f74"), SimilarSongId = Guid.Parse("3dab7f91-33f3-4271-a2ff-d9b5eb232068") }],
            CreatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3c15b1af-8b00-4356-ae83-873a553d99c6"),
            Title = "Kiss It Better",
            ImageUrl = "https://kiss_it_better.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2016, 1, 28),
            DurationSeconds = 242,
            Album = _albums[27],
            Artists = [_artists[31]],
            SimilarSongs = [new() { SongId = Guid.Parse("3c15b1af-8b00-4356-ae83-873a553d99c6"), SimilarSongId = Guid.Parse("3dab7f91-33f3-4271-a2ff-d9b5eb232068") },
                            new() { SongId = Guid.Parse("3c15b1af-8b00-4356-ae83-873a553d99c6"), SimilarSongId = Guid.Parse("74316db6-7ad9-4570-b837-136944e986ad") }],
            CreatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3dab7f91-33f3-4271-a2ff-d9b5eb232068"),
            Title = "Love on the Brain",
            ImageUrl = "https://love_on_the_brain.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2016, 1, 30),
            DurationSeconds = 228,
            Album = _albums[27],
            Artists = [_artists[32]],
            SimilarSongs = [new() { SongId = Guid.Parse("3dab7f91-33f3-4271-a2ff-d9b5eb232068"), SimilarSongId = Guid.Parse("74316db6-7ad9-4570-b837-136944e986ad") },
                            new() { SongId = Guid.Parse("3dab7f91-33f3-4271-a2ff-d9b5eb232068"), SimilarSongId = Guid.Parse("4cf28277-1496-4420-ac8a-9c24e1e41181") }],
            CreatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("74316db6-7ad9-4570-b837-136944e986ad"),
            Title = "Four Out Of Five",
            ImageUrl = "https://four_out_of_five.jpg",
            Genre = Genre.Rock,
            ReleaseDate = new DateOnly(2018, 5, 13),
            DurationSeconds = 275,
            Album = _albums[32],
            Artists = [_artists[32]],
            SimilarSongs = [new() { SongId = Guid.Parse("74316db6-7ad9-4570-b837-136944e986ad"), SimilarSongId = Guid.Parse("4cf28277-1496-4420-ac8a-9c24e1e41181") },
                            new() { SongId = Guid.Parse("74316db6-7ad9-4570-b837-136944e986ad"), SimilarSongId = Guid.Parse("9a9e7afb-d32b-4975-a7ed-d0174761e6e7") }],
            CreatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("4cf28277-1496-4420-ac8a-9c24e1e41181"),
            Title = "One Point Perspective",
            ImageUrl = "https://one_point_perspective.jpg",
            Genre = Genre.Rock,
            ReleaseDate = new DateOnly(2018, 5, 15),
            DurationSeconds = 243,
            Album = _albums[32],
            Artists = [_artists[33]],
            SimilarSongs = [new() { SongId = Guid.Parse("4cf28277-1496-4420-ac8a-9c24e1e41181"), SimilarSongId = Guid.Parse("9a9e7afb-d32b-4975-a7ed-d0174761e6e7") },
                            new() { SongId = Guid.Parse("4cf28277-1496-4420-ac8a-9c24e1e41181"), SimilarSongId = Guid.Parse("ad372f33-1ace-46da-b3ef-f9156398a019") }],
            CreatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("9a9e7afb-d32b-4975-a7ed-d0174761e6e7"),
            Title = "Star Treatment",
            ImageUrl = "https://star_treatment.jpg",
            Genre = Genre.Rock,
            ReleaseDate = new DateOnly(2018, 5, 11),
            DurationSeconds = 245,
            Album = _albums[32],
            Artists = [_artists[33]],
            SimilarSongs = [new() { SongId = Guid.Parse("9a9e7afb-d32b-4975-a7ed-d0174761e6e7"), SimilarSongId = Guid.Parse("ad372f33-1ace-46da-b3ef-f9156398a019") },
                            new() { SongId = Guid.Parse("9a9e7afb-d32b-4975-a7ed-d0174761e6e7"), SimilarSongId = Guid.Parse("bc795075-07ac-4458-a8a2-56b7d8ab0437") }],
            CreatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("ad372f33-1ace-46da-b3ef-f9156398a019"),
            Title = "Batphone",
            ImageUrl = "https://batphone.jpg",
            Genre = Genre.Rock,
            ReleaseDate = new DateOnly(2018, 5, 13),
            DurationSeconds = 200,
            Album = _albums[32],
            Artists = [_artists[34]],
            SimilarSongs = [new() { SongId = Guid.Parse("ad372f33-1ace-46da-b3ef-f9156398a019"), SimilarSongId = Guid.Parse("bc795075-07ac-4458-a8a2-56b7d8ab0437") },
                            new() { SongId = Guid.Parse("ad372f33-1ace-46da-b3ef-f9156398a019"), SimilarSongId = Guid.Parse("72c4be16-5b12-432b-8cec-4fa162788527") }],
            CreatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("bc795075-07ac-4458-a8a2-56b7d8ab0437"),
            Title = "Paradise",
            ImageUrl = "https://paradise.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2016, 8, 20),
            DurationSeconds = 278,
            Album = _albums[23],
            Artists = [_artists[34]],
            SimilarSongs = [new() { SongId = Guid.Parse("bc795075-07ac-4458-a8a2-56b7d8ab0437"), SimilarSongId = Guid.Parse("72c4be16-5b12-432b-8cec-4fa162788527") },
                            new() { SongId = Guid.Parse("bc795075-07ac-4458-a8a2-56b7d8ab0437"), SimilarSongId = Guid.Parse("f111ef40-05d2-4e04-9856-d63fade4a12d") }],
            CreatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("72c4be16-5b12-432b-8cec-4fa162788527"),
            Title = "Moon River",
            ImageUrl = "https://moon_river.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2016, 8, 22),
            DurationSeconds = 219,
            Album = _albums[23],
            Artists = [_artists[35]],
            SimilarSongs = [new() { SongId = Guid.Parse("72c4be16-5b12-432b-8cec-4fa162788527"), SimilarSongId = Guid.Parse("f111ef40-05d2-4e04-9856-d63fade4a12d") },
                            new() { SongId = Guid.Parse("72c4be16-5b12-432b-8cec-4fa162788527"), SimilarSongId = Guid.Parse("f027a18c-3164-42a2-b49b-f299a1477798") }],
            CreatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f111ef40-05d2-4e04-9856-d63fade4a12d"),
            Title = "Pink + White",
            ImageUrl = "https://pink_white.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2016, 8, 24),
            DurationSeconds = 204,
            Album = _albums[23],
            Artists = [_artists[35]],
            SimilarSongs = [new() { SongId = Guid.Parse("f111ef40-05d2-4e04-9856-d63fade4a12d"), SimilarSongId = Guid.Parse("f027a18c-3164-42a2-b49b-f299a1477798") },
                            new() { SongId = Guid.Parse("f111ef40-05d2-4e04-9856-d63fade4a12d"), SimilarSongId = Guid.Parse("a19c9d5d-5999-4972-aca8-1990d6c854ea") }],
            CreatedAt = new DateTime(2024, 8, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f027a18c-3164-42a2-b49b-f299a1477798"),
            Title = "Nikes",
            ImageUrl = "https://nikes.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2016, 8, 26),
            DurationSeconds = 214,
            Album = _albums[23],
            Artists = [_artists[36]],
            SimilarSongs = [new() { SongId = Guid.Parse("f027a18c-3164-42a2-b49b-f299a1477798"), SimilarSongId = Guid.Parse("a19c9d5d-5999-4972-aca8-1990d6c854ea") },
                            new() { SongId = Guid.Parse("f027a18c-3164-42a2-b49b-f299a1477798"), SimilarSongId = Guid.Parse("46d680bc-036d-4e72-bb12-1d8ed212c83a") }],
            CreatedAt = new DateTime(2024, 8, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a19c9d5d-5999-4972-aca8-1990d6c854ea"),
            Title = "Solo (Reprise)",
            ImageUrl = "https://solo_reprise.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2016, 8, 21),
            DurationSeconds = 157,
            Album = _albums[23],
            Artists = [_artists[36]],
            SimilarSongs = [new() { SongId = Guid.Parse("a19c9d5d-5999-4972-aca8-1990d6c854ea"), SimilarSongId = Guid.Parse("46d680bc-036d-4e72-bb12-1d8ed212c83a") },
                            new() { SongId = Guid.Parse("a19c9d5d-5999-4972-aca8-1990d6c854ea"), SimilarSongId = Guid.Parse("8f162292-9e44-4003-97b8-86fe410c486a") }],
            CreatedAt = new DateTime(2024, 8, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("46d680bc-036d-4e72-bb12-1d8ed212c83a"),
            Title = "Green Light",
            ImageUrl = "https://green_light.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2017, 6, 16),
            DurationSeconds = 234,
            Album = _albums[25],
            Artists = [_artists[37]],
            SimilarSongs = [new() { SongId = Guid.Parse("46d680bc-036d-4e72-bb12-1d8ed212c83a"), SimilarSongId = Guid.Parse("8f162292-9e44-4003-97b8-86fe410c486a") },
                            new() { SongId = Guid.Parse("46d680bc-036d-4e72-bb12-1d8ed212c83a"), SimilarSongId = Guid.Parse("b2c5d58f-9983-48c5-8a1e-96dafdb3307c") }],
            CreatedAt = new DateTime(2024, 8, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("8f162292-9e44-4003-97b8-86fe410c486a"),
            Title = "Homemade Dynamite",
            ImageUrl = "https://homemade_dynamite.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2017, 6, 18),
            DurationSeconds = 214,
            Album = _albums[25],
            Artists = [_artists[37]],
            SimilarSongs = [new() { SongId = Guid.Parse("8f162292-9e44-4003-97b8-86fe410c486a"), SimilarSongId = Guid.Parse("b2c5d58f-9983-48c5-8a1e-96dafdb3307c") },
                            new() { SongId = Guid.Parse("8f162292-9e44-4003-97b8-86fe410c486a"), SimilarSongId = Guid.Parse("d7cc064f-70a0-43ad-bd86-8d200580d6b8") }],
            CreatedAt = new DateTime(2024, 8, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b2c5d58f-9983-48c5-8a1e-96dafdb3307c"),
            Title = "Liability",
            ImageUrl = "https://liability.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2017, 6, 20),
            DurationSeconds = 178,
            Album = _albums[25],
            Artists = [_artists[38]],
            SimilarSongs = [new() { SongId = Guid.Parse("b2c5d58f-9983-48c5-8a1e-96dafdb3307c"), SimilarSongId = Guid.Parse("d7cc064f-70a0-43ad-bd86-8d200580d6b8") },
                            new() { SongId = Guid.Parse("b2c5d58f-9983-48c5-8a1e-96dafdb3307c"), SimilarSongId = Guid.Parse("daf6789f-eb96-423c-9958-82ba17c4517a") }],
            CreatedAt = new DateTime(2024, 8, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d7cc064f-70a0-43ad-bd86-8d200580d6b8"),
            Title = "Perfect Places",
            ImageUrl = "https://perfect_places.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2017, 6, 22),
            DurationSeconds = 230,
            Album = _albums[25],
            Artists = [_artists[38]],
            SimilarSongs = [new() { SongId = Guid.Parse("d7cc064f-70a0-43ad-bd86-8d200580d6b8"), SimilarSongId = Guid.Parse("daf6789f-eb96-423c-9958-82ba17c4517a") },
                            new() { SongId = Guid.Parse("d7cc064f-70a0-43ad-bd86-8d200580d6b8"), SimilarSongId = Guid.Parse("5248af81-7450-48a8-bce9-d179a79101b3") }],
            CreatedAt = new DateTime(2024, 8, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("daf6789f-eb96-423c-9958-82ba17c4517a"),
            Title = "Rollercoaster",
            ImageUrl = "https://rollercoaster.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2013, 6, 18),
            DurationSeconds = 212,
            Album = _albums[35],
            Artists = [_artists[39]],
            SimilarSongs = [new() { SongId = Guid.Parse("daf6789f-eb96-423c-9958-82ba17c4517a"), SimilarSongId = Guid.Parse("5248af81-7450-48a8-bce9-d179a79101b3") },
                            new() { SongId = Guid.Parse("daf6789f-eb96-423c-9958-82ba17c4517a"), SimilarSongId = Guid.Parse("a8bdf565-b0d2-46dd-aa34-fedd762ff61c") }],
            CreatedAt = new DateTime(2024, 8, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 18, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("5248af81-7450-48a8-bce9-d179a79101b3"),
            Title = "Black Skinhead",
            ImageUrl = "https://black_skinhead.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2013, 6, 20),
            DurationSeconds = 188,
            Album = _albums[35],
            Artists = [_artists[39]],
            SimilarSongs = [new() { SongId = Guid.Parse("5248af81-7450-48a8-bce9-d179a79101b3"), SimilarSongId = Guid.Parse("a8bdf565-b0d2-46dd-aa34-fedd762ff61c") },
                            new() { SongId = Guid.Parse("5248af81-7450-48a8-bce9-d179a79101b3"), SimilarSongId = Guid.Parse("be8f4a76-9966-4b2f-95ef-067dd7879655") }],
            CreatedAt = new DateTime(2024, 8, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 19, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a8bdf565-b0d2-46dd-aa34-fedd762ff61c"),
            Title = "Bound 2",
            ImageUrl = "https://bound2.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2013, 6, 22),
            DurationSeconds = 223,
            Album = _albums[35],
            Artists = [_artists[40]],
            SimilarSongs = [new() { SongId = Guid.Parse("a8bdf565-b0d2-46dd-aa34-fedd762ff61c"), SimilarSongId = Guid.Parse("be8f4a76-9966-4b2f-95ef-067dd7879655") },
                            new() { SongId = Guid.Parse("a8bdf565-b0d2-46dd-aa34-fedd762ff61c"), SimilarSongId = Guid.Parse("5904e413-0619-4099-afbc-71e133a68511") }],
            CreatedAt = new DateTime(2024, 8, 20, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 20, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("be8f4a76-9966-4b2f-95ef-067dd7879655"),
            Title = "I'm In It",
            ImageUrl = "https://im_in_it.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2013, 6, 19),
            DurationSeconds = 233,
            Album = _albums[35],
            Artists = [_artists[40]],
            SimilarSongs = [new() { SongId = Guid.Parse("be8f4a76-9966-4b2f-95ef-067dd7879655"), SimilarSongId = Guid.Parse("5904e413-0619-4099-afbc-71e133a68511") },
                            new() { SongId = Guid.Parse("be8f4a76-9966-4b2f-95ef-067dd7879655"), SimilarSongId = Guid.Parse("a5780fe8-4708-4574-a3de-416064b970d1") }],
            CreatedAt = new DateTime(2024, 8, 21, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 21, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("5904e413-0619-4099-afbc-71e133a68511"),
            Title = "Blood On The Leaves",
            ImageUrl = "https://blood_on_the_leaves.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2013, 6, 19),
            DurationSeconds = 206,
            Album = _albums[35],
            Artists = [_artists[41]],
            SimilarSongs = [new() { SongId = Guid.Parse("5904e413-0619-4099-afbc-71e133a68511"), SimilarSongId = Guid.Parse("a5780fe8-4708-4574-a3de-416064b970d1") },
                            new() { SongId = Guid.Parse("5904e413-0619-4099-afbc-71e133a68511"), SimilarSongId = Guid.Parse("212c0538-0cb2-4126-bcc9-baaa8265afb2") }],
            CreatedAt = new DateTime(2024, 8, 22, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 22, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a5780fe8-4708-4574-a3de-416064b970d1"),
            Title = "Reflection",
            ImageUrl = "https://reflection.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2015, 3, 15),
            DurationSeconds = 228,
            Album = _albums[31],
            Artists = [_artists[41]],
            SimilarSongs = [new() { SongId = Guid.Parse("a5780fe8-4708-4574-a3de-416064b970d1"), SimilarSongId = Guid.Parse("212c0538-0cb2-4126-bcc9-baaa8265afb2") },
                            new() { SongId = Guid.Parse("a5780fe8-4708-4574-a3de-416064b970d1"), SimilarSongId = Guid.Parse("748d7f2c-0e5d-45cd-8f5d-77da302bbc0c") }],
            CreatedAt = new DateTime(2024, 8, 23, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 23, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("212c0538-0cb2-4126-bcc9-baaa8265afb2"),
            Title = "Alright",
            ImageUrl = "https://alright.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2015, 3, 17),
            DurationSeconds = 225,
            Album = _albums[31],
            Artists = [_artists[42]],
            SimilarSongs = [new() { SongId = Guid.Parse("212c0538-0cb2-4126-bcc9-baaa8265afb2"), SimilarSongId = Guid.Parse("748d7f2c-0e5d-45cd-8f5d-77da302bbc0c") },
                            new() { SongId = Guid.Parse("212c0538-0cb2-4126-bcc9-baaa8265afb2"), SimilarSongId = Guid.Parse("3a2b8ce7-e279-42dd-8905-a42d35bf6fa0") }],
            CreatedAt = new DateTime(2024, 8, 24, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 24, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("748d7f2c-0e5d-45cd-8f5d-77da302bbc0c"),
            Title = "King Kunta",
            ImageUrl = "https://king_kunta.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2015, 3, 19),
            DurationSeconds = 234,
            Album = _albums[31],
            Artists = [_artists[42]],
            SimilarSongs = [new() { SongId = Guid.Parse("748d7f2c-0e5d-45cd-8f5d-77da302bbc0c"), SimilarSongId = Guid.Parse("3a2b8ce7-e279-42dd-8905-a42d35bf6fa0") },
                            new() { SongId = Guid.Parse("748d7f2c-0e5d-45cd-8f5d-77da302bbc0c"), SimilarSongId = Guid.Parse("af20c27b-e20c-459a-bbde-7603fc8715fc") }],
            CreatedAt = new DateTime(2024, 8, 25, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 25, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3a2b8ce7-e279-42dd-8905-a42d35bf6fa0"),
            Title = "The Blacker The Berry",
            ImageUrl = "https://the_blacker_the_berry.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2015, 3, 16),
            DurationSeconds = 212,
            Album = _albums[31],
            Artists = [_artists[43]],
            SimilarSongs = [new() { SongId = Guid.Parse("3a2b8ce7-e279-42dd-8905-a42d35bf6fa0"), SimilarSongId = Guid.Parse("af20c27b-e20c-459a-bbde-7603fc8715fc") },
                            new() { SongId = Guid.Parse("3a2b8ce7-e279-42dd-8905-a42d35bf6fa0"), SimilarSongId = Guid.Parse("a23d5a35-4168-40d2-a8eb-cd15f71f120c") }],
            CreatedAt = new DateTime(2024, 8, 26, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 26, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("af20c27b-e20c-459a-bbde-7603fc8715fc"),
            Title = "These Walls",
            ImageUrl = "https://these_walls.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2015, 3, 16),
            DurationSeconds = 287,
            Album = _albums[31],
            Artists = [_artists[43]],
            SimilarSongs = [new() { SongId = Guid.Parse("af20c27b-e20c-459a-bbde-7603fc8715fc"), SimilarSongId = Guid.Parse("a23d5a35-4168-40d2-a8eb-cd15f71f120c") },
                            new() { SongId = Guid.Parse("af20c27b-e20c-459a-bbde-7603fc8715fc"), SimilarSongId = Guid.Parse("8b39efa4-ce8d-4617-84e7-45bf095c290a") }],
            CreatedAt = new DateTime(2024, 8, 27, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 27, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a23d5a35-4168-40d2-a8eb-cd15f71f120c"),
            Title = "Complexion (A Zulu Love)",
            ImageUrl = "https://complexion.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2015, 3, 16),
            DurationSeconds = 279,
            Album = _albums[31],
            Artists = [_artists[44]],
            SimilarSongs = [new() { SongId = Guid.Parse("a23d5a35-4168-40d2-a8eb-cd15f71f120c"), SimilarSongId = Guid.Parse("8b39efa4-ce8d-4617-84e7-45bf095c290a") },
                            new() { SongId = Guid.Parse("a23d5a35-4168-40d2-a8eb-cd15f71f120c"), SimilarSongId = Guid.Parse("e5b2ec61-7152-4f1b-bfbb-4a05a9d8b227") }],
            CreatedAt = new DateTime(2024, 8, 28, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 28, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("8b39efa4-ce8d-4617-84e7-45bf095c290a"),
            Title = "Evening Shadows",
            ImageUrl = "https://evening_shadows.jpg",
            Genre = Genre.Jazz,
            ReleaseDate = new DateOnly(2021, 5, 25),
            DurationSeconds = 194,
            Album = _albums[19],
            Artists = [_artists[44]],
            SimilarSongs = [new() { SongId = Guid.Parse("8b39efa4-ce8d-4617-84e7-45bf095c290a"), SimilarSongId = Guid.Parse("e5b2ec61-7152-4f1b-bfbb-4a05a9d8b227") },
                            new() { SongId = Guid.Parse("8b39efa4-ce8d-4617-84e7-45bf095c290a"), SimilarSongId = Guid.Parse("b2c1d43d-78b7-4d82-9e4e-e852018c1e82") }],
            CreatedAt = new DateTime(2024, 8, 29, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 29, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e5b2ec61-7152-4f1b-bfbb-4a05a9d8b227"),
            Title = "Shape of You",
            ImageUrl = "https://shape_of_you.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2017, 1, 6),
            DurationSeconds = 233,
            Album = _albums[19],
            Artists = [_artists[45]],
            SimilarSongs = [new() { SongId = Guid.Parse("e5b2ec61-7152-4f1b-bfbb-4a05a9d8b227"), SimilarSongId = Guid.Parse("b2c1d43d-78b7-4d82-9e4e-e852018c1e82") },
                            new() { SongId = Guid.Parse("e5b2ec61-7152-4f1b-bfbb-4a05a9d8b227"), SimilarSongId = Guid.Parse("5b447453-56d5-418e-8af2-8c15594d67eb") }],
            CreatedAt = new DateTime(2024, 8, 30, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 30, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b2c1d43d-78b7-4d82-9e4e-e852018c1e82"),
            Title = "Perfect",
            ImageUrl = "https://perfect.jpg",
            Genre = Genre.Pop,
            ReleaseDate = new DateOnly(2017, 9, 15),
            DurationSeconds = 263,
            Album = _albums[19],
            Artists = [_artists[45]],
            SimilarSongs = [new() { SongId = Guid.Parse("b2c1d43d-78b7-4d82-9e4e-e852018c1e82"), SimilarSongId = Guid.Parse("5b447453-56d5-418e-8af2-8c15594d67eb") },
                            new() { SongId = Guid.Parse("b2c1d43d-78b7-4d82-9e4e-e852018c1e82"), SimilarSongId = Guid.Parse("282bd513-06b1-42a6-ba38-689e934ca254") }],
            CreatedAt = new DateTime(2024, 8, 31, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 31, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("5b447453-56d5-418e-8af2-8c15594d67eb"),
            Title = "Hyaena",
            ImageUrl = "https://hyaena.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2023, 7, 28),
            DurationSeconds = 225,
            Album = _albums[0],
            Artists = [_artists[46]],
            SimilarSongs = [new() { SongId = Guid.Parse("5b447453-56d5-418e-8af2-8c15594d67eb"), SimilarSongId = Guid.Parse("282bd513-06b1-42a6-ba38-689e934ca254") },
                            new() { SongId = Guid.Parse("5b447453-56d5-418e-8af2-8c15594d67eb"), SimilarSongId = Guid.Parse("4a3da028-5eff-4f76-a9e4-7959fbd62dc3") }],
            CreatedAt = new DateTime(2024, 9, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("282bd513-06b1-42a6-ba38-689e934ca254"),
            Title = "Fever",
            ImageUrl = "https://fever.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2023, 8, 1),
            DurationSeconds = 210,
            Album = _albums[0],
            Artists = [_artists[46]],
            SimilarSongs = [new() { SongId = Guid.Parse("282bd513-06b1-42a6-ba38-689e934ca254"), SimilarSongId = Guid.Parse("4a3da028-5eff-4f76-a9e4-7959fbd62dc3") },
                            new() { SongId = Guid.Parse("282bd513-06b1-42a6-ba38-689e934ca254"), SimilarSongId = Guid.Parse("55e26296-3afb-47aa-b9ce-c45993d3698b") }],
            CreatedAt = new DateTime(2024, 9, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("4a3da028-5eff-4f76-a9e4-7959fbd62dc3"),
            Title = "Looove",
            ImageUrl = "https://looove.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2023, 8, 5),
            DurationSeconds = 260,
            Album = _albums[0],
            Artists = [_artists[47]],
            SimilarSongs = [new() { SongId = Guid.Parse("4a3da028-5eff-4f76-a9e4-7959fbd62dc3"), SimilarSongId = Guid.Parse("55e26296-3afb-47aa-b9ce-c45993d3698b") },
                            new() { SongId = Guid.Parse("4a3da028-5eff-4f76-a9e4-7959fbd62dc3"), SimilarSongId = Guid.Parse("f45ef653-8cf8-470d-9b68-39e18ce83b56") }],
            CreatedAt = new DateTime(2024, 9, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("55e26296-3afb-47aa-b9ce-c45993d3698b"),
            Title = "Mafia",
            ImageUrl = "https://mafia.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2023, 8, 10),
            DurationSeconds = 235,
            Album = _albums[0],
            Artists = [_artists[47]],
            SimilarSongs = [new() { SongId = Guid.Parse("55e26296-3afb-47aa-b9ce-c45993d3698b"), SimilarSongId = Guid.Parse("f45ef653-8cf8-470d-9b68-39e18ce83b56") },
                            new() { SongId = Guid.Parse("55e26296-3afb-47aa-b9ce-c45993d3698b"), SimilarSongId = Guid.Parse("edcd516d-5da1-4dd6-bdeb-5cfefd2cd073") }],
            CreatedAt = new DateTime(2024, 9, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 4, 0, 0, 0),
            DeletedAt = new DateTime(2024, 9, 4, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("f45ef653-8cf8-470d-9b68-39e18ce83b56"),
            Title = "Gang Gang",
            ImageUrl = "https://gang_gang.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2023, 8, 15),
            DurationSeconds = 250,
            Album = _albums[0],
            Artists = [_artists[48]],
            SimilarSongs = [new() { SongId = Guid.Parse("f45ef653-8cf8-470d-9b68-39e18ce83b56"), SimilarSongId = Guid.Parse("edcd516d-5da1-4dd6-bdeb-5cfefd2cd073") },
                            new() { SongId = Guid.Parse("f45ef653-8cf8-470d-9b68-39e18ce83b56"), SimilarSongId = Guid.Parse("599b474a-99ce-48f1-92a8-fb5711b46369") }],
            CreatedAt = new DateTime(2024, 9, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 5, 0, 0, 0),
            DeletedAt = new DateTime(2024, 9, 5, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("edcd516d-5da1-4dd6-bdeb-5cfefd2cd073"),
            Title = "Til Further Notice",
            ImageUrl = "https://til_further_notice.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2023, 8, 20),
            DurationSeconds = 200,
            Album = _albums[0],
            Artists = [_artists[48]],
            SimilarSongs = [new() { SongId = Guid.Parse("edcd516d-5da1-4dd6-bdeb-5cfefd2cd073"), SimilarSongId = Guid.Parse("599b474a-99ce-48f1-92a8-fb5711b46369") },
                            new() { SongId = Guid.Parse("edcd516d-5da1-4dd6-bdeb-5cfefd2cd073"), SimilarSongId = Guid.Parse("0f43a456-5674-433b-b7b4-4406131f5a7f") }],
            CreatedAt = new DateTime(2024, 9, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 6, 0, 0, 0),
            DeletedAt = new DateTime(2024, 9, 6, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("599b474a-99ce-48f1-92a8-fb5711b46369"),
            Title = "Love Me",
            ImageUrl = "https://love_me.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2023, 8, 25),
            DurationSeconds = 220,
            Album = _albums[0],
            Artists = [_artists[49]],
            SimilarSongs = [new() { SongId = Guid.Parse("599b474a-99ce-48f1-92a8-fb5711b46369"), SimilarSongId = Guid.Parse("0f43a456-5674-433b-b7b4-4406131f5a7f") },
                            new() { SongId = Guid.Parse("599b474a-99ce-48f1-92a8-fb5711b46369"), SimilarSongId = Guid.Parse("64f534f8-f2d4-4402-95a3-54de48b678a8") }],
            CreatedAt = new DateTime(2024, 9, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 7, 0, 0, 0),
            DeletedAt = new DateTime(2024, 9, 7, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("0f43a456-5674-433b-b7b4-4406131f5a7f"),
            Title = "Skeletons",
            ImageUrl = "https://skeletons.jpg",
            Genre = Genre.HipHop,
            ReleaseDate = new DateOnly(2023, 8, 30),
            DurationSeconds = 270,
            Album = _albums[0],
            Artists = [_artists[49]], 
            SimilarSongs = [new() { SongId = Guid.Parse("0f43a456-5674-433b-b7b4-4406131f5a7f"), SimilarSongId = Guid.Parse("64f534f8-f2d4-4402-95a3-54de48b678a8") },
                            new() { SongId = Guid.Parse("0f43a456-5674-433b-b7b4-4406131f5a7f"), SimilarSongId = Guid.Parse("278cfa5a-6f44-420e-9930-07da6c43a6ad") }],
            CreatedAt = new DateTime(2024, 9, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 9, 8, 0, 0, 0),
            DeletedAt = new DateTime(2024, 9, 8, 12, 0, 0)
        }];

        public static readonly Song _newSong = new()
        {
            Title = "Evening Shadows",
            ImageUrl = "https://evening_shadows.jpg",
            Genre = Genre.Jazz,
            ReleaseDate = new DateOnly(2021, 5, 25),
            DurationSeconds = 194,
            Album = _albums[0],
            Artists = [_artists[15]],
            SimilarSongs = [new() { SongId = Guid.Parse("8b39efa4-ce8d-4617-84e7-45bf095c290a"), SimilarSongId = Guid.Parse("64f534f8-f2d4-4402-95a3-54de48b678a8") },
                            new() { SongId = Guid.Parse("8b39efa4-ce8d-4617-84e7-45bf095c290a"), SimilarSongId = Guid.Parse("278cfa5a-6f44-420e-9930-07da6c43a6ad") }]
        };

        public static readonly List<SongDto> _songDtos = _songs.Select(ToDto).ToList();

        public static readonly SongDto _newSongDto = ToDto(_newSong);

        public static readonly List<Song> _songsPagination = _songs.Where(song => song.DeletedAt == null).Take(10).ToList();

        public static readonly List<SongDto> _songDtosPagination = _songsPagination.Select(ToDto).ToList();

        public static readonly SongPaginationRequest _songPayload = new(SortCriteria: null, SearchCriteria: null, Title: null, Genre: null, DateRange: null, DurationRange: null, AlbumTitle: null, ArtistName: null);

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