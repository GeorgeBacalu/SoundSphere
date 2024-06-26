﻿using System.Text.Json.Serialization;

namespace SoundSphere.Database.Entities
{
    public class Song : BaseEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public GenreType Genre { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public int DurationSeconds { get; set; } = 0;

        public Album Album { get; set; } = null!;

        public IList<Artist> Artists { get; set; } = null!;

        [JsonIgnore] public IList<Playlist>? Playlists { get; set; }

        public IList<SongLink> SimilarSongs { get; set; } = null!;

        public override bool Equals(object? obj) => obj is Song song &&
            Id.Equals(song.Id) &&
            Title.Equals(song.Title) &&
            ImageUrl.Equals(song.ImageUrl) &&
            Genre == song.Genre &&
            ReleaseDate.Equals(song.ReleaseDate) &&
            DurationSeconds == song.DurationSeconds &&
            Album.Equals(song.Album) &&
            Artists.SequenceEqual(song.Artists) &&
            SimilarSongs.SequenceEqual(song.SimilarSongs) &&
            CreatedAt.Equals(song.CreatedAt) &&
            UpdatedAt.Equals(song.UpdatedAt) &&
            DeletedAt.Equals(song.DeletedAt);

        public override int GetHashCode() => HashCode.Combine(Id, Title, ImageUrl, Genre, ReleaseDate, DurationSeconds, Album, HashCode.Combine(Artists, Playlists, SimilarSongs, CreatedAt, UpdatedAt, DeletedAt));
    }

    public enum GenreType { Pop, Rock, Rnb, HipHop, Dance, Techno, Latino, Hindi, Reggae, Jazz, Classical, Country, Electronic }
}