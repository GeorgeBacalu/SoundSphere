using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<AlbumPair> AlbumPairs { get; set; }
        public DbSet<ArtistPair> ArtistPairs { get; set; }
        public DbSet<SongPair> SongPairs { get; set; }
        public DbSet<UserArtist> UsersArtists { get; set; }
        public DbSet<UserSong> UsersSongs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(user => user.Name).IsUnique();
                entity.HasIndex(user => user.Email).IsUnique();
                entity.HasIndex(user => user.Phone).IsUnique();
                entity.Property(user => user.Role).HasConversion(new EnumToStringConverter<Role>());
            });
            modelBuilder.Entity<Feedback>(entity => entity.Property(feedback => feedback.Type).HasConversion(new EnumToStringConverter<FeedbackType>()));
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(notification => notification.Type).HasConversion(new EnumToStringConverter<NotificationType>());
                entity.HasOne(notification => notification.Sender).WithMany().HasForeignKey(notification => notification.SenderId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(notification => notification.Receiver).WithMany().HasForeignKey(notification => notification.ReceiverId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Song>(entity => entity.Property(song => song.Genre).HasConversion(new EnumToStringConverter<Genre>()));
            modelBuilder.Entity<AlbumPair>(entity =>
            {
                entity.HasKey(albumPair => new { albumPair.AlbumId, albumPair.SimilarAlbumId });
                entity.HasOne(albumPair => albumPair.Album).WithMany(albumPair => albumPair.SimilarAlbums).HasForeignKey(albumPair => albumPair.AlbumId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(albumPair => albumPair.SimilarAlbum).WithMany().HasForeignKey(albumPair => albumPair.SimilarAlbumId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<ArtistPair>(entity =>
            {
                entity.HasKey(artistPair => new { artistPair.ArtistId, artistPair.SimilarArtistId });
                entity.HasOne(artistPair => artistPair.Artist).WithMany(artistPair => artistPair.SimilarArtists).HasForeignKey(artistPair => artistPair.ArtistId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(artistPair => artistPair.SimilarArtist).WithMany().HasForeignKey(artistPair => artistPair.SimilarArtistId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<SongPair>(entity =>
            {
                entity.HasKey(songPair => new { songPair.SongId, songPair.SimilarSongId });
                entity.HasOne(songPair => songPair.Song).WithMany(songPair => songPair.SimilarSongs).HasForeignKey(songPair => songPair.SongId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(songPair => songPair.SimilarSong).WithMany().HasForeignKey(songPair => songPair.SimilarSongId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<UserArtist>(entity =>
            {
                entity.HasKey(userArtist => new { userArtist.UserId, userArtist.ArtistId });
                entity.HasOne(userArtist => userArtist.User).WithMany(user => user.UserArtists).HasForeignKey(userArtist => userArtist.UserId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(userArtist => userArtist.Artist).WithMany().HasForeignKey(userArtist => userArtist.ArtistId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<UserSong>(entity =>
            {
                entity.HasKey(userSong => new { userSong.UserId, userSong.SongId });
                entity.HasOne(userSong => userSong.User).WithMany(user => user.UserSongs).HasForeignKey(userSong => userSong.UserId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(userSong => userSong.Song).WithMany().HasForeignKey(userSong => userSong.SongId).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}