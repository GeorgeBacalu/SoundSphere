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

        public List<Artist> GetAll(ArtistPaginationRequest payload) => _context.Artists
            .Include(artist => artist.SimilarArtists)
            .Where(artist => artist.DeletedAt == null)
            .ApplyPagination(payload)
            .ToList();

        public Artist GetById(Guid id) => _context.Artists
            .Include(artist => artist.SimilarArtists)
            .Where(artist => artist.DeletedAt == null)
            .SingleOrDefault(artist => artist.Id == id)
            ?? throw new ResourceNotFoundException(string.Format(ArtistNotFound, id));

        public Artist Add(Artist artist)
        {
            _context.Artists.Add(artist);
            _context.SaveChanges();
            return artist;
        }

        public Artist UpdateById(Artist artist, Guid id)
        {
            Artist artistToUpdate = GetById(id);
            artistToUpdate.Name = artist.Name;
            artistToUpdate.ImageUrl = artist.ImageUrl;
            artistToUpdate.Bio = artist.Bio;
            artistToUpdate.SimilarArtists = artist.SimilarArtists;
            if (_context.Entry(artistToUpdate).State == EntityState.Modified)
                artistToUpdate.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return artistToUpdate;
        }

        public Artist DeleteById(Guid id)
        {
            Artist artistToDelete = GetById(id);
            artistToDelete.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
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