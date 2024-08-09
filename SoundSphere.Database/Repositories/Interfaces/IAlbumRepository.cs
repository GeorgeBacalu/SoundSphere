using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Repositories.Interfaces
{
    public interface IAlbumRepository
    {
        List<Album> GetAll();

        Album GetById(Guid id);

        Album Add(Album album);

        Album UpdateById(Album album, Guid id);

        Album DeleteById(Guid id);

        void AddAlbumPair(Album album);
    }
}