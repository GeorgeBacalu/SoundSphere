using AutoMapper;
using FluentAssertions;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services;
using SoundSphere.Database.Context;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.AlbumMock;
using static SoundSphere.Test.Mocks.PlaylistMock;
using static SoundSphere.Test.Mocks.SongMock;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Integration.Services
{
    public class PlaylistServiceIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;
        private readonly IMapper _mapper;
        private readonly Playlist _playlist1 = GetPlaylist1();
        private readonly Playlist _playlist2 = GetPlaylist2();
        private readonly Playlist _newPlaylist = GetNewPlaylist();
        private readonly List<Playlist> _playlists = GetPlaylists();
        private readonly PlaylistDto _playlistDto1 = GetPlaylistDto1();
        private readonly PlaylistDto _playlistDto2 = GetPlaylistDto2();
        private readonly PlaylistDto _newPlaylistDto = GetNewPlaylistDto();
        private readonly List<PlaylistDto> _playlistDtos = GetPlaylistDtos();
        private readonly List<Album> _albums = GetAlbums();
        private readonly List<Song> _songs = GetSongs();
        private readonly List<User> _users = GetUsers();

        public PlaylistServiceIntegrationTest(DbFixture dbFixture) => (_dbFixture, _mapper) = (dbFixture, new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper());

        private void Execute(Action<PlaylistService, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var playlistService = new PlaylistService(new PlaylistRepository(context), new UserRepository(context), new SongRepository(context), _mapper);
            using var transaction = context.Database.BeginTransaction();
            _dbFixture.TrackAndAddEntities(context, _albums);
            _dbFixture.TrackAndAddEntities(context, _songs);
            _dbFixture.TrackAndAddEntities(context, _users);
            _dbFixture.TrackAndAddEntities(context, _playlists);
            action(playlistService, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((playlistService, context) => playlistService.GetAll().Should().BeEquivalentTo(_playlistDtos));

        [Fact] public void GetById_ValidId_Test() => Execute((playlistService, context) => playlistService.GetById(ValidPlaylistId).Should().BeEquivalentTo(_playlistDto1));

        [Fact] public void GetById_InvalidId_Test() => Execute((playlistService, context) => playlistService
            .Invoking(service => service.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((playlistService, context) =>
        {
            PlaylistDto result = playlistService.Add(_newPlaylistDto);
            context.Playlists.Find(result.Id).Should().BeEquivalentTo(_newPlaylist, options => options.Excluding(playlist => playlist.Id).Excluding(playlist => playlist.CreatedAt).Excluding(playlist => playlist.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((playlistService, context) =>
        {
            Playlist updatedPlaylist = _playlist1;
            updatedPlaylist.Title = _playlist2.Title;
            PlaylistDto updatedPlaylistDto = updatedPlaylist.ToDto(_mapper);
            PlaylistDto result = playlistService.UpdateById(_playlistDto2, ValidPlaylistId);
            result.Should().BeEquivalentTo(updatedPlaylistDto, options => options.Excluding(playlist => playlist.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((playlistService, context) => playlistService
            .Invoking(service => service.UpdateById(_playlistDto2, InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((playlistService, context) =>
        {
            PlaylistDto result = playlistService.DeleteById(ValidPlaylistId);
            result.Should().BeEquivalentTo(_playlistDto1, options => options.Excluding(playlist => playlist.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((playlistService, context) => playlistService
            .Invoking(service => service.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));
    }
}