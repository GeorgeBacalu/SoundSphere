using SoundSphere.Database.Entities;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface ISongService
    {
        List<Song> GetAll();

        Song GetById(Guid id);

        Song Add(Song song);

        Song UpdateById(Song song, Guid id);

        Song DeleteById(Guid id);
    }
}