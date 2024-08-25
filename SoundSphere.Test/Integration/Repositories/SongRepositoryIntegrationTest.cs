using FluentAssertions;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.AlbumMock;
using static SoundSphere.Test.Mocks.ArtistMock;
using static SoundSphere.Test.Mocks.SongMock;

namespace SoundSphere.Test.Integration.Repositories
{
    public class SongRepositoryIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;

        public SongRepositoryIntegrationTest(DbFixture dbFixture) => _dbFixture = dbFixture;

        private async Task ExecuteAsync(Func<SongRepository, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var songRepository = new SongRepository(context);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await _dbFixture.TrackAndAddAsync(context, _albums);
            await _dbFixture.TrackAndAddAsync(context, _artists);
            await _dbFixture.TrackAndAddAsync(context, _songs);
            await action(songRepository, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedSongs() => await ExecuteAsync(async (songRepository, context) => (await songRepository.GetAllAsync(_songPayload)).Should().BeEquivalentTo(_songsPagination));

        [Fact] public async Task GetByIdAsync_ShouldReturnSong_WhenSongIdIsValid() => await ExecuteAsync(async (songRepository, context) => (await songRepository.GetByIdAsync(ValidSongId)).Should().BeEquivalentTo(_songs[0], options => options.Excluding(song => song.SimilarSongs)));

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenSongIdIsInvalid() => await ExecuteAsync(async (songRepository, context) => await songRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));

        [Fact] public async Task AddAsync_ShouldAddNewSong_WhenSongDtoIsValid() => await ExecuteAsync(async (songRepository, context) =>
        {
            songRepository.LinkSongToAlbum(_newSong);
            songRepository.LinkSongToArtists(_newSong);
            Song result = await songRepository.AddAsync(_newSong);
            context.Songs.Find(result.Id).Should().BeEquivalentTo(_newSong, options => options.Excluding(song => song.Id).Excluding(song => song.CreatedAt).Excluding(song => song.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateSong_WhenSongIdIsValid() => await ExecuteAsync(async (songRepository, context) =>
        {
            Song updatedSong = _songs[0];
            updatedSong.Title = _songs[1].Title;
            updatedSong.ImageUrl = _songs[1].ImageUrl;
            updatedSong.Genre = _songs[1].Genre;
            updatedSong.ReleaseDate = _songs[1].ReleaseDate;
            updatedSong.DurationSeconds = _songs[1].DurationSeconds;
            updatedSong.Album = context.Albums.Find(_songs[1].Album.Id) ?? _songs[1].Album;
            updatedSong.Artists = _songs[1].Artists.Select(artist => context.Artists.Find(artist.Id) ?? artist).ToList();
            Song result = await songRepository.UpdateByIdAsync(updatedSong, ValidSongId);
            result.Should().BeEquivalentTo(updatedSong, options => options.Excluding(song => song.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenSongIdIsInvalid() => await ExecuteAsync(async (songRepository, context) => await songRepository
            .Invoking(repository => repository.UpdateByIdAsync(_songs[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteSong_WhenSongIdIsValid() => await ExecuteAsync(async (songRepository, context) =>
        {
            Song result = await songRepository.DeleteByIdAsync(ValidSongId);
            result.Should().BeEquivalentTo(_songs[0], options => options.Excluding(song => song.DeletedAt).Excluding(song => song.SimilarSongs));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenSongIdIsInvalid() => await ExecuteAsync(async (songRepository, context) => await songRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));
    }
}