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
using static SoundSphere.Test.Mocks.PlaylistMock;
using static SoundSphere.Test.Mocks.UserMock;
using static SoundSphere.Test.Mocks.SongMock;

namespace SoundSphere.Test.Unit.Services
{
    public class PlaylistServiceTest
    {
        private readonly Mock<IPlaylistRepository> _playlistRepositoryMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<ISongRepository> _songRepositoryMock = new();
        private readonly IPlaylistService _playlistService;
        private readonly IMapper _mapper;

        public PlaylistServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _playlistService = new PlaylistService(_playlistRepositoryMock.Object, _userRepositoryMock.Object, _songRepositoryMock.Object, _mapper);
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedPlaylists()
        {
            _playlistRepositoryMock.Setup(mock => mock.GetAllAsync(_playlistPayload)).ReturnsAsync(_playlistsPagination);
            (await _playlistService.GetAllAsync(_playlistPayload)).Should().BeEquivalentTo(_playlistDtosPagination);
        }

        [Fact] public async Task GetByIdAsync_ShouldReturnPlaylist_WhenPlaylistIdIsValid()
        {
            _playlistRepositoryMock.Setup(mock => mock.GetByIdAsync(ValidPlaylistId)).ReturnsAsync(_playlists[0]);
            (await _playlistService.GetByIdAsync(ValidPlaylistId)).Should().BeEquivalentTo(_playlistDtos[0]);
        }

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenPlaylistIdIsInvalid()
        {
            _playlistRepositoryMock.Setup(mock => mock.GetByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(PlaylistNotFound, InvalidId)));
            await _playlistService.Invoking(service => service.GetByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(PlaylistNotFound, InvalidId));
            _playlistRepositoryMock.Verify(mock => mock.GetByIdAsync(InvalidId));
        }

        [Fact] public async Task AddAsync_ShouldAddNewPlaylist_WhenPlaylistDtoIsValid()
        {
            _songs.Take(2).ToList().ForEach(song => _songRepositoryMock.Setup(mock => mock.GetByIdAsync(song.Id)).ReturnsAsync(song));
            _userRepositoryMock.Setup(mock => mock.GetByIdAsync(ValidUserId)).ReturnsAsync(_users[0]);
            _playlistRepositoryMock.Setup(mock => mock.AddAsync(It.IsAny<Playlist>())).ReturnsAsync(_newPlaylist);
            (await _playlistService.AddAsync(_newPlaylistDto)).Should().BeEquivalentTo(_newPlaylistDto, options => options.Excluding(playlist => playlist.Id).Excluding(playlist => playlist.CreatedAt).Excluding(playlist => playlist.UpdatedAt));
            _playlistRepositoryMock.Verify(mock => mock.AddAsync(It.IsAny<Playlist>()));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldUpdatePlaylist_WhenPlaylistIdIsValid()
        {
            Playlist updatedPlaylist = _playlists[0];
            updatedPlaylist.Title = _playlists[1].Title;
            PlaylistDto updatedPlaylistDto = updatedPlaylist.ToDto(_mapper);
            _playlistRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<Playlist>(), ValidPlaylistId)).ReturnsAsync(updatedPlaylist);
            (await _playlistService.UpdateByIdAsync(_playlistDtos[1], ValidPlaylistId)).Should().BeEquivalentTo(updatedPlaylistDto);
            _playlistRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<Playlist>(), ValidPlaylistId));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenPlaylistIdIsInvalid()
        {
            _playlistRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<Playlist>(), InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(PlaylistNotFound, InvalidId)));
            await _playlistService.Invoking(service => service.UpdateByIdAsync(_playlistDtos[1], InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(PlaylistNotFound, InvalidId));
            _playlistRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<Playlist>(), InvalidId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldDeletePlaylist_WhenPlaylistIdIsValid()
        {
            Playlist deletedPlaylist = _playlists[0];
            deletedPlaylist.DeletedAt = DateTime.UtcNow;
            PlaylistDto deletedPlaylistDto = deletedPlaylist.ToDto(_mapper);
            _playlistRepositoryMock.Setup(mock => mock.DeleteByIdAsync(ValidPlaylistId)).ReturnsAsync(deletedPlaylist);
            (await _playlistService.DeleteByIdAsync(ValidPlaylistId)).Should().BeEquivalentTo(deletedPlaylistDto);
            _playlistRepositoryMock.Verify(mock => mock.DeleteByIdAsync(ValidPlaylistId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenPlaylistIdIsInvalid()
        {
            _playlistRepositoryMock.Setup(mock => mock.DeleteByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(PlaylistNotFound, InvalidId)));
            await _playlistService.Invoking(service => service.DeleteByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(PlaylistNotFound, InvalidId));
            _playlistRepositoryMock.Verify(mock => mock.DeleteByIdAsync(InvalidId));
        }
    }
}