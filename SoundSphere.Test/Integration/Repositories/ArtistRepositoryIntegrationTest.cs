using FluentAssertions;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.ArtistMock;

namespace SoundSphere.Test.Integration.Repositories
{
    public class ArtistRepositoryIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;

        public ArtistRepositoryIntegrationTest(DbFixture dbFixture) => _dbFixture = dbFixture;

        private async Task ExecuteAsync(Func<ArtistRepository, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var artistRepository = new ArtistRepository(context);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await context.Artists.AddRangeAsync(_artists);
            await context.SaveChangesAsync();
            await action(artistRepository, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedArtists() => await ExecuteAsync(async (artistRepository, context) => (await artistRepository.GetAllAsync(_artistPayload)).Should().BeEquivalentTo(_artistsPagination));

        [Fact] public async Task GetByIdAsync_ShouldReturnArtist_WhenArtistIdIsValid() => await ExecuteAsync(async (artistRepository, context) => (await artistRepository.GetByIdAsync(ValidArtistId)).Should().BeEquivalentTo(_artists[0], options => options.Excluding(artist => artist.SimilarArtists)));

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid() => await ExecuteAsync(async (artistRepository, context) => await artistRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId)));

        [Fact] public async Task AddAsync_ShouldAddNewArtist_WhenArtistDtoIsValid() => await ExecuteAsync(async (artistRepository, context) =>
        {
            Artist result = await artistRepository.AddAsync(_newArtist);
            context.Artists.Find(result.Id).Should().BeEquivalentTo(_newArtist, options => options.Excluding(artist => artist.Id).Excluding(artist => artist.CreatedAt).Excluding(artist => artist.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateArtist_WhenArtistIdIsValid() => await ExecuteAsync(async (artistRepository, context) =>
        {
            Artist updatedArtist = _artists[0];
            updatedArtist.Name = _artists[1].Name;
            updatedArtist.ImageUrl = _artists[1].ImageUrl;
            updatedArtist.Bio = _artists[1].Bio;
            Artist result = await artistRepository.UpdateByIdAsync(_artists[1], ValidArtistId);
            result.Should().BeEquivalentTo(updatedArtist, options => options.Excluding(artist => artist.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid() => await ExecuteAsync(async (artistRepository, context) => await artistRepository
            .Invoking(repository => repository.UpdateByIdAsync(_artists[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId)));

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteArtist_WhenArtistIdIsValid() => await ExecuteAsync(async (artistRepository, context) =>
        {
            Artist result = await artistRepository.DeleteByIdAsync(ValidArtistId);
            result.Should().BeEquivalentTo(_artists[0], options => options.Excluding(artist => artist.DeletedAt).Excluding(artist => artist.SimilarArtists));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid() => await ExecuteAsync(async (artistRepository, context) => await artistRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId)));
    }
}