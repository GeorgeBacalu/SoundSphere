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

        private void Execute(Action<SongRepository, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var songRepository = new SongRepository(context);
            using var transaction = context.Database.BeginTransaction();
            _dbFixture.TrackAndAddEntities(context, _albums);
            _dbFixture.TrackAndAddEntities(context, _artists);
            _dbFixture.TrackAndAddEntities(context, _songs);
            action(songRepository, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((songRepository, context) => songRepository.GetAll(_songPayload).Should().BeEquivalentTo(_songsPagination));

        [Fact] public void GetById_ValidId_Test() => Execute((songRepository, context) => songRepository.GetById(ValidSongId).Should().BeEquivalentTo(_songs[0], options => options.Excluding(song => song.SimilarSongs)));

        [Fact] public void GetById_InvalidId_Test() => Execute((songRepository, context) => songRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((songRepository, context) =>
        {
            songRepository.LinkSongToAlbum(_newSong);
            songRepository.LinkSongToArtists(_newSong);
            Song result = songRepository.Add(_newSong);
            context.Songs.Find(result.Id).Should().BeEquivalentTo(_newSong, options => options.Excluding(song => song.Id).Excluding(song => song.CreatedAt).Excluding(song => song.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((songRepository, context) =>
        {
            Song updatedSong = _songs[0];
            updatedSong.Title = _songs[1].Title;
            updatedSong.ImageUrl = _songs[1].ImageUrl;
            updatedSong.Genre = _songs[1].Genre;
            updatedSong.ReleaseDate = _songs[1].ReleaseDate;
            updatedSong.DurationSeconds = _songs[1].DurationSeconds;
            updatedSong.Album = context.Albums.Find(_songs[1].Album.Id) ?? _songs[1].Album;
            updatedSong.Artists = _songs[1].Artists.Select(artist => context.Artists.Find(artist.Id) ?? artist).ToList();
            updatedSong.SimilarSongs = _songs[1].SimilarSongs;
            Song result = songRepository.UpdateById(updatedSong, ValidSongId);
            result.Should().BeEquivalentTo(updatedSong, options => options.Excluding(song => song.UpdatedAt).Excluding(song => song.SimilarSongs));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((songRepository, context) => songRepository
            .Invoking(repository => repository.UpdateById(_songs[1], InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((songRepository, context) =>
        {
            Song result = songRepository.DeleteById(ValidSongId);
            result.Should().BeEquivalentTo(_songs[0], options => options.Excluding(song => song.DeletedAt).Excluding(song => song.SimilarSongs));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((songRepository, context) => songRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));
    }
}