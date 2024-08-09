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
        private readonly Playlist _playlist1 = GetPlaylist1();
        private readonly Playlist _playlist2 = GetPlaylist2();
        private readonly Playlist _newPlaylist = GetNewPlaylist();
        private readonly List<Playlist> _playlists = GetPlaylists();
        private readonly PlaylistDto _playlistDto1 = GetPlaylistDto1();
        private readonly PlaylistDto _playlistDto2 = GetPlaylistDto2();
        private readonly PlaylistDto _newPlaylistDto = GetNewPlaylistDto();
        private readonly List<PlaylistDto> _playlistDtos = GetPlaylistDtos();
        private readonly User _user1 = GetUser1();
        private readonly List<Song> _songs1 = GetSongs1();

        public PlaylistServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _playlistService = new PlaylistService(_playlistRepositoryMock.Object, _userRepositoryMock.Object, _songRepositoryMock.Object, _mapper);
        }

        [Fact] public void GetAll_Test()
        {
            _playlistRepositoryMock.Setup(mock => mock.GetAll()).Returns(_playlists);
            _playlistService.GetAll().Should().BeEquivalentTo(_playlistDtos);
        }

        [Fact] public void GetById_ValidId_Test()
        {
            _playlistRepositoryMock.Setup(mock => mock.GetById(ValidPlaylistId)).Returns(_playlist1);
            _playlistService.GetById(ValidPlaylistId).Should().BeEquivalentTo(_playlistDto1);
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
            _songs1.ForEach(song => _songRepositoryMock.Setup(mock => mock.GetById(song.Id)).Returns(song));
            _userRepositoryMock.Setup(mock => mock.GetById(ValidUserId)).Returns(_user1);
            _playlistRepositoryMock.Setup(mock => mock.Add(It.IsAny<Playlist>())).Returns(_newPlaylist);
            _playlistService.Add(_newPlaylistDto).Should().BeEquivalentTo(_newPlaylistDto, options => options.Excluding(playlist => playlist.Id).Excluding(playlist => playlist.CreatedAt).Excluding(playlist => playlist.UpdatedAt));
            _playlistRepositoryMock.Verify(mock => mock.Add(It.IsAny<Playlist>()));
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Playlist updatedPlaylist = _playlist1;
            updatedPlaylist.Title = _playlist2.Title;
            PlaylistDto updatedPlaylistDto = updatedPlaylist.ToDto(_mapper);
            _playlistRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Playlist>(), ValidPlaylistId)).Returns(updatedPlaylist);
            _playlistService.UpdateById(_playlistDto2, ValidPlaylistId).Should().BeEquivalentTo(updatedPlaylistDto);
            _playlistRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Playlist>(), ValidPlaylistId));
        }

        [Fact] public void UpdateById_InvalidId_Test()
        {
            _playlistRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Playlist>(), InvalidId)).Throws(new ResourceNotFoundException(string.Format(PlaylistNotFound, InvalidId)));
            _playlistService.Invoking(service => service.UpdateById(_playlistDto2, InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(PlaylistNotFound, InvalidId));
            _playlistRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Playlist>(), InvalidId));
        }

        [Fact] public void DeleteById_ValidId_Test()
        {
            Playlist deletedPlaylist = _playlist1;
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