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
        private readonly Album _album1 = GetAlbum1();
        private readonly Album _album2 = GetAlbum2();
        private readonly Album _newAlbum = GetNewAlbum();
        private readonly List<Album> _albums = GetAlbums();

        public AlbumRepositoryTest()
        {
            IQueryable<Album> queryableAlbums = _albums.AsQueryable();
            _dbSetMock.As<IQueryable<Album>>().Setup(mock => mock.Provider).Returns(queryableAlbums.Provider);
            _dbSetMock.As<IQueryable<Album>>().Setup(mock => mock.Expression).Returns(queryableAlbums.Expression);
            _dbSetMock.As<IQueryable<Album>>().Setup(mock => mock.ElementType).Returns(queryableAlbums.ElementType);
            _dbSetMock.As<IQueryable<Album>>().Setup(mock => mock.GetEnumerator()).Returns(queryableAlbums.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Albums).Returns(_dbSetMock.Object);
            _albumRepository = new AlbumRepository(_dbContextMock.Object);
        }

        [Fact] public void GetAll_Test() => _albumRepository.GetAll().Should().BeEquivalentTo(_albums);

        [Fact] public void GetById_ValidId_Test() => _albumRepository.GetById(ValidAlbumId).Should().BeEquivalentTo(_album1);

        [Fact] public void GetById_InvalidId_Test() => _albumRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId));

        [Fact] public void Add_Test()
        {
            Album result = _albumRepository.Add(_newAlbum);
            result.Should().BeEquivalentTo(_newAlbum, options => options.Excluding(album => album.Id).Excluding(album => album.CreatedAt).Excluding(album => album.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Mock<CustomEntityEntry<Album>> entryMock = new();
            entryMock.SetupProperty(mock => mock.State, EntityState.Modified);
            _dbContextMock.Setup(mock => mock.Entry(It.IsAny<Album>())).Returns(entryMock.Object);
            Album updatedAlbum = _album1;
            updatedAlbum.Title = _album2.Title;
            updatedAlbum.ImageUrl = _album2.ImageUrl;
            updatedAlbum.ReleaseDate = _album2.ReleaseDate;
            updatedAlbum.SimilarAlbums = _album2.SimilarAlbums;
            Album result = _albumRepository.UpdateById(_album2, ValidAlbumId);
            result.Should().BeEquivalentTo(updatedAlbum, options => options.Excluding(album => album.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_InvalidId_Test() => _albumRepository
            .Invoking(repository => repository.UpdateById(_album2, InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId));

        [Fact] public void DeleteById_ValidId_Test()
        {
            Album result = _albumRepository.DeleteById(ValidAlbumId);
            result.Should().BeEquivalentTo(_album1, options => options.Excluding(album => album.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void DeleteById_InvalidId_Test() => _albumRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId));
    }
}