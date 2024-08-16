using FluentAssertions;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.PlaylistMock;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Integration.Repositories
{
    public class PlaylistRepositoryIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;

        public PlaylistRepositoryIntegrationTest(DbFixture dbFixture) => _dbFixture = dbFixture;

        private void Execute(Action<PlaylistRepository, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var playlistRepository = new PlaylistRepository(context);
            using var transaction = context.Database.BeginTransaction();
            _dbFixture.TrackAndAddEntities(context, _users);
            _dbFixture.TrackAndAddEntities(context, _playlists);
            action(playlistRepository, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((playlistRepository, context) => playlistRepository.GetAll(_playlistPayload).Should().BeEquivalentTo(_playlistsPagination));

        [Fact] public void GetById_ValidId_Test() => Execute((playlistRepository, context) => playlistRepository.GetById(ValidPlaylistId).Should().BeEquivalentTo(_playlists[0]));

        [Fact] public void GetById_InvalidId_Test() => Execute((playlistRepository, context) => playlistRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((playlistRepository, context) =>
        {
            playlistRepository.LinkPlaylistToUser(_newPlaylist);
            Playlist result = playlistRepository.Add(_newPlaylist);
            context.Playlists.Find(result.Id).Should().BeEquivalentTo(_newPlaylist, options => options.Excluding(playlist => playlist.Id).Excluding(playlist => playlist.CreatedAt).Excluding(playlist => playlist.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((playlistRepository, context) =>
        {
            Playlist updatedPlaylist = _playlists[0];
            updatedPlaylist.Title = _playlists[1].Title;
            Playlist result = playlistRepository.UpdateById(_playlists[1], ValidPlaylistId);
            result.Should().BeEquivalentTo(updatedPlaylist, options => options.Excluding(playlist => playlist.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((playlistRepository, context) => playlistRepository
            .Invoking(repository => repository.UpdateById(_playlists[1], InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((playlistRepository, context) =>
        {
            Playlist result = playlistRepository.DeleteById(ValidPlaylistId);
            result.Should().BeEquivalentTo(_playlists[0], options => options.Excluding(playlist => playlist.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((playlistRepository, context) => playlistRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));
    }
}