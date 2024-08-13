using Microsoft.EntityFrameworkCore;
using SoundSphere.Database.Context;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Extensions;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;

namespace SoundSphere.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) => _context = context;

        public List<User> GetAll(UserPaginationRequest payload) => _context.Users
            .Where(user => user.DeletedAt == null)
            .ApplyPagination(payload)
            .ToList();

        public User GetById(Guid id) => _context.Users
            .Where(user => user.DeletedAt == null)
            .SingleOrDefault(user => user.Id == id)
            ?? throw new ResourceNotFoundException(string.Format(UserNotFound, id));

        public User Add(User user)
        {
            user.Password = "password";
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User UpdateById(User user, Guid id)
        {
            User userToUpdate = GetById(id);
            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            userToUpdate.Phone = user.Phone;
            userToUpdate.Address= user.Address;
            userToUpdate.Birthday = user.Birthday;
            userToUpdate.ImageUrl = user.ImageUrl;
            userToUpdate.Role = user.Role;
            if (_context.Entry(userToUpdate).State == EntityState.Modified)
                userToUpdate.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return userToUpdate;
        }

        public User DeleteById(Guid id)
        {
            User userToDelete = GetById(id);
            userToDelete.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return userToDelete;
        }

        public void AddUserArtist(User user) => _context.UsersArtists.AddRange(_context.Artists
            .Select(artist => new UserArtist { User = user, Artist = artist })
            .ToList());

        public void AddUserSong(User user) => _context.UsersSongs.AddRange(_context.Songs
            .Select(song => new UserSong { User = user, Song = song })
            .ToList());
    }
}