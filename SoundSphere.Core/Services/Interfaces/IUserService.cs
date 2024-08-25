using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Auth;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
namespace SoundSphere.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync(UserPaginationRequest payload);

        Task<UserDto> GetByIdAsync(Guid id);

        Task<UserDto> UpdateByIdAsync(UserDto userDto, Guid id);

        Task<UserDto> DeleteByIdAsync(Guid id);

        Task<UserDto> RegisterAsync(RegisterRequest payload);

        Task<string> LoginAsync(LoginRequest payload);

        string GenerateToken(User user);
    }
}