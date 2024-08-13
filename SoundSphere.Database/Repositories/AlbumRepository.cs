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

        public List<Album> GetAll(AlbumPaginationRequest payload) => _context.Albums
            .Include(album => album.SimilarAlbums)
            .Where(album => album.DeletedAt == null)
            .ApplyPagination(payload)
            .ToList();

        public Album GetById(Guid id) => _context.Albums
            .Include(album => album.SimilarAlbums)
            .Where(album => album.DeletedAt == null)
            .SingleOrDefault(album => album.Id == id)
            ?? throw new ResourceNotFoundException(string.Format(AlbumNotFound, id));

        public Album Add(Album album)
        {
            _context.Albums.Add(album);
            _context.SaveChanges();
            return album;
        }

        public Album UpdateById(Album album, Guid id)
        {
            Album albumToUpdate = GetById(id);
            albumToUpdate.Title = album.Title;
            albumToUpdate.ImageUrl = album.ImageUrl;
            albumToUpdate.ReleaseDate = album.ReleaseDate;
            albumToUpdate.SimilarAlbums = album.SimilarAlbums;
            if (_context.Entry(albumToUpdate).State == EntityState.Modified)
                albumToUpdate.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return albumToUpdate;
        }

        public Album DeleteById(Guid id)
        {
            Album albumToDelete = GetById(id);
            albumToDelete.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return albumToDelete;
        }

        public void AddAlbumPair(Album album) => album.SimilarAlbums = album.SimilarAlbums
            .Select(albumPair => _context.Albums.Find(albumPair.SimilarAlbumId))
            .Where(albumPair => albumPair != null)
            .Select(similarAlbum => new AlbumPair { Album = album, SimilarAlbum = similarAlbum })
            .ToList();
    }
}