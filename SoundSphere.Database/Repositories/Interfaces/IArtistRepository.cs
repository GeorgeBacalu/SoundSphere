using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        List<Artist> GetAll(ArtistPaginationRequest payload);

        Artist GetById(Guid id);

        Artist Add(Artist artist);

        Artist UpdateById(Artist artist, Guid id);

        Artist DeleteById(Guid id);

        void AddArtistPair(Artist artist);

        void AddUserArtist(Artist artist);
    }
}