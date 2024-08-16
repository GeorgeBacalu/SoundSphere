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

        [Fact] public void GetAll_Test() => Execute((playlistService, context) => playlistService.GetAll(_playlistPayload).Should().BeEquivalentTo(_playlistDtosPagination));

        [Fact] public void GetById_ValidId_Test() => Execute((playlistService, context) => playlistService.GetById(ValidPlaylistId).Should().BeEquivalentTo(_playlistDtos[0]));

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
            Playlist updatedPlaylist = _playlists[0];
            updatedPlaylist.Title = _playlists[1].Title;
            PlaylistDto updatedPlaylistDto = updatedPlaylist.ToDto(_mapper);
            PlaylistDto result = playlistService.UpdateById(_playlistDtos[1], ValidPlaylistId);
            result.Should().BeEquivalentTo(updatedPlaylistDto, options => options.Excluding(playlist => playlist.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((playlistService, context) => playlistService
            .Invoking(service => service.UpdateById(_playlistDtos[1], InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((playlistService, context) =>
        {
            PlaylistDto result = playlistService.DeleteById(ValidPlaylistId);
            result.Should().BeEquivalentTo(_playlistDtos[0], options => options.Excluding(playlist => playlist.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((playlistService, context) => playlistService
            .Invoking(service => service.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));
    }
}