using AutoMapper;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;

namespace SoundSphere.Core.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;

        public PlaylistService(IPlaylistRepository playlistRepository, IUserRepository userRepository, ISongRepository songRepository, IMapper mapper) =>
            (_playlistRepository, _userRepository, _songRepository, _mapper) = (playlistRepository, userRepository, songRepository, mapper);

        public async Task<List<PlaylistDto>> GetAllAsync(PlaylistPaginationRequest payload) => (await _playlistRepository.GetAllAsync(payload)).ToDtos(_mapper);

        public async Task<PlaylistDto> GetByIdAsync(Guid id) => (await _playlistRepository.GetByIdAsync(id)).ToDto(_mapper);

        public async Task<PlaylistDto> AddAsync(PlaylistDto playlistDto)
        {
            Playlist playlist = await playlistDto.ToEntityAsync(_userRepository, _songRepository, _mapper);
            _playlistRepository.LinkPlaylistToUser(playlist);
            return (await _playlistRepository.AddAsync(playlist)).ToDto(_mapper);
        }

        public async Task<PlaylistDto> UpdateByIdAsync(PlaylistDto playlistDto, Guid id) => (await _playlistRepository.UpdateByIdAsync(await playlistDto.ToEntityAsync(_userRepository, _songRepository, _mapper), id)).ToDto(_mapper);

        public async Task<PlaylistDto> DeleteByIdAsync(Guid id) => (await _playlistRepository.DeleteByIdAsync(id)).ToDto(_mapper);
    }
}