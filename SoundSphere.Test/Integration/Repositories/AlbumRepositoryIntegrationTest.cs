using FluentAssertions;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.AlbumMock;

namespace SoundSphere.Test.Integration.Repositories
{
    public class AlbumRepositoryIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;

        public AlbumRepositoryIntegrationTest(DbFixture dbFixture) => _dbFixture = dbFixture;

        private async Task ExecuteAsync(Func<AlbumRepository, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var albumRepository = new AlbumRepository(context);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await context.AddRangeAsync(_albums);
            await context.SaveChangesAsync();
            await action(albumRepository, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAll_Test() => await ExecuteAsync(async (albumRepository, context) => (await albumRepository.GetAllAsync(_albumPayload)).Should().BeEquivalentTo(_albumsPagination));

        [Fact] public async Task GetById_ValidId_Test() => await ExecuteAsync(async (albumRepository, context) => (await albumRepository.GetByIdAsync(ValidAlbumId)).Should().BeEquivalentTo(_albums[0], options => options.Excluding(album => album.SimilarAlbums)));

        [Fact] public async Task GetById_InvalidId_Test() => await ExecuteAsync(async (albumRepository, context) => await albumRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));

        [Fact] public async Task Add_Test() => await ExecuteAsync(async (albumRepository, context) =>
        {
            Album result = await albumRepository.AddAsync(_newAlbum);
            context.Albums.Find(result.Id).Should().BeEquivalentTo(_newAlbum, options => options.Excluding(album => album.Id).Excluding(album => album.CreatedAt).Excluding(album => album.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateById_ValidId_Test() => await ExecuteAsync(async (albumRepository, context) =>
        {
            Album updatedAlbum = _albums[0];
            updatedAlbum.Title = _albums[1].Title;
            updatedAlbum.ImageUrl = _albums[1].ImageUrl;
            updatedAlbum.ReleaseDate = _albums[1].ReleaseDate;
            Album result = await albumRepository.UpdateByIdAsync(updatedAlbum, ValidAlbumId);
            result.Should().BeEquivalentTo(updatedAlbum, options => options.Excluding(album => album.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateById_InvalidId_Test() => await ExecuteAsync(async (albumRepository, context) => await albumRepository
            .Invoking(repository => repository.UpdateByIdAsync(_albums[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));

        [Fact] public async Task DeleteById_ValidId_Test() => await ExecuteAsync(async (albumRepository, context) =>
        {
            Album result = await albumRepository.DeleteByIdAsync(ValidAlbumId);
            result.Should().BeEquivalentTo(_albums[0], options => options.Excluding(album => album.DeletedAt).Excluding(album => album.SimilarAlbums));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteById_InvalidId_Test() => await ExecuteAsync(async (albumRepository, context) => await albumRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));
    }
}