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
    
        private void Execute(Action<AlbumRepository, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var albumRepository = new AlbumRepository(context);
            using var transaction = context.Database.BeginTransaction();
            context.Albums.AddRange(_albums);
            context.SaveChanges();
            action(albumRepository, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((albumRepository, context) => albumRepository.GetAll(_albumPayload).Should().BeEquivalentTo(_albumsPagination));

        [Fact] public void GetById_ValidId_Test() => Execute((albumRepository, context) => albumRepository.GetById(ValidAlbumId).Should().BeEquivalentTo(_albums[0], options => options.Excluding(album => album.SimilarAlbums)));

        [Fact] public void GetById_InvalidId_Test() => Execute((albumRepository, context) => albumRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((albumRepository, context) =>
        {
            Album result = albumRepository.Add(_newAlbum);
            context.Albums.Find(result.Id).Should().BeEquivalentTo(_newAlbum, options => options.Excluding(album => album.Id).Excluding(album => album.CreatedAt).Excluding(album => album.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((albumRepository, context) =>
        {
            Album updatedAlbum = _albums[0];
            updatedAlbum.Title = _albums[1].Title;
            updatedAlbum.ImageUrl = _albums[1].ImageUrl;
            updatedAlbum.ReleaseDate = _albums[1].ReleaseDate;
            updatedAlbum.SimilarAlbums = _albums[1].SimilarAlbums;
            Album result = albumRepository.UpdateById(_albums[1], ValidAlbumId);
            result.Should().BeEquivalentTo(updatedAlbum, options => options.Excluding(album => album.UpdatedAt).Excluding(album => album.SimilarAlbums));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((albumRepository, context) => albumRepository
            .Invoking(repository => repository.UpdateById(_albums[1], InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((albumRepository, context) =>
        {
            Album result = albumRepository.DeleteById(ValidAlbumId);
            result.Should().BeEquivalentTo(_albums[0], options => options.Excluding(album => album.DeletedAt).Excluding(album => album.SimilarAlbums));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((albumRepository, context) => albumRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));
    }
}