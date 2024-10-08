﻿using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Test.Mocks
{
    public class ArtistMock
    {
        private ArtistMock() { }

        public static readonly List<Artist> _artists = [new()
        {
            Id = Guid.Parse("4e75ecdd-aafe-4c35-836b-1b83fc7b8f88"),
            Name = "The Weeknd",
            ImageUrl = "https://the_weeknd.jpg",
            Bio = "Canadian singer, songwriter, and record producer known for his flamboyant style and broad vocal range.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("4e75ecdd-aafe-4c35-836b-1b83fc7b8f88"), SimilarArtistId = Guid.Parse("8c301aa9-6d56-4c06-b1f2-9b9956979345") },
                              new() { ArtistId = Guid.Parse("4e75ecdd-aafe-4c35-836b-1b83fc7b8f88"), SimilarArtistId = Guid.Parse("fc3e6343-6f10-42e8-91c9-2d36c0154c42") }],
            CreatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 1, 0, 0, 0),
            DeletedAt = null
        }, new() 
        {
            Id = Guid.Parse("8c301aa9-6d56-4c06-b1f2-9b9956979345"),
            Name = "Lana Del Rey",
            ImageUrl = "https://lana_del_rey.jpg",
            Bio = "American singer and songwriter noted for her cinematic sound and aesthetic in music videos.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("8c301aa9-6d56-4c06-b1f2-9b9956979345"), SimilarArtistId = Guid.Parse("fc3e6343-6f10-42e8-91c9-2d36c0154c42") },
                              new() { ArtistId = Guid.Parse("8c301aa9-6d56-4c06-b1f2-9b9956979345"), SimilarArtistId = Guid.Parse("96de52c4-7dbf-4db1-baed-255c8f9215db") }],
            CreatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("fc3e6343-6f10-42e8-91c9-2d36c0154c42"),
            Name = "Daft Punk",
            ImageUrl = "https://daft_punk.jpg",
            Bio = "French electronic music duo known for their visual stylization and disguises associated with their music.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("fc3e6343-6f10-42e8-91c9-2d36c0154c42"), SimilarArtistId = Guid.Parse("96de52c4-7dbf-4db1-baed-255c8f9215db") },
                              new() { ArtistId = Guid.Parse("fc3e6343-6f10-42e8-91c9-2d36c0154c42"), SimilarArtistId = Guid.Parse("66c4b36e-0fac-4911-9e9a-98d42d8ece10") }],
            CreatedAt = new DateTime(2024, 7, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("96de52c4-7dbf-4db1-baed-255c8f9215db"),
            Name = "Kendrick Lamar",
            ImageUrl = "https://kendrick_lamar.jpg",
            Bio = "American rapper and songwriter acclaimed for his progressive musical styles and thoughtful lyrics.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("96de52c4-7dbf-4db1-baed-255c8f9215db"), SimilarArtistId = Guid.Parse("66c4b36e-0fac-4911-9e9a-98d42d8ece10") },
                              new() { ArtistId = Guid.Parse("96de52c4-7dbf-4db1-baed-255c8f9215db"), SimilarArtistId = Guid.Parse("1d9d1b54-6430-45c5-85d6-ae4d2288223f") }],
            CreatedAt = new DateTime(2024, 7, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("66c4b36e-0fac-4911-9e9a-98d42d8ece10"),
            Name = "Frank Ocean",
            ImageUrl = "https://frank_ocean.jpg",
            Bio = "American singer, songwriter, and photographer known for his idiosyncratic musical approach.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("66c4b36e-0fac-4911-9e9a-98d42d8ece10"), SimilarArtistId = Guid.Parse("1d9d1b54-6430-45c5-85d6-ae4d2288223f") },
                              new() { ArtistId = Guid.Parse("66c4b36e-0fac-4911-9e9a-98d42d8ece10"), SimilarArtistId = Guid.Parse("6fbd52c1-d395-4349-975b-70916be1b8d0") }],
            CreatedAt = new DateTime(2024, 7, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("1d9d1b54-6430-45c5-85d6-ae4d2288223f"),
            Name = "Sam Smith",
            ImageUrl = "https://sam_smith.jpg",
            Bio = "English singer and songwriter known for their soulful voice and emotive lyrics.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("1d9d1b54-6430-45c5-85d6-ae4d2288223f"), SimilarArtistId = Guid.Parse("6fbd52c1-d395-4349-975b-70916be1b8d0") },
                              new() { ArtistId = Guid.Parse("1d9d1b54-6430-45c5-85d6-ae4d2288223f"), SimilarArtistId = Guid.Parse("b37cefef-c6d2-49a7-8b12-cdeacf5fe4c1") }],
            CreatedAt = new DateTime(2024, 7, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("6fbd52c1-d395-4349-975b-70916be1b8d0"),
            Name = "Pink Floyd",
            ImageUrl = "https://pink_floyd.jpg",
            Bio = "British rock band famed for their philosophical lyrics, sonic experimentation, and elaborate live shows.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("6fbd52c1-d395-4349-975b-70916be1b8d0"), SimilarArtistId = Guid.Parse("b37cefef-c6d2-49a7-8b12-cdeacf5fe4c1") },
                              new() { ArtistId = Guid.Parse("6fbd52c1-d395-4349-975b-70916be1b8d0"), SimilarArtistId = Guid.Parse("08af1af0-b71e-44bc-b5d3-82dab55d6b81") }],
            CreatedAt = new DateTime(2024, 7, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("b37cefef-c6d2-49a7-8b12-cdeacf5fe4c1"),
            Name = "Michael Jackson",
            ImageUrl = "https://michael_jackson.jpg",
            Bio = "American singer, songwriter, and dancer dubbed the \"King of Pop,\" he is regarded as one of the most significant cultural figures of the 20th century.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("b37cefef-c6d2-49a7-8b12-cdeacf5fe4c1"), SimilarArtistId = Guid.Parse("08af1af0-b71e-44bc-b5d3-82dab55d6b81") },
                              new() { ArtistId = Guid.Parse("b37cefef-c6d2-49a7-8b12-cdeacf5fe4c1"), SimilarArtistId = Guid.Parse("2f715aa4-7597-4f8e-9594-1f0245d16a97") }],
            CreatedAt = new DateTime(2024, 7, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("08af1af0-b71e-44bc-b5d3-82dab55d6b81"),
            Name = "David Bowie",
            ImageUrl = "https://david_bowie.jpg",
            Bio = "English singer-songwriter and actor known for his reinvention and visual presentation.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("08af1af0-b71e-44bc-b5d3-82dab55d6b81"), SimilarArtistId = Guid.Parse("2f715aa4-7597-4f8e-9594-1f0245d16a97") },
                              new() { ArtistId = Guid.Parse("08af1af0-b71e-44bc-b5d3-82dab55d6b81"), SimilarArtistId = Guid.Parse("6a1e646b-e42d-4336-88c6-cb44d5f50682") }],
            CreatedAt = new DateTime(2024, 7, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2f715aa4-7597-4f8e-9594-1f0245d16a97"),
            Name = "Taylor Swift",
            ImageUrl = "https://taylor_swift.jpg",
            Bio = "American singer-songwriter whose discography spans diverse genres, and is known for her narrative songwriting.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("2f715aa4-7597-4f8e-9594-1f0245d16a97"), SimilarArtistId = Guid.Parse("6a1e646b-e42d-4336-88c6-cb44d5f50682") },
                              new() { ArtistId = Guid.Parse("2f715aa4-7597-4f8e-9594-1f0245d16a97"), SimilarArtistId = Guid.Parse("3f7b8c31-2f1f-4a5b-8b01-3395bf4c0931") }],
            CreatedAt = new DateTime(2024, 7, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("6a1e646b-e42d-4336-88c6-cb44d5f50682"),
            Name = "Harry Styles",
            ImageUrl = "https://harry_styles.jpg",
            Bio = "British singer, songwriter, and actor who began his music career as a member of the boy band One Direction.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("6a1e646b-e42d-4336-88c6-cb44d5f50682"), SimilarArtistId = Guid.Parse("3f7b8c31-2f1f-4a5b-8b01-3395bf4c0931") },
                              new() { ArtistId = Guid.Parse("6a1e646b-e42d-4336-88c6-cb44d5f50682"), SimilarArtistId = Guid.Parse("3a448c5a-e1c8-456e-955f-ed2f98d87aa6") }],
            CreatedAt = new DateTime(2024, 7, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3f7b8c31-2f1f-4a5b-8b01-3395bf4c0931"),
            Name = "Radiohead",
            ImageUrl = "https://radiohead.jpg",
            Bio = "English rock band known for their eclectic style and exploration of existential angst and alienation.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("3f7b8c31-2f1f-4a5b-8b01-3395bf4c0931"), SimilarArtistId = Guid.Parse("3a448c5a-e1c8-456e-955f-ed2f98d87aa6") },
                              new() { ArtistId = Guid.Parse("3f7b8c31-2f1f-4a5b-8b01-3395bf4c0931"), SimilarArtistId = Guid.Parse("9e198401-4c15-4d2d-b9fb-63001bee256e") }],
            CreatedAt = new DateTime(2024, 7, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("3a448c5a-e1c8-456e-955f-ed2f98d87aa6"),
            Name = "Beyonce",
            ImageUrl = "https://beyonce.jpg",
            Bio = "American singer, songwriter, and actress. Born and raised in Houston, Texas, Beyoncé performed in various singing and dancing competitions as a child.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("3a448c5a-e1c8-456e-955f-ed2f98d87aa6"), SimilarArtistId = Guid.Parse("9e198401-4c15-4d2d-b9fb-63001bee256e") },
                              new() { ArtistId = Guid.Parse("3a448c5a-e1c8-456e-955f-ed2f98d87aa6"), SimilarArtistId = Guid.Parse("60d224af-d862-43e5-b6f8-9c977feb2cfe") }],
            CreatedAt = new DateTime(2024, 7, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("9e198401-4c15-4d2d-b9fb-63001bee256e"),
            Name = "Adele",
            ImageUrl = "https://adele.jpg",
            Bio = "English singer-songwriter who has sold millions of albums worldwide and won a total of 15 Grammys as well as an Oscar.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("9e198401-4c15-4d2d-b9fb-63001bee256e"), SimilarArtistId = Guid.Parse("60d224af-d862-43e5-b6f8-9c977feb2cfe") },
                              new() { ArtistId = Guid.Parse("9e198401-4c15-4d2d-b9fb-63001bee256e"), SimilarArtistId = Guid.Parse("e5b25542-e19b-4661-a5e8-72fa34bfb7c9") }],
            CreatedAt = new DateTime(2024, 7, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("60d224af-d862-43e5-b6f8-9c977feb2cfe"),
            Name = "ABBA",
            ImageUrl = "https://abba.jpg",
            Bio = "Swedish pop group formed in Stockholm in 1972, known for hits like \"Dancing Queen\" and \"Mamma Mia.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("60d224af-d862-43e5-b6f8-9c977feb2cfe"), SimilarArtistId = Guid.Parse("e5b25542-e19b-4661-a5e8-72fa34bfb7c9") },
                              new() { ArtistId = Guid.Parse("60d224af-d862-43e5-b6f8-9c977feb2cfe"), SimilarArtistId = Guid.Parse("bdfd8e1e-09f2-4a2b-a2ec-6381b3f63616") }],
            CreatedAt = new DateTime(2024, 7, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 15, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e5b25542-e19b-4661-a5e8-72fa34bfb7c9"),
            Name = "Ed Sheeran",
            ImageUrl = "https://ed_sheeran.jpg",
            Bio = "English singer-songwriter known for his catchy pop songs and heartfelt lyrics.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("e5b25542-e19b-4661-a5e8-72fa34bfb7c9"), SimilarArtistId = Guid.Parse("bdfd8e1e-09f2-4a2b-a2ec-6381b3f63616") },
                              new() { ArtistId = Guid.Parse("e5b25542-e19b-4661-a5e8-72fa34bfb7c9"), SimilarArtistId = Guid.Parse("7aeef64b-accf-4e2c-8009-1e62cbc85689") }],
            CreatedAt = new DateTime(2024, 7, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 16, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("bdfd8e1e-09f2-4a2b-a2ec-6381b3f63616"),
            Name = "J Balvin",
            ImageUrl = "https://j_balvin.jpg",
            Bio = "Colombian reggaeton singer who has been referred to as the \"Prince of Reggaeton\" and is one of the best-selling Latin music artists.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("bdfd8e1e-09f2-4a2b-a2ec-6381b3f63616"), SimilarArtistId = Guid.Parse("7aeef64b-accf-4e2c-8009-1e62cbc85689") },
                              new() { ArtistId = Guid.Parse("bdfd8e1e-09f2-4a2b-a2ec-6381b3f63616"), SimilarArtistId = Guid.Parse("f33a1cf0-5c51-4b65-a0fe-88d28cb33155") }],
            CreatedAt = new DateTime(2024, 7, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 17, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("7aeef64b-accf-4e2c-8009-1e62cbc85689"),
            Name = "Travis Scott",
            ImageUrl = "https://travis_scott.jpg",
            Bio = "American rapper, singer, songwriter, and record producer known for his highly auto-tuned half-sung/half-rapped vocal style.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("7aeef64b-accf-4e2c-8009-1e62cbc85689"), SimilarArtistId = Guid.Parse("f33a1cf0-5c51-4b65-a0fe-88d28cb33155") },
                              new() { ArtistId = Guid.Parse("7aeef64b-accf-4e2c-8009-1e62cbc85689"), SimilarArtistId = Guid.Parse("124f550a-50d0-4f6a-b445-1b9a31a48571") }],
            CreatedAt = new DateTime(2024, 7, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 18, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f33a1cf0-5c51-4b65-a0fe-88d28cb33155"),
            Name = "Post Malone",
            ImageUrl = "https://post_malone.jpg",
            Bio = "American singer, rapper, and songwriter known for blending genres and subgenres of pop, rap, and rock.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("f33a1cf0-5c51-4b65-a0fe-88d28cb33155"), SimilarArtistId = Guid.Parse("124f550a-50d0-4f6a-b445-1b9a31a48571") },
                              new() { ArtistId = Guid.Parse("f33a1cf0-5c51-4b65-a0fe-88d28cb33155"), SimilarArtistId = Guid.Parse("dc3f0a12-0809-4fec-b70d-8a2035fc3cf0") }],
            CreatedAt = new DateTime(2024, 7, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 19, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("124f550a-50d0-4f6a-b445-1b9a31a48571"),
            Name = "Amy Winehouse",
            ImageUrl = "https://amy_winehouse.jpg",
            Bio = "English singer and songwriter known for her deep, expressive contralto vocals and her eclectic mix of musical genres.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("124f550a-50d0-4f6a-b445-1b9a31a48571"), SimilarArtistId = Guid.Parse("dc3f0a12-0809-4fec-b70d-8a2035fc3cf0") },
                              new() { ArtistId = Guid.Parse("124f550a-50d0-4f6a-b445-1b9a31a48571"), SimilarArtistId = Guid.Parse("90d3f8b5-a0fd-4616-a5ed-ebea0340b29f") }],
            CreatedAt = new DateTime(2024, 7, 20, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 20, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("dc3f0a12-0809-4fec-b70d-8a2035fc3cf0"),
            Name = "Kanye West",
            ImageUrl = "https://kanye_west.jpg",
            Bio = "American rapper, singer, songwriter, record producer, and fashion designer, known for his influence on the music industry.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("dc3f0a12-0809-4fec-b70d-8a2035fc3cf0"), SimilarArtistId = Guid.Parse("90d3f8b5-a0fd-4616-a5ed-ebea0340b29f") },
                              new() { ArtistId = Guid.Parse("dc3f0a12-0809-4fec-b70d-8a2035fc3cf0"), SimilarArtistId = Guid.Parse("2f41b1f0-bf26-406f-95ff-e29ee92b5a3a") }],
            CreatedAt = new DateTime(2024, 7, 21, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 21, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("90d3f8b5-a0fd-4616-a5ed-ebea0340b29f"),
            Name = "Lorde",
            ImageUrl = "https://lorde.jpg",
            Bio = "New Zealand singer-songwriter known for her unconventional musical styles and introspective songwriting.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("90d3f8b5-a0fd-4616-a5ed-ebea0340b29f"), SimilarArtistId = Guid.Parse("2f41b1f0-bf26-406f-95ff-e29ee92b5a3a") },
                              new() { ArtistId = Guid.Parse("90d3f8b5-a0fd-4616-a5ed-ebea0340b29f"), SimilarArtistId = Guid.Parse("6a9fcd23-2c91-4a32-a3c2-f44e35f1a421") }],
            CreatedAt = new DateTime(2024, 7, 22, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 22, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2f41b1f0-bf26-406f-95ff-e29ee92b5a3a"),
            Name = "Drake",
            ImageUrl = "https://drake.jpg",
            Bio = "Canadian rapper, singer, and actor from Toronto. He first gained recognition as an actor on the teen drama television series Degrassi: The Next Generation.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("2f41b1f0-bf26-406f-95ff-e29ee92b5a3a"), SimilarArtistId = Guid.Parse("6a9fcd23-2c91-4a32-a3c2-f44e35f1a421") },
                              new() { ArtistId = Guid.Parse("2f41b1f0-bf26-406f-95ff-e29ee92b5a3a"), SimilarArtistId = Guid.Parse("4b6e5e08-93b2-4f37-8c80-f59c662153e4") }],
            CreatedAt = new DateTime(2024, 7, 23, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 23, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("6a9fcd23-2c91-4a32-a3c2-f44e35f1a421"),
            Name = "Rihanna",
            ImageUrl = "https://rihanna.jpg",
            Bio = "Barbadian singer, actress, fashion designer, and businesswoman known for embracing various musical styles and reinventing her image throughout her career.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("6a9fcd23-2c91-4a32-a3c2-f44e35f1a421"), SimilarArtistId = Guid.Parse("4b6e5e08-93b2-4f37-8c80-f59c662153e4") },
                              new() { ArtistId = Guid.Parse("6a9fcd23-2c91-4a32-a3c2-f44e35f1a421"), SimilarArtistId = Guid.Parse("0f9d6cf2-497e-48ff-b1fa-440678951c28") }],
            CreatedAt = new DateTime(2024, 7, 24, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 24, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("4b6e5e08-93b2-4f37-8c80-f59c662153e4"),
            Name = "Dua Lipa",
            ImageUrl = "https://dua_lipa.jpg",
            Bio = "English singer and songwriter known for her disco-pop style and strong vocals.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("4b6e5e08-93b2-4f37-8c80-f59c662153e4"), SimilarArtistId = Guid.Parse("0f9d6cf2-497e-48ff-b1fa-440678951c28") },
                              new() { ArtistId = Guid.Parse("4b6e5e08-93b2-4f37-8c80-f59c662153e4"), SimilarArtistId = Guid.Parse("e97b22b8-d9a1-4a5e-bfa1-6ba9cc0758cf") }],
            CreatedAt = new DateTime(2024, 7, 25, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 25, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("0f9d6cf2-497e-48ff-b1fa-440678951c28"),
            Name = "Billie Eilish",
            ImageUrl = "https://billie_eilish.jpg",
            Bio = "American singer and songwriter known for her ethereal indie pop sounds and unique aesthetic.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("0f9d6cf2-497e-48ff-b1fa-440678951c28"), SimilarArtistId = Guid.Parse("e97b22b8-d9a1-4a5e-bfa1-6ba9cc0758cf") },
                              new() { ArtistId = Guid.Parse("0f9d6cf2-497e-48ff-b1fa-440678951c28"), SimilarArtistId = Guid.Parse("78322f23-3f6f-4420-ad26-3a014f5c5ea5") }],
            CreatedAt = new DateTime(2024, 7, 26, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 26, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e97b22b8-d9a1-4a5e-bfa1-6ba9cc0758cf"),
            Name = "Alicia Keys",
            ImageUrl = "https://alicia_keys.jpg",
            Bio = "American singer-songwriter and musician, known for her soulful voice and emotive piano compositions.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("e97b22b8-d9a1-4a5e-bfa1-6ba9cc0758cf"), SimilarArtistId = Guid.Parse("78322f23-3f6f-4420-ad26-3a014f5c5ea5") },
                              new() { ArtistId = Guid.Parse("e97b22b8-d9a1-4a5e-bfa1-6ba9cc0758cf"), SimilarArtistId = Guid.Parse("2a0c3b05-5f01-4b6b-84ea-b22f8bc301dc") }],
            CreatedAt = new DateTime(2024, 7, 27, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 27, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("78322f23-3f6f-4420-ad26-3a014f5c5ea5"),
            Name = "Arctic Monkeys",
            ImageUrl = "https://arctic_monkeys.jpg",
            Bio = "British rock band known for their energetic rock sound and detailed lyrics.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("78322f23-3f6f-4420-ad26-3a014f5c5ea5"), SimilarArtistId = Guid.Parse("2a0c3b05-5f01-4b6b-84ea-b22f8bc301dc") },
                              new() { ArtistId = Guid.Parse("78322f23-3f6f-4420-ad26-3a014f5c5ea5"), SimilarArtistId = Guid.Parse("4d905d70-95e3-4e76-bf59-ace2d3ec05ad") }],
            CreatedAt = new DateTime(2024, 7, 28, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 28, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2a0c3b05-5f01-4b6b-84ea-b22f8bc301dc"),
            Name = "Tame Impala",
            ImageUrl = "https://tame_impala.jpg",
            Bio = "Australian musical project led by multi-instrumentalist Kevin Parker, known for their distinctive psychedelic sound.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("2a0c3b05-5f01-4b6b-84ea-b22f8bc301dc"), SimilarArtistId = Guid.Parse("4d905d70-95e3-4e76-bf59-ace2d3ec05ad") },
                              new() { ArtistId = Guid.Parse("2a0c3b05-5f01-4b6b-84ea-b22f8bc301dc"), SimilarArtistId = Guid.Parse("9cb82f47-a287-403a-a5c6-0f821d7d4c8a") }],
            CreatedAt = new DateTime(2024, 7, 29, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 29, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("4d905d70-95e3-4e76-bf59-ace2d3ec05ad"),
            Name = "Grimes",
            ImageUrl = "https://grimes.jpg",
            Bio = "Canadian musician and visual artist known for her eclectic music style and high-pitched voice.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("4d905d70-95e3-4e76-bf59-ace2d3ec05ad"), SimilarArtistId = Guid.Parse("9cb82f47-a287-403a-a5c6-0f821d7d4c8a") },
                              new() { ArtistId = Guid.Parse("4d905d70-95e3-4e76-bf59-ace2d3ec05ad"), SimilarArtistId = Guid.Parse("0ac3e817-eda5-4a60-9e8c-6a82e2a3e5f3") }],
            CreatedAt = new DateTime(2024, 7, 30, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 30, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("9cb82f47-a287-403a-a5c6-0f821d7d4c8a"),
            Name = "SZA",
            ImageUrl = "https://sza.jpg",
            Bio = "American singer-songwriter known for her blend of R&B, soul, and hip-hop and thoughtful, provocative lyrics.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("9cb82f47-a287-403a-a5c6-0f821d7d4c8a"), SimilarArtistId = Guid.Parse("0ac3e817-eda5-4a60-9e8c-6a82e2a3e5f3") },
                              new() { ArtistId = Guid.Parse("9cb82f47-a287-403a-a5c6-0f821d7d4c8a"), SimilarArtistId = Guid.Parse("cd1f9668-1eed-4a1c-9894-f7b3e3b9b486") }],
            CreatedAt = new DateTime(2024, 7, 31, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 7, 31, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("0ac3e817-eda5-4a60-9e8c-6a82e2a3e5f3"),
            Name = "The Lumineers",
            ImageUrl = "https://the_lumineers.jpg",
            Bio = "American folk-rock band known for their rustic sound and emotive lyrics.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("0ac3e817-eda5-4a60-9e8c-6a82e2a3e5f3"), SimilarArtistId = Guid.Parse("cd1f9668-1eed-4a1c-9894-f7b3e3b9b486") },
                              new() { ArtistId = Guid.Parse("0ac3e817-eda5-4a60-9e8c-6a82e2a3e5f3"), SimilarArtistId = Guid.Parse("f98851a4-dd7b-45df-83d2-77df1f53f678") }],
            CreatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 1, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("cd1f9668-1eed-4a1c-9894-f7b3e3b9b486"),
            Name = "Tyler, The Creator",
            ImageUrl = "https://tyler_the_creator.jpg",
            Bio = "American rapper and producer known for his innovative production and provocative lyrics.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("cd1f9668-1eed-4a1c-9894-f7b3e3b9b486"), SimilarArtistId = Guid.Parse("f98851a4-dd7b-45df-83d2-77df1f53f678") },
                              new() { ArtistId = Guid.Parse("cd1f9668-1eed-4a1c-9894-f7b3e3b9b486"), SimilarArtistId = Guid.Parse("2ba4fb8c-22d7-4ba6-9e95-1a1bfaa0fb4b") }],
            CreatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 2, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f98851a4-dd7b-45df-83d2-77df1f53f678"),
            Name = "Norah Jones",
            ImageUrl = "https://norah_jones.jpg",
            Bio = "American singer, songwriter, and pianist known for her jazzy voice and ability to blend different genres including jazz and pop.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("f98851a4-dd7b-45df-83d2-77df1f53f678"), SimilarArtistId = Guid.Parse("2ba4fb8c-22d7-4ba6-9e95-1a1bfaa0fb4b") },
                              new() { ArtistId = Guid.Parse("f98851a4-dd7b-45df-83d2-77df1f53f678"), SimilarArtistId = Guid.Parse("bb90ec41-ce2d-4165-993b-c5ee2aabe8ce") }],
            CreatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 3, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("2ba4fb8c-22d7-4ba6-9e95-1a1bfaa0fb4b"),
            Name = "John Legend",
            ImageUrl = "https://john_legend.jpg",
            Bio = "American singer, songwriter, producer, and actor known for his smooth vocal quality and thoughtful musicianship.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("2ba4fb8c-22d7-4ba6-9e95-1a1bfaa0fb4b"), SimilarArtistId = Guid.Parse("bb90ec41-ce2d-4165-993b-c5ee2aabe8ce") },
                              new() { ArtistId = Guid.Parse("2ba4fb8c-22d7-4ba6-9e95-1a1bfaa0fb4b"), SimilarArtistId = Guid.Parse("e1d9cee7-ecde-4d07-bc64-3cec9c3f7b04") }],
            CreatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 4, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("bb90ec41-ce2d-4165-993b-c5ee2aabe8ce"),
            Name = "Kid Cudi",
            ImageUrl = "https://kid_cudi.jpg",
            Bio = "American rapper and actor known for his innovative sound that has influenced a generation of artists.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("bb90ec41-ce2d-4165-993b-c5ee2aabe8ce"), SimilarArtistId = Guid.Parse("e1d9cee7-ecde-4d07-bc64-3cec9c3f7b04") },
                              new() { ArtistId = Guid.Parse("bb90ec41-ce2d-4165-993b-c5ee2aabe8ce"), SimilarArtistId = Guid.Parse("a89d646a-230a-43ee-bd42-7988a4f80711") }],
            CreatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 5, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("e1d9cee7-ecde-4d07-bc64-3cec9c3f7b04"),
            Name = "Khalid",
            ImageUrl = "https://khalid.jpg",
            Bio = "American singer known for his rich, soulful voice and introspective themes.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("e1d9cee7-ecde-4d07-bc64-3cec9c3f7b04"), SimilarArtistId = Guid.Parse("a89d646a-230a-43ee-bd42-7988a4f80711") },
                              new() { ArtistId = Guid.Parse("e1d9cee7-ecde-4d07-bc64-3cec9c3f7b04"), SimilarArtistId = Guid.Parse("d2a3e8cf-b32d-4abe-9ae8-6ce7a89d2f64") }],
            CreatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 6, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("a89d646a-230a-43ee-bd42-7988a4f80711"),
            Name = "Sufjan Stevens",
            ImageUrl = "https://sufjan_stevens.jpg",
            Bio = "American singer-songwriter known for his lyrical and instrumental complexity and exploration of religious and spiritual themes.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("a89d646a-230a-43ee-bd42-7988a4f80711"), SimilarArtistId = Guid.Parse("d2a3e8cf-b32d-4abe-9ae8-6ce7a89d2f64") },
                              new() { ArtistId = Guid.Parse("a89d646a-230a-43ee-bd42-7988a4f80711"), SimilarArtistId = Guid.Parse("fe3b72a5-0aa0-49f3-8407-accf2b081227") }],
            CreatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 7, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d2a3e8cf-b32d-4abe-9ae8-6ce7a89d2f64"),
            Name = "Hozier",
            ImageUrl = "https://hozier.jpg",
            Bio = "Irish singer and songwriter known for his poetic lyrics and soulful rock influences.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("d2a3e8cf-b32d-4abe-9ae8-6ce7a89d2f64"), SimilarArtistId = Guid.Parse("fe3b72a5-0aa0-49f3-8407-accf2b081227") },
                              new() { ArtistId = Guid.Parse("d2a3e8cf-b32d-4abe-9ae8-6ce7a89d2f64"), SimilarArtistId = Guid.Parse("1e00d247-6e57-413e-9ede-d4835f5cfbd1") }],
            CreatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 8, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("fe3b72a5-0aa0-49f3-8407-accf2b081227"),
            Name = "Maroon 5",
            ImageUrl = "https://maroon_5.jpg",
            Bio = "American pop rock band known for their catchy pop hits and dynamic stage presence.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("fe3b72a5-0aa0-49f3-8407-accf2b081227"), SimilarArtistId = Guid.Parse("1e00d247-6e57-413e-9ede-d4835f5cfbd1") },
                              new() { ArtistId = Guid.Parse("fe3b72a5-0aa0-49f3-8407-accf2b081227"), SimilarArtistId = Guid.Parse("f32c8b31-d086-4d22-95e3-efb8e6cebbd8") }],
            CreatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 9, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("1e00d247-6e57-413e-9ede-d4835f5cfbd1"),
            Name = "Lady Gaga",
            ImageUrl = "https://lady_gaga.jpg",
            Bio = "American singer, songwriter, and actress known for her unconventionality and provocative work as well as visual experimentation.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("1e00d247-6e57-413e-9ede-d4835f5cfbd1"), SimilarArtistId = Guid.Parse("f32c8b31-d086-4d22-95e3-efb8e6cebbd8") },
                              new() { ArtistId = Guid.Parse("1e00d247-6e57-413e-9ede-d4835f5cfbd1"), SimilarArtistId = Guid.Parse("6ab42fcb-33e8-4f13-ba58-c1ad5d98d543") }],
            CreatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 10, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f32c8b31-d086-4d22-95e3-efb8e6cebbd8"),
            Name = "Shakira",
            ImageUrl = "https://shakira.jpg",
            Bio = "Colombian singer, songwriter, and dancer known for her blend of Latin music with mainstream pop and her energetic belly dancing.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("f32c8b31-d086-4d22-95e3-efb8e6cebbd8"), SimilarArtistId = Guid.Parse("6ab42fcb-33e8-4f13-ba58-c1ad5d98d543") },
                              new() { ArtistId = Guid.Parse("f32c8b31-d086-4d22-95e3-efb8e6cebbd8"), SimilarArtistId = Guid.Parse("99aa6f02-c3c4-44fa-aa2a-23f31c2f355b") }],
            CreatedAt = new DateTime(2024, 8, 11, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 11, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("6ab42fcb-33e8-4f13-ba58-c1ad5d98d543"),
            Name = "Coldplay",
            ImageUrl = "https://coldplay.jpg",
            Bio = "British rock band known for their soulful melodies and strong lyrical themes of love and personal introspection.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("6ab42fcb-33e8-4f13-ba58-c1ad5d98d543"), SimilarArtistId = Guid.Parse("99aa6f02-c3c4-44fa-aa2a-23f31c2f355b") },
                              new() { ArtistId = Guid.Parse("6ab42fcb-33e8-4f13-ba58-c1ad5d98d543"), SimilarArtistId = Guid.Parse("f4f1479f-ccb7-4519-a636-fd3f1f8f921c") }],
            CreatedAt = new DateTime(2024, 8, 12, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 12, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("99aa6f02-c3c4-44fa-aa2a-23f31c2f355b"),
            Name = "BTS",
            ImageUrl = "https://bts.jpg",
            Bio = "South Korean boy band sensation known for their global impact on pop music and dynamic choreography.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("99aa6f02-c3c4-44fa-aa2a-23f31c2f355b"), SimilarArtistId = Guid.Parse("f4f1479f-ccb7-4519-a636-fd3f1f8f921c") },
                              new() { ArtistId = Guid.Parse("99aa6f02-c3c4-44fa-aa2a-23f31c2f355b"), SimilarArtistId = Guid.Parse("d41d8cd9-245e-45ff-a5a7-d5d80a42f118") }],
            CreatedAt = new DateTime(2024, 8, 13, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 13, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("f4f1479f-ccb7-4519-a636-fd3f1f8f921c"),
            Name = "Juanes",
            ImageUrl = "https://juanes.jpg",
            Bio = "Colombian rock musician known for his focus on universal themes and his advocacy for peace in Latin America.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("f4f1479f-ccb7-4519-a636-fd3f1f8f921c"), SimilarArtistId = Guid.Parse("d41d8cd9-245e-45ff-a5a7-d5d80a42f118") },
                              new() { ArtistId = Guid.Parse("f4f1479f-ccb7-4519-a636-fd3f1f8f921c"), SimilarArtistId = Guid.Parse("98087cf2-9ae8-46b8-9bba-4cd0f63c8c08") }],
            CreatedAt = new DateTime(2024, 8, 14, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 14, 0, 0, 0),
            DeletedAt = null
        }, new()
        {
            Id = Guid.Parse("d41d8cd9-245e-45ff-a5a7-d5d80a42f118"),
            Name = "Celine Dion",
            ImageUrl = "https://celine_dion.jpg",
            Bio = "Canadian singer known for her powerful vocals and emotional ballads, having achieved global fame.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("d41d8cd9-245e-45ff-a5a7-d5d80a42f118"), SimilarArtistId = Guid.Parse("98087cf2-9ae8-46b8-9bba-4cd0f63c8c08") },
                              new() { ArtistId = Guid.Parse("d41d8cd9-245e-45ff-a5a7-d5d80a42f118"), SimilarArtistId = Guid.Parse("da17e22b-8b95-4f85-83d3-57a1ec9613e7") }],
            CreatedAt = new DateTime(2024, 8, 15, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 15, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 15, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("98087cf2-9ae8-46b8-9bba-4cd0f63c8c08"),
            Name = "Metallica",
            ImageUrl = "https://metallica.jpg",
            Bio = "American heavy metal band renowned for their fast tempos, aggressive musicianship, and deep lyrical content.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("98087cf2-9ae8-46b8-9bba-4cd0f63c8c08"), SimilarArtistId = Guid.Parse("da17e22b-8b95-4f85-83d3-57a1ec9613e7") },
                              new() { ArtistId = Guid.Parse("98087cf2-9ae8-46b8-9bba-4cd0f63c8c08"), SimilarArtistId = Guid.Parse("3c16dffc-25a0-45ac-804d-cfcfbe730f1b") }],
            CreatedAt = new DateTime(2024, 8, 16, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 16, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 16, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("da17e22b-8b95-4f85-83d3-57a1ec9613e7"),
            Name = "Enrique Iglesias",
            ImageUrl = "https://enrique_iglesias.jpg",
            Bio = "Spanish singer, songwriter, and actor known as the King of Latin Pop and one of the best-selling Latin artists.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("da17e22b-8b95-4f85-83d3-57a1ec9613e7"), SimilarArtistId = Guid.Parse("3c16dffc-25a0-45ac-804d-cfcfbe730f1b") },
                              new() { ArtistId = Guid.Parse("da17e22b-8b95-4f85-83d3-57a1ec9613e7"), SimilarArtistId = Guid.Parse("4fa7fb3d-1aa8-4e91-ba7a-6f4781c01d41") }],
            CreatedAt = new DateTime(2024, 8, 17, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 17, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 17, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("3c16dffc-25a0-45ac-804d-cfcfbe730f1b"),
            Name = "The Rolling Stones",
            ImageUrl = "https://the_rolling_stones.jpg",
            Bio = "English rock band known for their many hits and enduring influence on rock and roll since the 1960s.",
            SimilarArtists = [new() { ArtistId = Guid.Parse("3c16dffc-25a0-45ac-804d-cfcfbe730f1b"), SimilarArtistId = Guid.Parse("4fa7fb3d-1aa8-4e91-ba7a-6f4781c01d41") },
                              new() { ArtistId = Guid.Parse("3c16dffc-25a0-45ac-804d-cfcfbe730f1b"), SimilarArtistId = Guid.Parse("4e75ecdd-aafe-4c35-836b-1b83fc7b8f88") }],
            CreatedAt = new DateTime(2024, 8, 18, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 18, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 18, 12, 0, 0)
        }, new()
        {
            Id = Guid.Parse("4fa7fb3d-1aa8-4e91-ba7a-6f4781c01d41"),
            Name = "Queen",
            ImageUrl = "https://queen.jpg",
            Bio = "British rock band famous for their style diversity, elaborate live performances, and classic hits like \"Bohemian Rhapsody\".",
            SimilarArtists = [new() { ArtistId = Guid.Parse("4fa7fb3d-1aa8-4e91-ba7a-6f4781c01d41"), SimilarArtistId = Guid.Parse("4e75ecdd-aafe-4c35-836b-1b83fc7b8f88") },
                              new() { ArtistId = Guid.Parse("4fa7fb3d-1aa8-4e91-ba7a-6f4781c01d41"), SimilarArtistId = Guid.Parse("8c301aa9-6d56-4c06-b1f2-9b9956979345") }],
            CreatedAt = new DateTime(2024, 8, 19, 0, 0, 0),
            UpdatedAt = new DateTime(2024, 8, 19, 0, 0, 0),
            DeletedAt = new DateTime(2024, 8, 19, 12, 0, 0)
        }];

        public static readonly Artist _newArtist = new()
        {
            Name = "U2",
            ImageUrl = "https://u2.jpg",
            Bio = "Irish rock band internationally acclaimed for their rock anthems and humanitarian efforts."
        };

        public static readonly List<ArtistDto> _artistDtos = _artists.Select(ToDto).ToList();

        public static readonly ArtistDto _newArtistDto = ToDto(_newArtist);

        public static readonly List<Artist> _artistsPagination = _artists.Where(artist => artist.DeletedAt == null).Take(10).ToList();

        public static readonly List<ArtistDto> _artistDtosPagination = _artistsPagination.Select(ToDto).ToList();

        public static readonly ArtistPaginationRequest _artistPayload = new(SortCriteria: null, SearchCriteria: null, Name: null);

        public static ArtistDto ToDto(Artist artist) => new()
        {
            Id = artist.Id,
            Name = artist.Name,
            ImageUrl = artist.ImageUrl,
            Bio = artist.Bio,
            SimilarArtistsIds = artist.SimilarArtists.Select(artistPair => artistPair.SimilarArtistId).ToList(),
            CreatedAt = artist.CreatedAt,
            UpdatedAt = artist.UpdatedAt,
            DeletedAt = artist.DeletedAt
        };
    }
}