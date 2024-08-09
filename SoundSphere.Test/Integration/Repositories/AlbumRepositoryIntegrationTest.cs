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
        private readonly Album _album1 = GetAlbum1();
        private readonly Album _album2 = GetAlbum2();
        private readonly Album _newAlbum = GetNewAlbum();
        private readonly List<Album> _albums = GetAlbums();

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

        [Fact] public void GetAll_Test() => Execute((albumRepository, context) => albumRepository.GetAll().Should().BeEquivalentTo(_albums));

        [Fact] public void GetById_ValidId_Test() => Execute((albumRepository, context) => albumRepository.GetById(ValidAlbumId).Should().BeEquivalentTo(_album1, options => options.Excluding(album => album.SimilarAlbums)));

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
            Album updatedAlbum = _album1;
            updatedAlbum.Title = _album2.Title;
            updatedAlbum.ImageUrl = _album2.ImageUrl;
            updatedAlbum.ReleaseDate = _album2.ReleaseDate;
            updatedAlbum.SimilarAlbums = _album2.SimilarAlbums;
            Album result = albumRepository.UpdateById(_album2, ValidAlbumId);
            result.Should().BeEquivalentTo(updatedAlbum, options => options.Excluding(album => album.UpdatedAt).Excluding(album => album.SimilarAlbums));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((albumRepository, context) => albumRepository
            .Invoking(repository => repository.UpdateById(_album2, InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((albumRepository, context) =>
        {
            Album result = albumRepository.DeleteById(ValidAlbumId);
            result.Should().BeEquivalentTo(_album1, options => options.Excluding(album => album.DeletedAt).Excluding(album => album.SimilarAlbums));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((albumRepository, context) => albumRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));
    }
}