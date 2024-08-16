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

        [Fact] public void GetAll_Test()
        {
            _playlistRepositoryMock.Setup(mock => mock.GetAll(_playlistPayload)).Returns(_playlistsPagination);
            _playlistService.GetAll(_playlistPayload).Should().BeEquivalentTo(_playlistDtosPagination);
        }

        [Fact] public void GetById_ValidId_Test()
        {
            _playlistRepositoryMock.Setup(mock => mock.GetById(ValidPlaylistId)).Returns(_playlists[0]);
            _playlistService.GetById(ValidPlaylistId).Should().BeEquivalentTo(_playlistDtos[0]);
        }

        [Fact] public void GetById_InvalidId_Test()
        {
            _playlistRepositoryMock.Setup(mock => mock.GetById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(PlaylistNotFound, InvalidId)));
            _playlistService.Invoking(service => service.GetById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(PlaylistNotFound, InvalidId));
            _playlistRepositoryMock.Verify(mock => mock.GetById(InvalidId));
        }

        [Fact] public void Add_Test()
        {
            _songs.Take(2).ToList().ForEach(song => _songRepositoryMock.Setup(mock => mock.GetById(song.Id)).Returns(song));
            _userRepositoryMock.Setup(mock => mock.GetById(ValidUserId)).Returns(_users[0]);
            _playlistRepositoryMock.Setup(mock => mock.Add(It.IsAny<Playlist>())).Returns(_newPlaylist);
            _playlistService.Add(_newPlaylistDto).Should().BeEquivalentTo(_newPlaylistDto, options => options.Excluding(playlist => playlist.Id).Excluding(playlist => playlist.CreatedAt).Excluding(playlist => playlist.UpdatedAt));
            _playlistRepositoryMock.Verify(mock => mock.Add(It.IsAny<Playlist>()));
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Playlist updatedPlaylist = _playlists[0];
            updatedPlaylist.Title = _playlists[1].Title;
            PlaylistDto updatedPlaylistDto = updatedPlaylist.ToDto(_mapper);
            _playlistRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Playlist>(), ValidPlaylistId)).Returns(updatedPlaylist);
            _playlistService.UpdateById(_playlistDtos[1], ValidPlaylistId).Should().BeEquivalentTo(updatedPlaylistDto);
            _playlistRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Playlist>(), ValidPlaylistId));
        }

        [Fact] public void UpdateById_InvalidId_Test()
        {
            _playlistRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Playlist>(), InvalidId)).Throws(new ResourceNotFoundException(string.Format(PlaylistNotFound, InvalidId)));
            _playlistService.Invoking(service => service.UpdateById(_playlistDtos[1], InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(PlaylistNotFound, InvalidId));
            _playlistRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Playlist>(), InvalidId));
        }

        [Fact] public void DeleteById_ValidId_Test()
        {
            Playlist deletedPlaylist = _playlists[0];
            deletedPlaylist.DeletedAt = DateTime.UtcNow;
            PlaylistDto deletedPlaylistDto = deletedPlaylist.ToDto(_mapper);
            _playlistRepositoryMock.Setup(mock => mock.DeleteById(ValidPlaylistId)).Returns(deletedPlaylist);
            _playlistService.DeleteById(ValidPlaylistId).Should().BeEquivalentTo(deletedPlaylistDto);
            _playlistRepositoryMock.Verify(mock => mock.DeleteById(ValidPlaylistId));
        }

        [Fact] public void DeleteById_InvalidId_Test()
        {
            _playlistRepositoryMock.Setup(mock => mock.DeleteById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(PlaylistNotFound, InvalidId)));
            _playlistService.Invoking(service => service.DeleteById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(PlaylistNotFound, InvalidId));
            _playlistRepositoryMock.Verify(mock => mock.DeleteById(InvalidId));
        }
    }
}