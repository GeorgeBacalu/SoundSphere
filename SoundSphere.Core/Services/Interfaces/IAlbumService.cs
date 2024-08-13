using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface IAlbumService
    {
        List<AlbumDto> GetAll(AlbumPaginationRequest payload);

        AlbumDto GetById(Guid id);

        AlbumDto Add(AlbumDto albumDtoDto);

        AlbumDto UpdateById(AlbumDto albumDto, Guid id);

        AlbumDto DeleteById(Guid id);
    }
}