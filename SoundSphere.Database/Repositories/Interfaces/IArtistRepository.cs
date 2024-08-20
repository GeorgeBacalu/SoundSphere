using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetAllAsync(ArtistPaginationRequest payload);

        Task<Artist> GetByIdAsync(Guid id);

        Task<Artist> AddAsync(Artist artist);

        Task<Artist> UpdateByIdAsync(Artist artist, Guid id);

        Task<Artist> DeleteByIdAsync(Guid id);

        void AddArtistPair(Artist artist);

        void AddUserArtist(Artist artist);
    }
}