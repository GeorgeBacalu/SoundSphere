using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();

        User GetById(Guid id);

        User Add(User user);

        User UpdateById(User user, Guid id);

        User DeleteById(Guid id);

        void AddUserArtist(User user);

        void AddUserSong(User user);
    }
}