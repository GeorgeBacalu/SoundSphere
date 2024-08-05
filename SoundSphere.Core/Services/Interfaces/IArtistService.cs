using SoundSphere.Database.Entities;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface IArtistService
    {
        List<Artist> GetAll();

        Artist GetById(Guid id);

        Artist Add(Artist artist);

        Artist UpdateById(Artist artist, Guid id);

        Artist DeleteById(Guid id);
    }
}