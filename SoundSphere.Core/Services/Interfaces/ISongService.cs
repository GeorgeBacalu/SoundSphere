using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface ISongService
    {
        Task<List<SongDto>> GetAllAsync(SongPaginationRequest payload);

        Task<SongDto> GetByIdAsync(Guid id);

        Task<SongDto> AddAsync(SongDto songDto);

        Task<SongDto> UpdateByIdAsync(SongDto songDto, Guid id);

        Task<SongDto> DeleteByIdAsync(Guid id);
    }
}