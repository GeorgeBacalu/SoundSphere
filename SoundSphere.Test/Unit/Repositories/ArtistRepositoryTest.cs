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
            var asyncQueryableArtists = (IQueryable<Artist>)new AsyncQueryable<Artist>(_artists);
            _dbSetMock.As<IQueryable<Artist>>().Setup(mock => mock.Provider).Returns(asyncQueryableArtists.Provider);
            _dbSetMock.As<IQueryable<Artist>>().Setup(mock => mock.Expression).Returns(asyncQueryableArtists.Expression);
            _dbSetMock.As<IQueryable<Artist>>().Setup(mock => mock.ElementType).Returns(asyncQueryableArtists.ElementType);
            _dbSetMock.As<IQueryable<Artist>>().Setup(mock => mock.GetEnumerator()).Returns(asyncQueryableArtists.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Artists).Returns(_dbSetMock.Object);
            _artistRepository = new ArtistRepository(_dbContextMock.Object);
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedArtists() => (await _artistRepository.GetAllAsync(_artistPayload)).Should().BeEquivalentTo(_artistsPagination);

        [Fact] public async Task GetByIdAsync_ShouldReturnArtist_WhenArtistIdIsValid() => (await _artistRepository.GetByIdAsync(ValidArtistId)).Should().BeEquivalentTo(_artists[0]);

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid() => await _artistRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId));

        [Fact] public async Task AddAsync_ShouldAddNewArtist_WhenArtistDtoIsValid()
        {
            Artist result = await _artistRepository.AddAsync(_newArtist);
            result.Should().BeEquivalentTo(_newArtist, options => options.Excluding(artist => artist.Id).Excluding(artist => artist.CreatedAt).Excluding(artist => artist.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateArtist_WhenArtistIdIsValid()
        {
            Mock<CustomEntityEntry<Artist>> entryMock = new();
            entryMock.SetupProperty(mock => mock.State, EntityState.Modified);
            _dbContextMock.Setup(mock => mock.Entry(It.IsAny<Artist>())).Returns(entryMock.Object);
            Artist updatedArtist = _artists[0];
            updatedArtist.Name = _artists[1].Name;
            updatedArtist.ImageUrl = _artists[1].ImageUrl;
            updatedArtist.Bio = _artists[1].Bio;
            updatedArtist.SimilarArtists = _artists[1].SimilarArtists;
            Artist result = await _artistRepository.UpdateByIdAsync(_artists[1], ValidArtistId);
            result.Should().BeEquivalentTo(updatedArtist, options => options.Excluding(artist => artist.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid() => await _artistRepository
            .Invoking(repository => repository.UpdateByIdAsync(_artists[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId));

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteUser_WhenArtistIdIsValid()
        {
            Artist result = await _artistRepository.DeleteByIdAsync(ValidArtistId);
            result.Should().BeEquivalentTo(_artists[0], options => options.Excluding(artist => artist.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid() => await _artistRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId));
    }
}