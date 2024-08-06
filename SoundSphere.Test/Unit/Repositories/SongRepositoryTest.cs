using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.SongMock;

namespace SoundSphere.Test.Unit.Repositories
{
    public class SongRepositoryTest
    {
        private readonly Mock<DbSet<Song>> _dbSetMock = new();
        private readonly Mock<AppDbContext> _dbContextMock = new();
        private readonly ISongRepository _songRepository;
        private readonly Song _song1 = GetSong1();
        private readonly Song _song2 = GetSong2();
        private readonly Song _newSong = GetNewSong();
        private readonly List<Song> _songs = GetSongs();

        public SongRepositoryTest()
        {
            IQueryable<Song> queryableSongs = _songs.AsQueryable();
            _dbSetMock.As<IQueryable<Song>>().Setup(mock => mock.Provider).Returns(queryableSongs.Provider);
            _dbSetMock.As<IQueryable<Song>>().Setup(mock => mock.Expression).Returns(queryableSongs.Expression);
            _dbSetMock.As<IQueryable<Song>>().Setup(mock => mock.ElementType).Returns(queryableSongs.ElementType);
            _dbSetMock.As<IQueryable<Song>>().Setup(mock => mock.GetEnumerator()).Returns(queryableSongs.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Songs).Returns(_dbSetMock.Object);
            _songRepository = new SongRepository(_dbContextMock.Object);
        }

        [Fact] public void GetAll_Test() => _songRepository.GetAll().Should().BeEquivalentTo(_songs);

        [Fact] public void GetById_ValidId_Test() => _songRepository.GetById(ValidSongId).Should().BeEquivalentTo(_song1);

        [Fact] public void GetById_InvalidId_Test() => _songRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId));

        [Fact] public void Add_Test()
        {
            Song result = _songRepository.Add(_newSong);
            result.Should().BeEquivalentTo(_newSong, options => options.Excluding(song => song.Id).Excluding(song => song.CreatedAt).Excluding(song => song.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Mock<CustomEntityEntry<Song>> entryMock = new();
            entryMock.SetupProperty(mock => mock.State, EntityState.Modified);
            _dbContextMock.Setup(mock => mock.Entry(It.IsAny<Song>())).Returns(entryMock.Object);
            Song updatedSong = _song1;
            updatedSong.Title = _song2.Title;
            updatedSong.ImageUrl = _song2.ImageUrl;
            updatedSong.Genre = _song2.Genre;
            updatedSong.ReleaseDate = _song2.ReleaseDate;
            updatedSong.DurationSeconds = _song2.DurationSeconds;
            updatedSong.Album = _song2.Album;
            updatedSong.Artists = _song2.Artists;
            updatedSong.SimilarSongs = _song2.SimilarSongs;
            Song result = _songRepository.UpdateById(_song2, ValidSongId);
            result.Should().BeEquivalentTo(updatedSong, options => options.Excluding(song => song.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_InvalidId_Test() => _songRepository
            .Invoking(repository => repository.UpdateById(_song2, InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId));

        [Fact] public void DeleteById_ValidId_Test()
        {
            Song result = _songRepository.DeleteById(ValidSongId);
            result.Should().BeEquivalentTo(_song1, options => options.Excluding(song => song.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void DeleteById_InvalidId_Test() => _songRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId));
    }
}