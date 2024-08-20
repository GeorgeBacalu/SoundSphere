using AutoMapper;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;

namespace SoundSphere.Core.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public ArtistService(IArtistRepository artistRepository, IMapper mapper) => (_artistRepository, _mapper) = (artistRepository, mapper);

        public async Task<List<ArtistDto>> GetAllAsync(ArtistPaginationRequest payload) => (await _artistRepository.GetAllAsync(payload)).ToDtos(_mapper);

        public async Task<ArtistDto> GetByIdAsync(Guid id) => (await _artistRepository.GetByIdAsync(id)).ToDto(_mapper);

        public async Task<ArtistDto> AddAsync(ArtistDto artistDto)
        {
            Artist artist = artistDto.ToEntity(_mapper);
            _artistRepository.AddArtistPair(artist);
            _artistRepository.AddUserArtist(artist);
            return (await _artistRepository.AddAsync(artist)).ToDto(_mapper);
        }

        public async Task<ArtistDto> UpdateByIdAsync(ArtistDto artistDto, Guid id) => (await _artistRepository.UpdateByIdAsync(artistDto.ToEntity(_mapper), id)).ToDto(_mapper);

        public async Task<ArtistDto> DeleteByIdAsync(Guid id) => (await _artistRepository.DeleteByIdAsync(id)).ToDto(_mapper);
    }
}