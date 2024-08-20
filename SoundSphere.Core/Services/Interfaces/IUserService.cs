using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync(UserPaginationRequest payload);

        Task<UserDto> GetByIdAsync(Guid id);

        Task<UserDto> AddAsync(UserDto userDto);

        Task<UserDto> UpdateByIdAsync(UserDto userDto, Guid id);

        Task<UserDto> DeleteByIdAsync(Guid id);
    }
}