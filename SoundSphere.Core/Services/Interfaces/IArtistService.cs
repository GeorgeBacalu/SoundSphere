using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface IArtistService
    {
        Task<List<ArtistDto>> GetAllAsync(ArtistPaginationRequest payload);

        Task<ArtistDto> GetByIdAsync(Guid id);

        Task<ArtistDto> AddAsync(ArtistDto artistDto);

        Task<ArtistDto> UpdateByIdAsync(ArtistDto artistDto, Guid id);

        Task<ArtistDto> DeleteByIdAsync(Guid id);
    }
}