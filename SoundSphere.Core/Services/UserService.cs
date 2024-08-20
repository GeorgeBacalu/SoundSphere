using AutoMapper;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;

namespace SoundSphere.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper) => (_userRepository, _mapper) = (userRepository, mapper);

        public async Task<List<UserDto>> GetAllAsync(UserPaginationRequest payload) => (await _userRepository.GetAllAsync(payload)).ToDtos(_mapper);

        public async Task<UserDto> GetByIdAsync(Guid id) => (await _userRepository.GetByIdAsync(id)).ToDto(_mapper);

        public async Task<UserDto> AddAsync(UserDto userDto)
        {
            User user = userDto.ToEntity(_mapper);
            _userRepository.AddUserArtist(user);
            _userRepository.AddUserSong(user);
            return (await _userRepository.AddAsync(user)).ToDto(_mapper);
        }

        public async Task<UserDto> UpdateByIdAsync(UserDto userDto, Guid id) => (await _userRepository.UpdateByIdAsync(userDto.ToEntity(_mapper), id)).ToDto(_mapper);

        public async Task<UserDto> DeleteByIdAsync(Guid id) => (await _userRepository.DeleteByIdAsync(id)).ToDto(_mapper);
    }
}