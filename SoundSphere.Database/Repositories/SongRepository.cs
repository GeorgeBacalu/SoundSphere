using Microsoft.EntityFrameworkCore;
using SoundSphere.Database.Context;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Extensions;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;

namespace SoundSphere.Database.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly AppDbContext _context;

        public SongRepository(AppDbContext context) => _context = context;

        public async Task<List<Song>> GetAllAsync(SongPaginationRequest payload) => await _context.Songs
            .Include(song => song.Album)
            .Include(song => song.Artists)
            .Include(song => song.SimilarSongs)
            .Where(song => song.DeletedAt == null)
            .ApplyPagination(payload)
            .ToListAsync();

        public async Task<Song> GetByIdAsync(Guid id) => await _context.Songs
            .Include(song => song.Album)
            .Include(song => song.Artists)
            .Include(song => song.SimilarSongs)
            .Where(song => song.DeletedAt == null)
            .SingleOrDefaultAsync(song => song.Id == id)
            ?? throw new ResourceNotFoundException(string.Format(SongNotFound, id));

        public async Task<Song> AddAsync(Song song)
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            return song;
        }

        public async Task<Song> UpdateByIdAsync(Song song, Guid id)
        {
            Song songToUpdate = await GetByIdAsync(id);
            songToUpdate.Title = song.Title;
            songToUpdate.ImageUrl = song.ImageUrl;
            songToUpdate.Genre = song.Genre;
            songToUpdate.ReleaseDate = song.ReleaseDate;
            songToUpdate.DurationSeconds = song.DurationSeconds;
            songToUpdate.Album = song.Album;
            songToUpdate.Artists = song.Artists;
            songToUpdate.SimilarSongs = song.SimilarSongs;
            if (_context.Entry(songToUpdate).State == EntityState.Modified)
                songToUpdate.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return songToUpdate;
        }

        public async Task<Song> DeleteByIdAsync(Guid id)
        {
            Song songToDelete = await GetByIdAsync(id);
            songToDelete.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return songToDelete;
        }

        public void LinkSongToAlbum(Song song)
        {
            if (_context.Albums.Find(song.Album.Id) is Album existingAlbum)
                song.Album = _context.Attach(existingAlbum).Entity;
        }

        public void LinkSongToArtists(Song song) => song.Artists = song.Artists
            .Select(artist => _context.Artists.Find(artist.Id))
            .Where(artist => artist != null)
            .Select(artist => _context.Attach(artist!).Entity)
            .ToList();

        public void AddSongPair(Song song) => song.SimilarSongs = song.SimilarSongs
            .Select(songPair => _context.Songs.Find(songPair.SimilarSongId))
            .Where(songPair => songPair != null)
            .Select(similarSong => new SongPair { Song = song, SimilarSong = similarSong })
            .ToList();

        public void AddUserSong(Song song) => _context.UsersSongs.AddRange(_context.Users
            .Select(user => new UserSong { User = user, Song = song })
            .ToList());
    }
}