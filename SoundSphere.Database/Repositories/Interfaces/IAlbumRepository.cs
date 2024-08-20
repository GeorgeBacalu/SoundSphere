using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Repositories.Interfaces
{
    public interface IAlbumRepository
    {
        Task<List<Album>> GetAllAsync(AlbumPaginationRequest payload);

        Task<Album> GetByIdAsync(Guid id);

        Task<Album> AddAsync(Album album);

        Task<Album> UpdateByIdAsync(Album album, Guid id);

        Task<Album> DeleteByIdAsync(Guid id);

        void AddAlbumPair(Album album);
    }
}