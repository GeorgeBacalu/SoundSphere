using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Test.Mocks
{
    public class AlbumMock
    {
        private AlbumMock() { }

        public static readonly List<Album> _albums = [new()
        {
            Id = Guid.Parse("6ee76a77-2be4-42e3-8417-e60d282cffcb"),
            Title = "Utopia",
            ImageUrl = "https://utopia.jpg",
            ReleaseDate = new DateOnly(2022, 4, 25),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("6ee76a77-2be4-42e3-8417-e60d282cffcb"), SimilarAlbumId = Guid.Parse("b58f5f3f-d5e8-49f3-8b12-cfe33f762b4f") },
                             new() { AlbumId = Guid.Parse("6ee76a77-2be4-42e3-8417-e60d282cffcb"), SimilarAlbumId = Guid.Parse("05c4fe5d-8c0f-4eff-8ff2-e2e0e91218db") }],
            CreatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b58f5f3f-d5e8-49f3-8b12-cfe33f762b4f"),
            Title = "Echoes of Silence",
            ImageUrl = "https://echoes_of_silence.jpg",
            ReleaseDate = new DateOnly(2011, 12, 21),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("b58f5f3f-d5e8-49f3-8b12-cfe33f762b4f"), SimilarAlbumId = Guid.Parse("05c4fe5d-8c0f-4eff-8ff2-e2e0e91218db") },
                             new() { AlbumId = Guid.Parse("b58f5f3f-d5e8-49f3-8b12-cfe33f762b4f"), SimilarAlbumId = Guid.Parse("77953b24-e1c0-4d1f-a730-a12d5460b0d1") }],
            CreatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("05c4fe5d-8c0f-4eff-8ff2-e2e0e91218db"),
            Title = "Born to Die",
            ImageUrl = "https://born_to_die.jpg",
            ReleaseDate = new DateOnly(2012, 1, 27),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("05c4fe5d-8c0f-4eff-8ff2-e2e0e91218db"), SimilarAlbumId = Guid.Parse("77953b24-e1c0-4d1f-a730-a12d5460b0d1") },
                             new() { AlbumId = Guid.Parse("05c4fe5d-8c0f-4eff-8ff2-e2e0e91218db"), SimilarAlbumId = Guid.Parse("e6d397e8-c355-4d68-b9e4-4a1c67daf6fa") }],
            CreatedAt = new DateTime(2024, 7, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("77953b24-e1c0-4d1f-a730-a12d5460b0d1"),
            Title = "Random Access Memories",
            ImageUrl = "https://random_access_memories.jpg",
            ReleaseDate = new DateOnly(2013, 5, 17),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("77953b24-e1c0-4d1f-a730-a12d5460b0d1"), SimilarAlbumId = Guid.Parse("e6d397e8-c355-4d68-b9e4-4a1c67daf6fa") },
                             new() { AlbumId = Guid.Parse("77953b24-e1c0-4d1f-a730-a12d5460b0d1"), SimilarAlbumId = Guid.Parse("c0ec7e3d-8d8b-47b5-9b29-8a4b4c5357c1") }],
            CreatedAt = new DateTime(2024, 7, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e6d397e8-c355-4d68-b9e4-4a1c67daf6fa"),
            Title = "Good Kid, M.A.A.D City",
            ImageUrl = "https://good_kid_maad_city.jpg",
            ReleaseDate = new DateOnly(2012, 10, 22),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("e6d397e8-c355-4d68-b9e4-4a1c67daf6fa"), SimilarAlbumId = Guid.Parse("c0ec7e3d-8d8b-47b5-9b29-8a4b4c5357c1") },
                             new() { AlbumId = Guid.Parse("e6d397e8-c355-4d68-b9e4-4a1c67daf6fa"), SimilarAlbumId = Guid.Parse("8f70a802-6741-48de-9f2f-7f1d184b1687") }],
            CreatedAt = new DateTime(2024, 7, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c0ec7e3d-8d8b-47b5-9b29-8a4b4c5357c1"),
            Title = "Channel Orange",
            ImageUrl = "https://channel_orange.jpg",
            ReleaseDate = new DateOnly(2012, 7, 10),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("c0ec7e3d-8d8b-47b5-9b29-8a4b4c5357c1"), SimilarAlbumId = Guid.Parse("8f70a802-6741-48de-9f2f-7f1d184b1687") },
                             new() { AlbumId = Guid.Parse("c0ec7e3d-8d8b-47b5-9b29-8a4b4c5357c1"), SimilarAlbumId = Guid.Parse("bd2096d2-5b54-432a-9363-a99ed0078c7a") }],
            CreatedAt = new DateTime(2024, 7, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("8f70a802-6741-48de-9f2f-7f1d184b1687"),
            Title = "In the Lonely Hour",
            ImageUrl = "https://in_the_lonely_hour.jpg",
            ReleaseDate = new DateOnly(2014, 5, 26),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("8f70a802-6741-48de-9f2f-7f1d184b1687"), SimilarAlbumId = Guid.Parse("bd2096d2-5b54-432a-9363-a99ed0078c7a") },
                             new() { AlbumId = Guid.Parse("8f70a802-6741-48de-9f2f-7f1d184b1687"), SimilarAlbumId = Guid.Parse("6542c145-0f0e-432f-8b8c-9a6a53c6b8e8") }],
            CreatedAt = new DateTime(2024, 7, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("bd2096d2-5b54-432a-9363-a99ed0078c7a"),
            Title = "The Dark Side of the Moon",
            ImageUrl = "https://the_dark_side_of_the_moon.jpg",
            ReleaseDate = new DateOnly(1973, 3, 1),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("bd2096d2-5b54-432a-9363-a99ed0078c7a"), SimilarAlbumId = Guid.Parse("6542c145-0f0e-432f-8b8c-9a6a53c6b8e8") },
                             new() { AlbumId = Guid.Parse("bd2096d2-5b54-432a-9363-a99ed0078c7a"), SimilarAlbumId = Guid.Parse("c3e92b3d-f3b7-4935-baac-68b47054e897") }],
            CreatedAt = new DateTime(2024, 7, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("6542c145-0f0e-432f-8b8c-9a6a53c6b8e8"),
            Title = "Thriller",
            ImageUrl = "https://thriller.jpg",
            ReleaseDate = new DateOnly(1982, 11, 30),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("6542c145-0f0e-432f-8b8c-9a6a53c6b8e8"), SimilarAlbumId = Guid.Parse("c3e92b3d-f3b7-4935-baac-68b47054e897") },
                             new() { AlbumId = Guid.Parse("6542c145-0f0e-432f-8b8c-9a6a53c6b8e8"), SimilarAlbumId = Guid.Parse("d4b7c7f3-8c6f-4f1a-91ff-ee2a850b2f3e") }],
            CreatedAt = new DateTime(2024, 7, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c3e92b3d-f3b7-4935-baac-68b47054e897"),
            Title = "Black Star",
            ImageUrl = "https://black_star.jpg",
            ReleaseDate = new DateOnly(2016, 1, 8),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("c3e92b3d-f3b7-4935-baac-68b47054e897"), SimilarAlbumId = Guid.Parse("d4b7c7f3-8c6f-4f1a-91ff-ee2a850b2f3e") },
                             new() { AlbumId = Guid.Parse("c3e92b3d-f3b7-4935-baac-68b47054e897"), SimilarAlbumId = Guid.Parse("f30c8c10-d54f-498c-8423-6e1d2dcaae4d") }],
            CreatedAt = new DateTime(2024, 7, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d4b7c7f3-8c6f-4f1a-91ff-ee2a850b2f3e"),
            Title = "Fine Line",
            ImageUrl = "https://fine_line.jpg",
            ReleaseDate = new DateOnly(2019, 12, 13),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("d4b7c7f3-8c6f-4f1a-91ff-ee2a850b2f3e"), SimilarAlbumId = Guid.Parse("f30c8c10-d54f-498c-8423-6e1d2dcaae4d") },
                             new() { AlbumId = Guid.Parse("d4b7c7f3-8c6f-4f1a-91ff-ee2a850b2f3e"), SimilarAlbumId = Guid.Parse("5fefa585-5a2f-4f4b-b6f5-850768f7c7b8") }],
            CreatedAt = new DateTime(2024, 7, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f30c8c10-d54f-498c-8423-6e1d2dcaae4d"),
            Title = "After Hours",
            ImageUrl = "https://after_hours.jpg",
            ReleaseDate = new DateOnly(2020, 3, 20),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("f30c8c10-d54f-498c-8423-6e1d2dcaae4d"), SimilarAlbumId = Guid.Parse("5fefa585-5a2f-4f4b-b6f5-850768f7c7b8") },
                             new() { AlbumId = Guid.Parse("f30c8c10-d54f-498c-8423-6e1d2dcaae4d"), SimilarAlbumId = Guid.Parse("9f1c4000-6f48-4bda-aa6d-22f0581629d1") }],
            CreatedAt = new DateTime(2024, 7, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("5fefa585-5a2f-4f4b-b6f5-850768f7c7b8"),
            Title = "Evermore",
            ImageUrl = "https://evermore.jpg",
            ReleaseDate = new DateOnly(2020, 12, 11),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("5fefa585-5a2f-4f4b-b6f5-850768f7c7b8"), SimilarAlbumId = Guid.Parse("9f1c4000-6f48-4bda-aa6d-22f0581629d1") },
                             new() { AlbumId = Guid.Parse("5fefa585-5a2f-4f4b-b6f5-850768f7c7b8"), SimilarAlbumId = Guid.Parse("ae93288c-a804-4f40-a97c-6f3572da3b01") }],
            CreatedAt = new DateTime(2024, 7, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("9f1c4000-6f48-4bda-aa6d-22f0581629d1"),
            Title = "A Moon Shaped Pool",
            ImageUrl = "https://a_moon_shaped_pool.jpg",
            ReleaseDate = new DateOnly(2016, 5, 8),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("9f1c4000-6f48-4bda-aa6d-22f0581629d1"), SimilarAlbumId = Guid.Parse("ae93288c-a804-4f40-a97c-6f3572da3b01") },
                             new() { AlbumId = Guid.Parse("9f1c4000-6f48-4bda-aa6d-22f0581629d1"), SimilarAlbumId = Guid.Parse("3e5b76a4-c2bf-4c7d-9abf-a59b3e8e0bc2") }],
            CreatedAt = new DateTime(2024, 7, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("ae93288c-a804-4f40-a97c-6f3572da3b01"),
            Title = "DAMN.",
            ImageUrl = "https://damn.jpg",
            ReleaseDate = new DateOnly(2017, 4, 14),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("ae93288c-a804-4f40-a97c-6f3572da3b01"), SimilarAlbumId = Guid.Parse("3e5b76a4-c2bf-4c7d-9abf-a59b3e8e0bc2") },
                             new() { AlbumId = Guid.Parse("ae93288c-a804-4f40-a97c-6f3572da3b01"), SimilarAlbumId = Guid.Parse("9d4a518b-6673-4a91-b179-2b8c68a4f3f3") }],
            CreatedAt = new DateTime(2024, 7, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3e5b76a4-c2bf-4c7d-9abf-a59b3e8e0bc2"),
            Title = "Lemonade",
            ImageUrl = "https://lemonade.jpg",
            ReleaseDate = new DateOnly(2016, 4, 23),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("3e5b76a4-c2bf-4c7d-9abf-a59b3e8e0bc2"), SimilarAlbumId = Guid.Parse("9d4a518b-6673-4a91-b179-2b8c68a4f3f3") },
                             new() { AlbumId = Guid.Parse("3e5b76a4-c2bf-4c7d-9abf-a59b3e8e0bc2"), SimilarAlbumId = Guid.Parse("c5f5b360-fbfd-4e17-aca3-e2a1d5b25895") }],
            CreatedAt = new DateTime(2024, 7, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("9d4a518b-6673-4a91-b179-2b8c68a4f3f3"),
            Title = "21",
            ImageUrl = "https://21_adele.jpg",
            ReleaseDate = new DateOnly(2011, 1, 24),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("9d4a518b-6673-4a91-b179-2b8c68a4f3f3"), SimilarAlbumId = Guid.Parse("c5f5b360-fbfd-4e17-aca3-e2a1d5b25895") },
                             new() { AlbumId = Guid.Parse("9d4a518b-6673-4a91-b179-2b8c68a4f3f3"), SimilarAlbumId = Guid.Parse("57d9b8a7-d36b-4975-bd4a-a4c5dbe5c5cc") }],
            CreatedAt = new DateTime(2024, 7, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c5f5b360-fbfd-4e17-aca3-e2a1d5b25895"),
            Title = "Voyage",
            ImageUrl = "https://voyage.jpg",
            ReleaseDate = new DateOnly(2021, 11, 5),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("c5f5b360-fbfd-4e17-aca3-e2a1d5b25895"), SimilarAlbumId = Guid.Parse("57d9b8a7-d36b-4975-bd4a-a4c5dbe5c5cc") },
                             new() { AlbumId = Guid.Parse("c5f5b360-fbfd-4e17-aca3-e2a1d5b25895"), SimilarAlbumId = Guid.Parse("2d68f441-d532-4e0d-8f51-c5d086078dd9") }],
            CreatedAt = new DateTime(2024, 7, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 18, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("57d9b8a7-d36b-4975-bd4a-a4c5dbe5c5cc"),
            Title = "Starboy",
            ImageUrl = "https://starboy.jpg",
            ReleaseDate = new DateOnly(2016, 11, 25),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("57d9b8a7-d36b-4975-bd4a-a4c5dbe5c5cc"), SimilarAlbumId = Guid.Parse("2d68f441-d532-4e0d-8f51-c5d086078dd9") },
                             new() { AlbumId = Guid.Parse("57d9b8a7-d36b-4975-bd4a-a4c5dbe5c5cc"), SimilarAlbumId = Guid.Parse("1b6d3a7a-d9d4-478e-b0c1-8d8c2a4f6c0f") }],
            CreatedAt = new DateTime(2024, 7, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 19, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2d68f441-d532-4e0d-8f51-c5d086078dd9"),
            Title = "Divide",
            ImageUrl = "https://divide.jpg",
            ReleaseDate = new DateOnly(2017, 3, 3),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("2d68f441-d532-4e0d-8f51-c5d086078dd9"), SimilarAlbumId = Guid.Parse("1b6d3a7a-d9d4-478e-b0c1-8d8c2a4f6c0f") },
                             new() { AlbumId = Guid.Parse("2d68f441-d532-4e0d-8f51-c5d086078dd9"), SimilarAlbumId = Guid.Parse("2e2f662e-c2df-4660-800c-3613b6001bf1") }],
            CreatedAt = new DateTime(2024, 7, 20, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 20, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("1b6d3a7a-d9d4-478e-b0c1-8d8c2a4f6c0f"),
            Title = "Vibras",
            ImageUrl = "https://vibras.jpg",
            ReleaseDate = new DateOnly(2018, 5, 25),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("1b6d3a7a-d9d4-478e-b0c1-8d8c2a4f6c0f"), SimilarAlbumId = Guid.Parse("2e2f662e-c2df-4660-800c-3613b6001bf1") },
                             new() { AlbumId = Guid.Parse("1b6d3a7a-d9d4-478e-b0c1-8d8c2a4f6c0f"), SimilarAlbumId = Guid.Parse("cc3cf857-e7f5-4c0b-9eaa-3f6e4c8d2045") }],
            CreatedAt = new DateTime(2024, 7, 21, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 21, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2e2f662e-c2df-4660-800c-3613b6001bf1"),
            Title = "Astroworld",
            ImageUrl = "https://astroworld.jpg",
            ReleaseDate = new DateOnly(2018, 8, 3),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("2e2f662e-c2df-4660-800c-3613b6001bf1"), SimilarAlbumId = Guid.Parse("cc3cf857-e7f5-4c0b-9eaa-3f6e4c8d2045") },
                             new() { AlbumId = Guid.Parse("2e2f662e-c2df-4660-800c-3613b6001bf1"), SimilarAlbumId = Guid.Parse("995a2c8e-cdd5-4c0f-b875-8b6192b6b014") }],
            CreatedAt = new DateTime(2024, 7, 22, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 22, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("cc3cf857-e7f5-4c0b-9eaa-3f6e4c8d2045"),
            Title = "Hollywoods Bleeding",
            ImageUrl = "https://hollywoods_bleeding.jpg",
            ReleaseDate = new DateOnly(2019, 9, 6),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("cc3cf857-e7f5-4c0b-9eaa-3f6e4c8d2045"), SimilarAlbumId = Guid.Parse("995a2c8e-cdd5-4c0f-b875-8b6192b6b014") },
                             new() { AlbumId = Guid.Parse("cc3cf857-e7f5-4c0b-9eaa-3f6e4c8d2045"), SimilarAlbumId = Guid.Parse("fb9eac8d-0aec-4cc2-b5c6-f9eca5c6d408") }],
            CreatedAt = new DateTime(2024, 7, 23, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 23, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("995a2c8e-cdd5-4c0f-b875-8b6192b6b014"),
            Title = "Blonde",
            ImageUrl = "https://blonde.jpg",
            ReleaseDate = new DateOnly(2016, 8, 20),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("995a2c8e-cdd5-4c0f-b875-8b6192b6b014"), SimilarAlbumId = Guid.Parse("fb9eac8d-0aec-4cc2-b5c6-f9eca5c6d408") },
                             new() { AlbumId = Guid.Parse("995a2c8e-cdd5-4c0f-b875-8b6192b6b014"), SimilarAlbumId = Guid.Parse("0fd3aee7-0bd0-4482-a78d-17e3c7d8b4d5") }],
            CreatedAt = new DateTime(2024, 7, 24, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 24, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("fb9eac8d-0aec-4cc2-b5c6-f9eca5c6d408"),
            Title = "My Beautiful Dark Twisted Fantasy",
            ImageUrl = "https://my_beautiful_dark_twisted_fantasy.jpg",
            ReleaseDate = new DateOnly(2010, 11, 22),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("fb9eac8d-0aec-4cc2-b5c6-f9eca5c6d408"), SimilarAlbumId = Guid.Parse("0fd3aee7-0bd0-4482-a78d-17e3c7d8b4d5") },
                             new() { AlbumId = Guid.Parse("fb9eac8d-0aec-4cc2-b5c6-f9eca5c6d408"), SimilarAlbumId = Guid.Parse("41ad644e-bdec-472c-ae5d-9f2d1e7fcb0d") }],
            CreatedAt = new DateTime(2024, 7, 25, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 25, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("0fd3aee7-0bd0-4482-a78d-17e3c7d8b4d5"),
            Title = "Melodrama",
            ImageUrl = "https://melodrama.jpg",
            ReleaseDate = new DateOnly(2017, 6, 16),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("0fd3aee7-0bd0-4482-a78d-17e3c7d8b4d5"), SimilarAlbumId = Guid.Parse("41ad644e-bdec-472c-ae5d-9f2d1e7fcb0d") },
                             new() { AlbumId = Guid.Parse("0fd3aee7-0bd0-4482-a78d-17e3c7d8b4d5"), SimilarAlbumId = Guid.Parse("c7e1f8b6-d8df-4eca-b45e-736b5d63b4d5") }],
            CreatedAt = new DateTime(2024, 7, 26, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 26, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("41ad644e-bdec-472c-ae5d-9f2d1e7fcb0d"),
            Title = "Nothing Was the Same",
            ImageUrl = "https://nothing_was_the_same.jpg",
            ReleaseDate = new DateOnly(2013, 9, 24),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("41ad644e-bdec-472c-ae5d-9f2d1e7fcb0d"), SimilarAlbumId = Guid.Parse("c7e1f8b6-d8df-4eca-b45e-736b5d63b4d5") },
                             new() { AlbumId = Guid.Parse("41ad644e-bdec-472c-ae5d-9f2d1e7fcb0d"), SimilarAlbumId = Guid.Parse("aac3f3d7-3146-421f-981d-68f02d1352f0") }],
            CreatedAt = new DateTime(2024, 7, 27, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 27, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("c7e1f8b6-d8df-4eca-b45e-736b5d63b4d5"),
            Title = "ANTI",
            ImageUrl = "https://anti.jpg",
            ReleaseDate = new DateOnly(2016, 1, 28),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("c7e1f8b6-d8df-4eca-b45e-736b5d63b4d5"), SimilarAlbumId = Guid.Parse("aac3f3d7-3146-421f-981d-68f02d1352f0") },
                             new() { AlbumId = Guid.Parse("c7e1f8b6-d8df-4eca-b45e-736b5d63b4d5"), SimilarAlbumId = Guid.Parse("a393b54c-01fb-4fd3-a8bb-3395bf603934") }],
            CreatedAt = new DateTime(2024, 7, 28, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 28, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("aac3f3d7-3146-421f-981d-68f02d1352f0"),
            Title = "Future Nostalgia",
            ImageUrl = "https://future_nostalgia.jpg",
            ReleaseDate = new DateOnly(2020, 3, 27),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("aac3f3d7-3146-421f-981d-68f02d1352f0"), SimilarAlbumId = Guid.Parse("a393b54c-01fb-4fd3-a8bb-3395bf603934") },
                             new() { AlbumId = Guid.Parse("aac3f3d7-3146-421f-981d-68f02d1352f0"), SimilarAlbumId = Guid.Parse("77f3f764-0a2e-4be2-a753-5555af2c4d52") }],
            CreatedAt = new DateTime(2024, 7, 29, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 29, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a393b54c-01fb-4fd3-a8bb-3395bf603934"),
            Title = "When We All Fall Asleep, Where Do We Go?",
            ImageUrl = "https://when_we_all_fall_asleep.jpg",
            ReleaseDate = new DateOnly(2019, 3, 29),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("a393b54c-01fb-4fd3-a8bb-3395bf603934"), SimilarAlbumId = Guid.Parse("77f3f764-0a2e-4be2-a753-5555af2c4d52") },
                             new() { AlbumId = Guid.Parse("a393b54c-01fb-4fd3-a8bb-3395bf603934"), SimilarAlbumId = Guid.Parse("71b4bdec-c4a4-413b-b874-e3fc2b8a1235") }],
            CreatedAt = new DateTime(2024, 7, 30, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 30, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("77f3f764-0a2e-4be2-a753-5555af2c4d52"),
            Title = "The Life of Pablo",
            ImageUrl = "https://the_life_of_pablo.jpg",
            ReleaseDate = new DateOnly(2016, 2, 14),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("77f3f764-0a2e-4be2-a753-5555af2c4d52"), SimilarAlbumId = Guid.Parse("71b4bdec-c4a4-413b-b874-e3fc2b8a1235") },
                             new() { AlbumId = Guid.Parse("77f3f764-0a2e-4be2-a753-5555af2c4d52"), SimilarAlbumId = Guid.Parse("39fb8791-3b22-4aad-99d1-27b51a5a3e37") }],
            CreatedAt = new DateTime(2024, 7, 31, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 31, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("71b4bdec-c4a4-413b-b874-e3fc2b8a1235"),
            Title = "To Pimp a Butterfly",
            ImageUrl = "https://to_pimp_a_butterfly.jpg",
            ReleaseDate = new DateOnly(2015, 3, 15),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("71b4bdec-c4a4-413b-b874-e3fc2b8a1235"), SimilarAlbumId = Guid.Parse("39fb8791-3b22-4aad-99d1-27b51a5a3e37") },
                             new() { AlbumId = Guid.Parse("71b4bdec-c4a4-413b-b874-e3fc2b8a1235"), SimilarAlbumId = Guid.Parse("7cd3c9a2-bc21-4797-b488-67e845a0e79b") }],
            CreatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("39fb8791-3b22-4aad-99d1-27b51a5a3e37"),
            Title = "Tranquility Base Hotel & Casino",
            ImageUrl = "https://tranquility_base.jpg",
            ReleaseDate = new DateOnly(2018, 5, 11),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("39fb8791-3b22-4aad-99d1-27b51a5a3e37"), SimilarAlbumId = Guid.Parse("7cd3c9a2-bc21-4797-b488-67e845a0e79b") },
                             new() { AlbumId = Guid.Parse("39fb8791-3b22-4aad-99d1-27b51a5a3e37"), SimilarAlbumId = Guid.Parse("ade35692-6d10-4c2f-8c3b-ff7b638d06e7") }],
            CreatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7cd3c9a2-bc21-4797-b488-67e845a0e79b"),
            Title = "Currents",
            ImageUrl = "https://currents.jpg",
            ReleaseDate = new DateOnly(2015, 7, 17),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("7cd3c9a2-bc21-4797-b488-67e845a0e79b"), SimilarAlbumId = Guid.Parse("ade35692-6d10-4c2f-8c3b-ff7b638d06e7") },
                             new() { AlbumId = Guid.Parse("7cd3c9a2-bc21-4797-b488-67e845a0e79b"), SimilarAlbumId = Guid.Parse("89b992ff-29be-49d3-b786-8d2e8d44de55") }],
            CreatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("ade35692-6d10-4c2f-8c3b-ff7b638d06e7"),
            Title = "Art Angels",
            ImageUrl = "https://art_angels.jpg",
            ReleaseDate = new DateOnly(2015, 11, 6),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("ade35692-6d10-4c2f-8c3b-ff7b638d06e7"), SimilarAlbumId = Guid.Parse("89b992ff-29be-49d3-b786-8d2e8d44de55") },
                             new() { AlbumId = Guid.Parse("ade35692-6d10-4c2f-8c3b-ff7b638d06e7"), SimilarAlbumId = Guid.Parse("ec2c9fe3-e8a2-4ec6-a7b5-885e10dd87ec") }],
            CreatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("89b992ff-29be-49d3-b786-8d2e8d44de55"),
            Title = "Yeezus",
            ImageUrl = "https://yeezus.jpg",
            ReleaseDate = new DateOnly(2013, 6, 18),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("89b992ff-29be-49d3-b786-8d2e8d44de55"), SimilarAlbumId = Guid.Parse("ec2c9fe3-e8a2-4ec6-a7b5-885e10dd87ec") },
                             new() { AlbumId = Guid.Parse("89b992ff-29be-49d3-b786-8d2e8d44de55"), SimilarAlbumId = Guid.Parse("3ef8d9f9-88f8-4f39-b73d-261b5a7f2c8a") }],
            CreatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("ec2c9fe3-e8a2-4ec6-a7b5-885e10dd87ec"),
            Title = "Ctrl",
            ImageUrl = "https://ctrl.jpg",
            ReleaseDate = new DateOnly(2017, 6, 9),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("ec2c9fe3-e8a2-4ec6-a7b5-885e10dd87ec"), SimilarAlbumId = Guid.Parse("3ef8d9f9-88f8-4f39-b73d-261b5a7f2c8a") },
                             new() { AlbumId = Guid.Parse("ec2c9fe3-e8a2-4ec6-a7b5-885e10dd87ec"), SimilarAlbumId = Guid.Parse("07fa60f8-8b91-4c33-bf53-1e4f3b79f597") }],
            CreatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3ef8d9f9-88f8-4f39-b73d-261b5a7f2c8a"),
            Title = "Cleopatra",
            ImageUrl = "https://cleopatra.jpg",
            ReleaseDate = new DateOnly(2016, 4, 8),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("3ef8d9f9-88f8-4f39-b73d-261b5a7f2c8a"), SimilarAlbumId = Guid.Parse("07fa60f8-8b91-4c33-bf53-1e4f3b79f597") },
                             new() { AlbumId = Guid.Parse("3ef8d9f9-88f8-4f39-b73d-261b5a7f2c8a"), SimilarAlbumId = Guid.Parse("d6a47f2d-bb31-4f1c-a9df-6b557b2f6b26") }],
            CreatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("07fa60f8-8b91-4c33-bf53-1e4f3b79f597"),
            Title = "Flower Boy",
            ImageUrl = "https://flower_boy.jpg",
            ReleaseDate = new DateOnly(2017, 7, 21),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("07fa60f8-8b91-4c33-bf53-1e4f3b79f597"), SimilarAlbumId = Guid.Parse("d6a47f2d-bb31-4f1c-a9df-6b557b2f6b26") },
                             new() { AlbumId = Guid.Parse("07fa60f8-8b91-4c33-bf53-1e4f3b79f597"), SimilarAlbumId = Guid.Parse("b2d7f920-68b2-4ba4-a99e-abf3069f887c") }],
            CreatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d6a47f2d-bb31-4f1c-a9df-6b557b2f6b26"),
            Title = "In Rainbows",
            ImageUrl = "https://in_rainbows.jpg",
            ReleaseDate = new DateOnly(2007, 10, 10),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("d6a47f2d-bb31-4f1c-a9df-6b557b2f6b26"), SimilarAlbumId = Guid.Parse("b2d7f920-68b2-4ba4-a99e-abf3069f887c") },
                             new() { AlbumId = Guid.Parse("d6a47f2d-bb31-4f1c-a9df-6b557b2f6b26"), SimilarAlbumId = Guid.Parse("1c3a213b-aa9b-4c8b-a393-4c881522eebb") }],
            CreatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b2d7f920-68b2-4ba4-a99e-abf3069f887c"),
            Title = "Stoney",
            ImageUrl = "https://stoney.jpg",
            ReleaseDate = new DateOnly(2016, 12, 9),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("b2d7f920-68b2-4ba4-a99e-abf3069f887c"), SimilarAlbumId = Guid.Parse("1c3a213b-aa9b-4c8b-a393-4c881522eebb") },
                             new() { AlbumId = Guid.Parse("b2d7f920-68b2-4ba4-a99e-abf3069f887c"), SimilarAlbumId = Guid.Parse("a8a214b9-c3d1-420e-9e63-3418c9367e81") }],
            CreatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("1c3a213b-aa9b-4c8b-a393-4c881522eebb"),
            Title = "Kid A",
            ImageUrl = "https://kid_a.jpg",
            ReleaseDate = new DateOnly(2020, 3, 6),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("1c3a213b-aa9b-4c8b-a393-4c881522eebb"), SimilarAlbumId = Guid.Parse("a8a214b9-c3d1-420e-9e63-3418c9367e81") },
                             new() { AlbumId = Guid.Parse("1c3a213b-aa9b-4c8b-a393-4c881522eebb"), SimilarAlbumId = Guid.Parse("8f5296a1-0df2-49d5-9b69-6c6afe6c8e18") }],
            CreatedAt = new DateTime(2024, 8, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a8a214b9-c3d1-420e-9e63-3418c9367e81"),
            Title = "Ghost Stories",
            ImageUrl = "https://ghost_stories.jpg",
            ReleaseDate = new DateOnly(2014, 5, 16),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("a8a214b9-c3d1-420e-9e63-3418c9367e81"), SimilarAlbumId = Guid.Parse("8f5296a1-0df2-49d5-9b69-6c6afe6c8e18") },
                             new() { AlbumId = Guid.Parse("a8a214b9-c3d1-420e-9e63-3418c9367e81"), SimilarAlbumId = Guid.Parse("5df3c97b-d624-4d55-8a33-4e4d49abdeba") }],
            CreatedAt = new DateTime(2024, 8, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("8f5296a1-0df2-49d5-9b69-6c6afe6c8e18"),
            Title = "Awaken, My Love!",
            ImageUrl = "https://awaken_my_love.jpg",
            ReleaseDate = new DateOnly(2016, 12, 2),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("8f5296a1-0df2-49d5-9b69-6c6afe6c8e18"), SimilarAlbumId = Guid.Parse("5df3c97b-d624-4d55-8a33-4e4d49abdeba") },
                             new() { AlbumId = Guid.Parse("8f5296a1-0df2-49d5-9b69-6c6afe6c8e18"), SimilarAlbumId = Guid.Parse("2fd6f336-fc89-45e8-baae-2c8d3ec384cb") }],
            CreatedAt = new DateTime(2024, 8, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("5df3c97b-d624-4d55-8a33-4e4d49abdeba"),
            Title = "4 Your Eyes Only",
            ImageUrl = "https://4_your_eyes_only.jpg",
            ReleaseDate = new DateOnly(2016, 12, 9),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("5df3c97b-d624-4d55-8a33-4e4d49abdeba"), SimilarAlbumId = Guid.Parse("2fd6f336-fc89-45e8-baae-2c8d3ec384cb") },
                             new() { AlbumId = Guid.Parse("5df3c97b-d624-4d55-8a33-4e4d49abdeba"), SimilarAlbumId = Guid.Parse("71d6e3ac-413b-46f1-9aaf-c36d4056b7c2") }],
            CreatedAt = new DateTime(2024, 8, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2fd6f336-fc89-45e8-baae-2c8d3ec384cb"),
            Title = "Man on the Moon",
            ImageUrl = "https://man_on_the_moon.jpg",
            ReleaseDate = new DateOnly(2009, 9, 15),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("2fd6f336-fc89-45e8-baae-2c8d3ec384cb"), SimilarAlbumId = Guid.Parse("71d6e3ac-413b-46f1-9aaf-c36d4056b7c2") },
                             new() { AlbumId = Guid.Parse("2fd6f336-fc89-45e8-baae-2c8d3ec384cb"), SimilarAlbumId = Guid.Parse("67a03545-13d2-45b6-bf01-668c9ee315bb") }],
            CreatedAt = new DateTime(2024, 8, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 15, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 15, 0, 0, 0)
        }, new()
        {
            Id = Guid.Parse("71d6e3ac-413b-46f1-9aaf-c36d4056b7c2"),
            Title = "American Teen",
            ImageUrl = "https://american_teen.jpg",
            ReleaseDate = new DateOnly(2017, 3, 3),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("71d6e3ac-413b-46f1-9aaf-c36d4056b7c2"), SimilarAlbumId = Guid.Parse("67a03545-13d2-45b6-bf01-668c9ee315bb") },
                             new() { AlbumId = Guid.Parse("71d6e3ac-413b-46f1-9aaf-c36d4056b7c2"), SimilarAlbumId = Guid.Parse("b8107cbb-18fb-49b0-876b-86777a3bafff") }],
            CreatedAt = new DateTime(2024, 8, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 16, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 16, 0, 0, 0)
        }, new()
        {
            Id = Guid.Parse("67a03545-13d2-45b6-bf01-668c9ee315bb"),
            Title = "Carrie & Lowell",
            ImageUrl = "https://carrie_and_lowell.jpg",
            ReleaseDate = new DateOnly(2015, 3, 31),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("67a03545-13d2-45b6-bf01-668c9ee315bb"), SimilarAlbumId = Guid.Parse("b8107cbb-18fb-49b0-876b-86777a3bafff") },
                             new() { AlbumId = Guid.Parse("67a03545-13d2-45b6-bf01-668c9ee315bb"), SimilarAlbumId = Guid.Parse("11d77e95-f4f2-4a6f-980b-674d81d8c1d8") }],
            CreatedAt = new DateTime(2024, 8, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 17, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 17, 0, 0, 0)
        }, new()
        {
            Id = Guid.Parse("b8107cbb-18fb-49b0-876b-86777a3bafff"),
            Title = "Beauty Behind the Madness",
            ImageUrl = "https://beauty_behind_the_madness.jpg",
            ReleaseDate = new DateOnly(2015, 8, 28),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("b8107cbb-18fb-49b0-876b-86777a3bafff"), SimilarAlbumId = Guid.Parse("11d77e95-f4f2-4a6f-980b-674d81d8c1d8") },
                             new() { AlbumId = Guid.Parse("b8107cbb-18fb-49b0-876b-86777a3bafff"), SimilarAlbumId = Guid.Parse("6ee76a77-2be4-42e3-8417-e60d282cffcb") }],
            CreatedAt = new DateTime(2024, 8, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 18, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 18, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("11d77e95-f4f2-4a6f-980b-674d81d8c1d8"),
            Title = "Reputation",
            ImageUrl = "https://reputation.jpg",
            ReleaseDate = new DateOnly(2017, 11, 10),
            SimilarAlbums = [new() { AlbumId = Guid.Parse("11d77e95-f4f2-4a6f-980b-674d81d8c1d8"), SimilarAlbumId = Guid.Parse("6ee76a77-2be4-42e3-8417-e60d282cffcb") },
                             new() { AlbumId = Guid.Parse("11d77e95-f4f2-4a6f-980b-674d81d8c1d8"), SimilarAlbumId = Guid.Parse("b58f5f3f-d5e8-49f3-8b12-cfe33f762b4f") }],
            CreatedAt = new DateTime(2024, 8, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 19, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 19, 12, 0, 0)
        }];

        public static readonly Album _newAlbum = new()
        {
            Title = "Cold Mode",
            ImageUrl = "https://cold_mode.jpg",
            ReleaseDate = new DateOnly(2023, 6, 18)
        };

        public static readonly List<AlbumDto> _albumDtos = _albums.Select(ToDto).ToList();

        public static readonly AlbumDto _newAlbumDto = ToDto(_newAlbum);

        public static readonly List<Album> _albumsPagination = _albums.Where(album => album.DeletedAt == null).Take(10).ToList();

        public static readonly List<AlbumDto> _albumDtosPagination = _albumsPagination.Select(ToDto).ToList();

        public static readonly AlbumPaginationRequest _albumPayload = new(SortCriteria: null, SearchCriteria: null, Title: null, DateRange: null);

        public static AlbumDto ToDto(Album album) => new()
        {
            Id = album.Id,
            Title = album.Title,
            ImageUrl = album.ImageUrl,
            ReleaseDate = album.ReleaseDate,
            SimilarAlbumsIds = album.SimilarAlbums.Select(albumPair => albumPair.SimilarAlbumId).ToList(),
            CreatedAt = album.CreatedAt,
            UpdatedAt = album.UpdatedAt,
            DeletedAt = album.DeletedAt
        };
    }
}