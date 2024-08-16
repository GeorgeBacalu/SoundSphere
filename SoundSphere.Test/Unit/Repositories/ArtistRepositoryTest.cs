using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.ArtistMock;

namespace SoundSphere.Test.Unit.Repositories
{
    public class ArtistRepositoryTest
    {
        private readonly Mock<DbSet<Artist>> _dbSetMock = new();
        private readonly Mock<AppDbContext> _dbContextMock = new();
        private readonly IArtistRepository _artistRepository;

        public ArtistRepositoryTest()
        {
            IQueryable<Artist> queryableArtists = _artists.AsQueryable();
            _dbSetMock.As<IQueryable<Artist>>().Setup(mock => mock.Provider).Returns(queryableArtists.Provider);
            _dbSetMock.As<IQueryable<Artist>>().Setup(mock => mock.Expression).Returns(queryableArtists.Expression);
            _dbSetMock.As<IQueryable<Artist>>().Setup(mock => mock.ElementType).Returns(queryableArtists.ElementType);
            _dbSetMock.As<IQueryable<Artist>>().Setup(mock => mock.GetEnumerator()).Returns(queryableArtists.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Artists).Returns(_dbSetMock.Object);
            _artistRepository = new ArtistRepository(_dbContextMock.Object);
        }

        [Fact] public void GetAll_Test() => _artistRepository.GetAll(_artistPayload).Should().BeEquivalentTo(_artistsPagination);

        [Fact] public void GetById_ValidId_Test() => _artistRepository.GetById(ValidArtistId).Should().BeEquivalentTo(_artists[0]);

        [Fact] public void GetById_InvalidId_Test() => _artistRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId));

        [Fact] public void Add_Test()
        {
            Artist result = _artistRepository.Add(_newArtist);
            result.Should().BeEquivalentTo(_newArtist, options => options.Excluding(artist => artist.Id).Excluding(artist => artist.CreatedAt).Excluding(artist => artist.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Mock<CustomEntityEntry<Artist>> entryMock = new();
            entryMock.SetupProperty(mock => mock.State, EntityState.Modified);
            _dbContextMock.Setup(mock => mock.Entry(It.IsAny<Artist>())).Returns(entryMock.Object);
            Artist updatedArtist = _artists[0];
            updatedArtist.Name = _artists[1].Name;
            updatedArtist.ImageUrl = _artists[1].ImageUrl;
            updatedArtist.Bio = _artists[1].Bio;
            updatedArtist.SimilarArtists = _artists[1].SimilarArtists;
            Artist result = _artistRepository.UpdateById(_artists[1], ValidArtistId);
            result.Should().BeEquivalentTo(updatedArtist, options => options.Excluding(artist => artist.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_InvalidId_Test() => _artistRepository
            .Invoking(repository => repository.UpdateById(_artists[1], InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId));

        [Fact] public void DeleteById_ValidId_Test()
        {
            Artist result = _artistRepository.DeleteById(ValidArtistId);
            result.Should().BeEquivalentTo(_artists[0], options => options.Excluding(artist => artist.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void DeleteById_InvalidId_Test() => _artistRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId));
    }
}