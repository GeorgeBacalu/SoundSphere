using AutoMapper;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Auth;
using SoundSphere.Database.Entities;

namespace SoundSphere.Core.Mappings
{
    public static class UserMappingExtensions
    {
        public static List<UserDto> ToDtos(this List<User> users, IMapper mapper) => users.Select(user => user.ToDto(mapper)).ToList();

        public static List<User> ToEntities(this List<UserDto> userDtos, IMapper mapper) => userDtos.Select(userDto => userDto.ToEntity(mapper)).ToList();

        public static UserDto ToDto(this User user, IMapper mapper) => mapper.Map<UserDto>(user);

        public static User ToEntity(this UserDto userDto, IMapper mapper) => mapper.Map<User>(userDto);

        public static User ToEntity(this RegisterRequest payload) => new()
        {
            Name = payload.Name,
            Email = payload.Email,
            Phone = payload.Phone,
            Address = payload.Address,
            Birthday = payload.Birthday,
            ImageUrl = payload.ImageUrl,
            Role = payload.Role
        };
    }
}