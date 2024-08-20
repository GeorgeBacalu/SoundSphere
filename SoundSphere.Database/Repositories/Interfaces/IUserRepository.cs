using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(UserPaginationRequest payload);

        Task<User> GetByIdAsync(Guid id);

        Task<User> AddAsync(User user);

        Task<User> UpdateByIdAsync(User user, Guid id);

        Task<User> DeleteByIdAsync(Guid id);

        void AddUserArtist(User user);

        void AddUserSong(User user);
    }
}