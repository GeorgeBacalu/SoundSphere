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

        public async Task<List<User>> GetAllAsync(UserPaginationRequest payload) => await _context.Users
            .Where(user => user.DeletedAt == null)
            .ApplyPagination(payload)
            .ToListAsync();

        public async Task<User> GetByIdAsync(Guid id) => await _context.Users
            .Where(user => user.DeletedAt == null)
            .SingleOrDefaultAsync(user => user.Id == id)
            ?? throw new ResourceNotFoundException(string.Format(UserNotFound, id));

        public async Task<User> AddAsync(User user)
        {
            user.Password = "password";
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateByIdAsync(User user, Guid id)
        {
            User userToUpdate = await GetByIdAsync(id);
            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            userToUpdate.Phone = user.Phone;
            userToUpdate.Address= user.Address;
            userToUpdate.Birthday = user.Birthday;
            userToUpdate.ImageUrl = user.ImageUrl;
            userToUpdate.Role = user.Role;
            if (_context.Entry(userToUpdate).State == EntityState.Modified)
                userToUpdate.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return userToUpdate;
        }

        public async Task<User> DeleteByIdAsync(Guid id)
        {
            User userToDelete = await GetByIdAsync(id);
            userToDelete.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
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