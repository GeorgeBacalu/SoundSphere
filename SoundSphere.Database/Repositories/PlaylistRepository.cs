﻿using Microsoft.EntityFrameworkCore;
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

        public List<Playlist> GetAll(PlaylistPaginationRequest payload) => _context.Playlists
            .Include(playlist => playlist.User)
            .Include(playlist => playlist.Songs)
            .Where(playlist => playlist.DeletedAt == null)
            .ApplyPagination(payload)
            .ToList();

        public Playlist GetById(Guid id) => _context.Playlists
            .Include(playlist => playlist.User)
            .Include(playlist => playlist.Songs)
            .Where(playlist => playlist.DeletedAt == null)
            .SingleOrDefault(playlist => playlist.Id == id)
            ?? throw new ResourceNotFoundException(string.Format(PlaylistNotFound, id));

        public Playlist Add(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            _context.SaveChanges();
            return playlist;
        }

        public Playlist UpdateById(Playlist playlist, Guid id)
        {
            Playlist playlistToUpdate = GetById(id);
            playlistToUpdate.Title = playlist.Title;
            if (_context.Entry(playlistToUpdate).State == EntityState.Modified)
                playlistToUpdate.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return playlistToUpdate;
        }

        public Playlist DeleteById(Guid id)
        {
            Playlist playlistToDelete = GetById(id);
            playlistToDelete.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return playlistToDelete;
        }

        public void LinkPlaylistToUser(Playlist playlist)
        {
            if (_context.Users.Find(playlist.User.Id) is User existingUser)
                playlist.User = _context.Attach(existingUser).Entity;
        }
    }
}