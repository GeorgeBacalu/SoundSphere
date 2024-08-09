using SoundSphere.Database.Dtos.Common;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface IAlbumService
    {
        List<AlbumDto> GetAll();

        AlbumDto GetById(Guid id);

        AlbumDto Add(AlbumDto albumDtoDto);

        AlbumDto UpdateById(AlbumDto albumDto, Guid id);

        AlbumDto DeleteById(Guid id);
    }
}