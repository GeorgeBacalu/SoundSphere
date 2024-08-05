using AutoMapper;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;

namespace SoundSphere.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper) => (_userRepository, _mapper) = (userRepository, mapper);

        public List<UserDto> GetAll() => _userRepository.GetAll().ToDtos(_mapper);

        public UserDto GetById(Guid id) => _userRepository.GetById(id).ToDto(_mapper);

        public UserDto Add(UserDto userDto)
        {
            User user = userDto.ToEntity(_mapper);
            _userRepository.AddUserArtist(user);
            _userRepository.AddUserSong(user);
            return _userRepository.Add(user).ToDto(_mapper);
        }

        public UserDto UpdateById(UserDto userDto, Guid id) => _userRepository.UpdateById(userDto.ToEntity(_mapper), id).ToDto(_mapper);

        public UserDto DeleteById(Guid id) => _userRepository.DeleteById(id).ToDto(_mapper);
    }
}