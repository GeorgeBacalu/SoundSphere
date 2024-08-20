using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Repositories.Interfaces
{
    public interface ISongRepository
    {
        Task<List<Song>> GetAllAsync(SongPaginationRequest payload);

        Task<Song> GetByIdAsync(Guid id);

        Task<Song> AddAsync(Song song);

        Task<Song> UpdateByIdAsync(Song song, Guid id);

        Task<Song> DeleteByIdAsync(Guid id);

        void LinkSongToAlbum(Song song);

        void LinkSongToArtists(Song song);

        void AddSongPair(Song song);

        void AddUserSong(Song song);
    }
}