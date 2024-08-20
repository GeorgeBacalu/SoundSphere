using AutoMapper;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;

namespace SoundSphere.Core.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public SongService(ISongRepository songRepository, IAlbumRepository albumRepository, IArtistRepository artistRepository, IMapper mapper) =>
            (_songRepository, _albumRepository, _artistRepository, _mapper) = (songRepository, albumRepository, artistRepository, mapper);

        public async Task<List<SongDto>> GetAllAsync(SongPaginationRequest payload) => (await _songRepository.GetAllAsync(payload)).ToDtos(_mapper);

        public async Task<SongDto> GetByIdAsync(Guid id) => (await _songRepository.GetByIdAsync(id)).ToDto(_mapper);

        public async Task<SongDto> AddAsync(SongDto songDto)
        {
            Song song = await songDto.ToEntityAsync(_albumRepository, _artistRepository, _mapper);
            _songRepository.LinkSongToAlbum(song);
            _songRepository.LinkSongToArtists(song);
            _songRepository.AddSongPair(song);
            _songRepository.AddUserSong(song);
            return (await _songRepository.AddAsync(song)).ToDto(_mapper);
        }

        public async Task<SongDto> UpdateByIdAsync(SongDto songDto, Guid id) => (await _songRepository.UpdateByIdAsync(await songDto.ToEntityAsync(_albumRepository, _artistRepository, _mapper), id)).ToDto(_mapper);

        public async Task<SongDto> DeleteByIdAsync(Guid id) => (await _songRepository.DeleteByIdAsync(id)).ToDto(_mapper);
    }
}