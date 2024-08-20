using AutoMapper;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;

namespace SoundSphere.Core.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;

        public AlbumService(IAlbumRepository albumRepository, IMapper mapper) => (_albumRepository, _mapper) = (albumRepository, mapper);

        public async Task<List<AlbumDto>> GetAllAsync(AlbumPaginationRequest payload) => (await _albumRepository.GetAllAsync(payload)).ToDtos(_mapper);

        public async Task<AlbumDto> GetByIdAsync(Guid id) => (await _albumRepository.GetByIdAsync(id)).ToDto(_mapper);

        public async Task<AlbumDto> AddAsync(AlbumDto albumDto)
        {
            Album album = albumDto.ToEntity(_mapper);
            _albumRepository.AddAlbumPair(album);
            return (await _albumRepository.AddAsync(album)).ToDto(_mapper);
        }

        public async Task<AlbumDto> UpdateByIdAsync(AlbumDto albumDto, Guid id) => (await _albumRepository.UpdateByIdAsync(albumDto.ToEntity(_mapper), id)).ToDto(_mapper);

        public async Task<AlbumDto> DeleteByIdAsync(Guid id) => (await _albumRepository.DeleteByIdAsync(id)).ToDto(_mapper);
    }
}