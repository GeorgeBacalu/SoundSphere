using FluentAssertions;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.AlbumMock;
using static SoundSphere.Test.Mocks.PlaylistMock;
using static SoundSphere.Test.Mocks.SongMock;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Integration.Repositories
{
    public class PlaylistRepositoryIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;

        public PlaylistRepositoryIntegrationTest(DbFixture dbFixture) => _dbFixture = dbFixture;

        private async Task ExecuteAsync(Func<PlaylistRepository, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var playlistRepository = new PlaylistRepository(context);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await _dbFixture.TrackAndAddAsync(context, _albums);
            await _dbFixture.TrackAndAddAsync(context, _songs);
            await _dbFixture.TrackAndAddAsync(context, _users);
            await _dbFixture.TrackAndAddAsync(context, _playlists);
            await action(playlistRepository, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAll_Test() => await ExecuteAsync(async (playlistRepository, context) => (await playlistRepository.GetAllAsync(_playlistPayload)).Should().BeEquivalentTo(_playlistsPagination));

        [Fact] public async Task GetById_ValidId_Test() => await ExecuteAsync(async (playlistRepository, context) => (await playlistRepository.GetByIdAsync(ValidPlaylistId)).Should().BeEquivalentTo(_playlists[0]));

        [Fact] public async Task GetById_InvalidId_Test() => await ExecuteAsync(async (playlistRepository, context) => await playlistRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));

        [Fact] public async Task Add_Test() => await ExecuteAsync(async (playlistRepository, context) =>
        {
            playlistRepository.LinkPlaylistToUser(_newPlaylist);
            Playlist result = await playlistRepository.AddAsync(_newPlaylist);
            context.Playlists.Find(result.Id).Should().BeEquivalentTo(_newPlaylist, options => options.Excluding(playlist => playlist.Id).Excluding(playlist => playlist.CreatedAt).Excluding(playlist => playlist.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateById_ValidId_Test() => await ExecuteAsync(async (playlistRepository, context) =>
        {
            Playlist updatedPlaylist = _playlists[0];
            updatedPlaylist.Title = _playlists[1].Title;
            Playlist result = await playlistRepository.UpdateByIdAsync(_playlists[1], ValidPlaylistId);
            result.Should().BeEquivalentTo(updatedPlaylist, options => options.Excluding(playlist => playlist.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateById_InvalidId_Test() => await ExecuteAsync(async (playlistRepository, context) => await playlistRepository
            .Invoking(repository => repository.UpdateByIdAsync(_playlists[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));

        [Fact] public async Task DeleteById_ValidId_Test() => await ExecuteAsync(async (playlistRepository, context) =>
        {
            Playlist result = await playlistRepository.DeleteByIdAsync(ValidPlaylistId);
            result.Should().BeEquivalentTo(_playlists[0], options => options.Excluding(playlist => playlist.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteById_InvalidId_Test() => await ExecuteAsync(async (playlistRepository, context) => await playlistRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));
    }
}