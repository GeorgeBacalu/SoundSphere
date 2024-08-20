using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface IPlaylistService
    {
        Task<List<PlaylistDto>> GetAllAsync(PlaylistPaginationRequest payload);

        Task<PlaylistDto> GetByIdAsync(Guid id);

        Task<PlaylistDto> AddAsync(PlaylistDto playlistDto);

        Task<PlaylistDto> UpdateByIdAsync(PlaylistDto playlistDto, Guid id);

        Task<PlaylistDto> DeleteByIdAsync(Guid id);
    }
}