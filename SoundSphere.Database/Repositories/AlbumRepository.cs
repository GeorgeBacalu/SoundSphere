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
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AppDbContext _context;

        public AlbumRepository(AppDbContext context) => _context = context;

        public async Task<List<Album>> GetAllAsync(AlbumPaginationRequest payload) => await _context.Albums
            .Include(album => album.SimilarAlbums)
            .Where(album => album.DeletedAt == null)
            .ApplyPagination(payload)
            .ToListAsync();

        public async Task<Album> GetByIdAsync(Guid id) => await _context.Albums
            .Include(album => album.SimilarAlbums)
            .Where(album => album.DeletedAt == null)
            .SingleOrDefaultAsync(album => album.Id == id)
            ?? throw new ResourceNotFoundException(string.Format(AlbumNotFound, id));

        public async Task<Album> AddAsync(Album album)
        {
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();
            return album;
        }

        public async Task<Album> UpdateByIdAsync(Album album, Guid id)
        {
            Album albumToUpdate = await GetByIdAsync(id);
            albumToUpdate.Title = album.Title;
            albumToUpdate.ImageUrl = album.ImageUrl;
            albumToUpdate.ReleaseDate = album.ReleaseDate;
            albumToUpdate.SimilarAlbums = album.SimilarAlbums;
            if (_context.Entry(albumToUpdate).State == EntityState.Modified)
                albumToUpdate.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return albumToUpdate;
        }

        public async Task<Album> DeleteByIdAsync(Guid id)
        {
            Album albumToDelete = await GetByIdAsync(id);
            albumToDelete.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return albumToDelete;
        }

        public void AddAlbumPair(Album album) => album.SimilarAlbums = album.SimilarAlbums
            .Select(albumPair => _context.Albums.Find(albumPair.SimilarAlbumId))
            .Where(albumPair => albumPair != null)
            .Select(similarAlbum => new AlbumPair { Album = album, SimilarAlbum = similarAlbum })
            .ToList();
    }
}