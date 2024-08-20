using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface IAlbumService
    {
        Task<List<AlbumDto>> GetAllAsync(AlbumPaginationRequest payload);

        Task<AlbumDto> GetByIdAsync(Guid id);

        Task<AlbumDto> AddAsync(AlbumDto albumDtoDto);

        Task<AlbumDto> UpdateByIdAsync(AlbumDto albumDto, Guid id);

        Task<AlbumDto> DeleteByIdAsync(Guid id);
    }
}