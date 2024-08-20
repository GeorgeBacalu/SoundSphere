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
    public class ArtistRepository : IArtistRepository
    {
        private readonly AppDbContext _context;

        public ArtistRepository(AppDbContext context) => _context = context;

        public async Task<List<Artist>> GetAllAsync(ArtistPaginationRequest payload) => await _context.Artists
            .Include(artist => artist.SimilarArtists)
            .Where(artist => artist.DeletedAt == null)
            .ApplyPagination(payload)
            .ToListAsync();

        public async Task<Artist> GetByIdAsync(Guid id) => await _context.Artists
            .Include(artist => artist.SimilarArtists)
            .Where(artist => artist.DeletedAt == null)
            .SingleOrDefaultAsync(artist => artist.Id == id)
            ?? throw new ResourceNotFoundException(string.Format(ArtistNotFound, id));

        public async Task<Artist> AddAsync(Artist artist)
        {
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
            return artist;
        }

        public async Task<Artist> UpdateByIdAsync(Artist artist, Guid id)
        {
            Artist artistToUpdate = await GetByIdAsync(id);
            artistToUpdate.Name = artist.Name;
            artistToUpdate.ImageUrl = artist.ImageUrl;
            artistToUpdate.Bio = artist.Bio;
            artistToUpdate.SimilarArtists = artist.SimilarArtists;
            if (_context.Entry(artistToUpdate).State == EntityState.Modified)
                artistToUpdate.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return artistToUpdate;
        }

        public async Task<Artist> DeleteByIdAsync(Guid id)
        {
            Artist artistToDelete = await GetByIdAsync(id);
            artistToDelete.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return artistToDelete;
        }

        public void AddArtistPair(Artist artist) => artist.SimilarArtists = artist.SimilarArtists
            .Select(artistPair => _context.Artists.Find(artistPair.SimilarArtistId))
            .Where(artistPair => artistPair != null)
            .Select(similarArtist => new ArtistPair { Artist = artist, SimilarArtist = similarArtist })
            .ToList();

        public void AddUserArtist(Artist artist) => _context.UsersArtists.AddRange(_context.Users
            .Select(user => new UserArtist { User = user, Artist = artist })
            .ToList());
    }
}