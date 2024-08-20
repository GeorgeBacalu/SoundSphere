using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Repositories.Interfaces
{
    public interface IPlaylistRepository
    {
        Task<List<Playlist>> GetAllAsync(PlaylistPaginationRequest payload);

        Task<Playlist> GetByIdAsync(Guid id);

        Task<Playlist> AddAsync(Playlist playlist);

        Task<Playlist> UpdateByIdAsync(Playlist playlist, Guid id);

        Task<Playlist> DeleteByIdAsync(Guid id);

        void LinkPlaylistToUser(Playlist playlist);
    }
}