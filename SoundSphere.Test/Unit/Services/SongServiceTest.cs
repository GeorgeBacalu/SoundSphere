using AutoMapper;
using FluentAssertions;
using Moq;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.AlbumMock;
using static SoundSphere.Test.Mocks.ArtistMock;
using static SoundSphere.Test.Mocks.SongMock;

namespace SoundSphere.Test.Unit.Services
{
    public class SongServiceTest
    {
        private readonly Mock<ISongRepository> _songRepositoryMock = new();
        private readonly Mock<IAlbumRepository> _albumRepositoryMock = new();
        private readonly Mock<IArtistRepository> _artistRepositoryMock = new();
        private readonly ISongService _songService;
        private readonly IMapper _mapper;

        public SongServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _songService = new SongService(_songRepositoryMock.Object, _albumRepositoryMock.Object, _artistRepositoryMock.Object, _mapper);
        }

        [Fact] public async Task GetAll_Test()
        {
            _songRepositoryMock.Setup(mock => mock.GetAllAsync(_songPayload)).ReturnsAsync(_songsPagination);
            (await _songService.GetAllAsync(_songPayload)).Should().BeEquivalentTo(_songDtosPagination);
        }

        [Fact] public async Task GetById_ValidId_Test()
        {
            _songRepositoryMock.Setup(mock => mock.GetByIdAsync(ValidSongId)).ReturnsAsync(_songs[0]);
            (await _songService.GetByIdAsync(ValidSongId)).Should().BeEquivalentTo(_songDtos[0]);
        }

        [Fact] public async Task GetById_InvalidId_Test()
        {
            _songRepositoryMock.Setup(mock => mock.GetByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(SongNotFound, InvalidId)));
            await _songService.Invoking(service => service.GetByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(SongNotFound, InvalidId));
            _songRepositoryMock.Verify(mock => mock.GetByIdAsync(InvalidId));
        }

        [Fact] public async Task Add_Test()
        {
            _artists.Take(1).ToList().ForEach(artist => _artistRepositoryMock.Setup(mock => mock.GetByIdAsync(artist.Id)).ReturnsAsync(artist));
            _albumRepositoryMock.Setup(mock => mock.GetByIdAsync(ValidAlbumId)).ReturnsAsync(_albums[0]);
            _songRepositoryMock.Setup(mock => mock.AddAsync(It.IsAny<Song>())).ReturnsAsync(_newSong);
            (await _songService.AddAsync(_newSongDto)).Should().BeEquivalentTo(_newSongDto, options => options.Excluding(song => song.Id).Excluding(song => song.CreatedAt).Excluding(song => song.UpdatedAt));
            _songRepositoryMock.Verify(mock => mock.AddAsync(It.IsAny<Song>()));
        }

        [Fact] public async Task UpdateById_ValidId_Test()
        {
            Song updatedSong = _songs[0];
            updatedSong.Title = _songs[1].Title;
            updatedSong.ImageUrl = _songs[1].ImageUrl;
            updatedSong.Genre = _songs[1].Genre;
            updatedSong.ReleaseDate = _songs[1].ReleaseDate;
            updatedSong.DurationSeconds = _songs[1].DurationSeconds;
            updatedSong.Album = _songs[1].Album;
            updatedSong.Artists = _songs[1].Artists;
            updatedSong.SimilarSongs = _songs[1].SimilarSongs;
            SongDto updatedSongDto = updatedSong.ToDto(_mapper);
            _songRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<Song>(), ValidSongId)).ReturnsAsync(updatedSong);
            (await _songService.UpdateByIdAsync(_songDtos[1], ValidSongId)).Should().BeEquivalentTo(updatedSongDto);
            _songRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<Song>(), ValidSongId));
        }

        [Fact] public async Task UpdateById_InvalidId_Test()
        {
            _songRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<Song>(), InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(SongNotFound, InvalidId)));
            await _songService.Invoking(service => service.UpdateByIdAsync(_songDtos[1], InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(SongNotFound, InvalidId));
            _songRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<Song>(), InvalidId));
        }

        [Fact] public async Task DeleteById_ValidId_Test()
        {
            Song deletedSong = _songs[0];
            deletedSong.DeletedAt = DateTime.UtcNow;
            SongDto deletedSongDto = deletedSong.ToDto(_mapper);
            _songRepositoryMock.Setup(mock => mock.DeleteByIdAsync(ValidSongId)).ReturnsAsync(deletedSong);
            (await _songService.DeleteByIdAsync(ValidSongId)).Should().BeEquivalentTo(deletedSongDto);
            _songRepositoryMock.Verify(mock => mock.DeleteByIdAsync(ValidSongId));
        }

        [Fact] public async Task DeleteById_InvalidId_Test()
        {
            _songRepositoryMock.Setup(mock => mock.DeleteByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(SongNotFound, InvalidId)));
            await _songService.Invoking(service => service.DeleteByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(SongNotFound, InvalidId));
            _songRepositoryMock.Verify(mock => mock.DeleteByIdAsync(InvalidId));
        }
    }
}