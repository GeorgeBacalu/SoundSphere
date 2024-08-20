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
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly AppDbContext _context;

        public PlaylistRepository(AppDbContext context) => _context = context;

        public async Task<List<Playlist>> GetAllAsync(PlaylistPaginationRequest payload) => await _context.Playlists
            .Include(playlist => playlist.User)
            .Include(playlist => playlist.Songs)
            .Where(playlist => playlist.DeletedAt == null)
            .ApplyPagination(payload)
            .ToListAsync();

        public async Task<Playlist> GetByIdAsync(Guid id) => await _context.Playlists
            .Include(playlist => playlist.User)
            .Include(playlist => playlist.Songs)
            .Where(playlist => playlist.DeletedAt == null)
            .SingleOrDefaultAsync(playlist => playlist.Id == id)
            ?? throw new ResourceNotFoundException(string.Format(PlaylistNotFound, id));

        public async Task<Playlist> AddAsync(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();
            return playlist;
        }

        public async Task<Playlist> UpdateByIdAsync(Playlist playlist, Guid id)
        {
            Playlist playlistToUpdate = await GetByIdAsync(id);
            playlistToUpdate.Title = playlist.Title;
            if (_context.Entry(playlistToUpdate).State == EntityState.Modified)
                playlistToUpdate.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return playlistToUpdate;
        }

        public async Task<Playlist> DeleteByIdAsync(Guid id)
        {
            Playlist playlistToDelete = await GetByIdAsync(id);
            playlistToDelete.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return playlistToDelete;
        }

        public void LinkPlaylistToUser(Playlist playlist)
        {
            if (_context.Users.Find(playlist.User.Id) is User existingUser)
                playlist.User = _context.Attach(existingUser).Entity;
        }
    }
}