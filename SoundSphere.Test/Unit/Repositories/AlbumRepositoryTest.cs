using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.AlbumMock;

namespace SoundSphere.Test.Unit.Repositories
{
    public class AlbumRepositoryTest
    {
        private readonly Mock<DbSet<Album>> _dbSetMock = new();
        private readonly Mock<AppDbContext> _dbContextMock = new();
        private readonly IAlbumRepository _albumRepository;

        public AlbumRepositoryTest()
        {
            var asyncQueryableAlbums = (IQueryable<Album>)new AsyncQueryable<Album>(_albums);
            _dbSetMock.As<IQueryable<Album>>().Setup(mock => mock.Provider).Returns(asyncQueryableAlbums.Provider);
            _dbSetMock.As<IQueryable<Album>>().Setup(mock => mock.Expression).Returns(asyncQueryableAlbums.Expression);
            _dbSetMock.As<IQueryable<Album>>().Setup(mock => mock.ElementType).Returns(asyncQueryableAlbums.ElementType);
            _dbSetMock.As<IQueryable<Album>>().Setup(mock => mock.GetEnumerator()).Returns(asyncQueryableAlbums.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Albums).Returns(_dbSetMock.Object);
            _albumRepository = new AlbumRepository(_dbContextMock.Object);
        }

        [Fact] public async Task GetAll_Test() => (await _albumRepository.GetAllAsync(_albumPayload)).Should().BeEquivalentTo(_albumsPagination);

        [Fact] public async Task GetById_ValidId_Test() => (await _albumRepository.GetByIdAsync(ValidAlbumId)).Should().BeEquivalentTo(_albums[0]);

        [Fact] public async Task GetById_InvalidId_Test() => await _albumRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId));

        [Fact] public async Task Add_Test()
        {
            Album result = await _albumRepository.AddAsync(_newAlbum);
            result.Should().BeEquivalentTo(_newAlbum, options => options.Excluding(album => album.Id).Excluding(album => album.CreatedAt).Excluding(album => album.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateById_ValidId_Test()
        {
            Mock<CustomEntityEntry<Album>> entryMock = new();
            entryMock.SetupProperty(mock => mock.State, EntityState.Modified);
            _dbContextMock.Setup(mock => mock.Entry(It.IsAny<Album>())).Returns(entryMock.Object);
            Album updatedAlbum = _albums[0];
            updatedAlbum.Title = _albums[1].Title;
            updatedAlbum.ImageUrl = _albums[1].ImageUrl;
            updatedAlbum.ReleaseDate = _albums[1].ReleaseDate;
            updatedAlbum.SimilarAlbums = _albums[1].SimilarAlbums;
            Album result = await _albumRepository.UpdateByIdAsync(_albums[1], ValidAlbumId);
            result.Should().BeEquivalentTo(updatedAlbum, options => options.Excluding(album => album.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateById_InvalidId_Test() => await _albumRepository
            .Invoking(repository => repository.UpdateByIdAsync(_albums[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId));

        [Fact] public async Task DeleteById_ValidId_Test()
        {
            Album result = await _albumRepository.DeleteByIdAsync(ValidAlbumId);
            result.Should().BeEquivalentTo(_albums[0], options => options.Excluding(album => album.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task DeleteById_InvalidId_Test() => await _albumRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId));
    }
}