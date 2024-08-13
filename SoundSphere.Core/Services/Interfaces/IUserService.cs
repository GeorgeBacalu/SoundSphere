using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAll(UserPaginationRequest payload);

        UserDto GetById(Guid id);

        UserDto Add(UserDto userDto);

        UserDto UpdateById(UserDto userDto, Guid id);

        UserDto DeleteById(Guid id);
    }
}