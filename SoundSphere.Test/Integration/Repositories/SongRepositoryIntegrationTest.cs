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
        private readonly Song _song1 = GetSong1();
        private readonly Song _song2 = GetSong2();
        private readonly Song _newSong = GetNewSong();
        private readonly List<Song> _songs = GetSongs();
        private readonly List<Album> _albums = GetAlbums();
        private readonly List<Artist> _artists = GetArtists();

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

        [Fact] public void GetAll_Test() => Execute((songRepository, context) => songRepository.GetAll().Should().BeEquivalentTo(_songs));

        [Fact] public void GetById_ValidId_Test() => Execute((songRepository, context) => songRepository.GetById(ValidSongId).Should().BeEquivalentTo(_song1, options => options.Excluding(song => song.SimilarSongs)));

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
            Song updatedSong = _song1;
            updatedSong.Title = _song2.Title;
            updatedSong.ImageUrl = _song2.ImageUrl;
            updatedSong.Genre = _song2.Genre;
            updatedSong.ReleaseDate = _song2.ReleaseDate;
            updatedSong.DurationSeconds = _song2.DurationSeconds;
            updatedSong.Album = context.Albums.Find(_song2.Album.Id) ?? _song2.Album;
            updatedSong.Artists = _song2.Artists.Select(artist => context.Artists.Find(artist.Id) ?? artist).ToList();
            updatedSong.SimilarSongs = _song2.SimilarSongs;
            Song result = songRepository.UpdateById(updatedSong, ValidSongId);
            result.Should().BeEquivalentTo(updatedSong, options => options.Excluding(song => song.UpdatedAt).Excluding(song => song.SimilarSongs));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((songRepository, context) => songRepository
            .Invoking(repository => repository.UpdateById(_song2, InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((songRepository, context) =>
        {
            Song result = songRepository.DeleteById(ValidSongId);
            result.Should().BeEquivalentTo(_song1, options => options.Excluding(song => song.DeletedAt).Excluding(song => song.SimilarSongs));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((songRepository, context) => songRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));
    }
}