using SoundSphere.Database.Entities;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface IAlbumService
    {
        List<Album> GetAll();

        Album GetById(Guid id);

        Album Add(Album album);

        Album UpdateById(Album album, Guid id);

        Album DeleteById(Guid id);
    }
}