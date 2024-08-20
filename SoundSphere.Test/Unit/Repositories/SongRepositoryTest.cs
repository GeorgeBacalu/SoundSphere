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

        public SongRepositoryTest()
        {
            var asyncQueryableSongs = (IQueryable<Song>)new AsyncQueryable<Song>(_songs);
            _dbSetMock.As<IQueryable<Song>>().Setup(mock => mock.Provider).Returns(asyncQueryableSongs.Provider);
            _dbSetMock.As<IQueryable<Song>>().Setup(mock => mock.Expression).Returns(asyncQueryableSongs.Expression);
            _dbSetMock.As<IQueryable<Song>>().Setup(mock => mock.ElementType).Returns(asyncQueryableSongs.ElementType);
            _dbSetMock.As<IQueryable<Song>>().Setup(mock => mock.GetEnumerator()).Returns(asyncQueryableSongs.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Songs).Returns(_dbSetMock.Object);
            _songRepository = new SongRepository(_dbContextMock.Object);
        }

        [Fact] public async Task GetAll_Test() => (await _songRepository.GetAllAsync(_songPayload)).Should().BeEquivalentTo(_songsPagination);

        [Fact] public async Task GetById_ValidId_Test() => (await _songRepository.GetByIdAsync(ValidSongId)).Should().BeEquivalentTo(_songs[0]);

        [Fact] public async Task GetById_InvalidId_Test() => await _songRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId));

        [Fact] public async Task Add_Test()
        {
            Song result = await _songRepository.AddAsync(_newSong);
            result.Should().BeEquivalentTo(_newSong, options => options.Excluding(song => song.Id).Excluding(song => song.CreatedAt).Excluding(song => song.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateById_ValidId_Test()
        {
            Mock<CustomEntityEntry<Song>> entryMock = new();
            entryMock.SetupProperty(mock => mock.State, EntityState.Modified);
            _dbContextMock.Setup(mock => mock.Entry(It.IsAny<Song>())).Returns(entryMock.Object);
            Song updatedSong = _songs[0];
            updatedSong.Title = _songs[1].Title;
            updatedSong.ImageUrl = _songs[1].ImageUrl;
            updatedSong.Genre = _songs[1].Genre;
            updatedSong.ReleaseDate = _songs[1].ReleaseDate;
            updatedSong.DurationSeconds = _songs[1].DurationSeconds;
            updatedSong.Album = _songs[1].Album;
            updatedSong.Artists = _songs[1].Artists;
            updatedSong.SimilarSongs = _songs[1].SimilarSongs;
            Song result = await _songRepository.UpdateByIdAsync(_songs[1], ValidSongId);
            result.Should().BeEquivalentTo(updatedSong, options => options.Excluding(song => song.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateById_InvalidId_Test() => await _songRepository
            .Invoking(repository => repository.UpdateByIdAsync(_songs[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId));

        [Fact] public async Task DeleteById_ValidId_Test()
        {
            Song result = await _songRepository.DeleteByIdAsync(ValidSongId);
            result.Should().BeEquivalentTo(_songs[0], options => options.Excluding(song => song.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task DeleteById_InvalidId_Test() => await _songRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId));
    }
}