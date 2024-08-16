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

        private void Execute(Action<ArtistRepository, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var artistRepository = new ArtistRepository(context);
            using var transaction = context.Database.BeginTransaction();
            context.Artists.AddRange(_artists);
            context.SaveChanges();
            action(artistRepository, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((artistRepository, context) => artistRepository.GetAll(_artistPayload).Should().BeEquivalentTo(_artistsPagination));

        [Fact] public void GetById_ValidId_Test() => Execute((artistRepository, context) => artistRepository.GetById(ValidArtistId).Should().BeEquivalentTo(_artists[0], options => options.Excluding(artist => artist.SimilarArtists)));

        [Fact] public void GetById_InvalidId_Test() => Execute((artistRepository, context) => artistRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((artistRepository, context) =>
        {
            Artist result = artistRepository.Add(_newArtist);
            context.Artists.Find(result.Id).Should().BeEquivalentTo(_newArtist, options => options.Excluding(artist => artist.Id).Excluding(artist => artist.CreatedAt).Excluding(artist => artist.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((artistRepository, context) =>
        {
            Artist updatedArtist = _artists[0];
            updatedArtist.Name = _artists[1].Name;
            updatedArtist.ImageUrl = _artists[1].ImageUrl;
            updatedArtist.Bio = _artists[1].Bio;
            updatedArtist.SimilarArtists = _artists[1].SimilarArtists;
            Artist result = artistRepository.UpdateById(_artists[1], ValidArtistId);
            result.Should().BeEquivalentTo(updatedArtist, options => options.Excluding(artist => artist.UpdatedAt).Excluding(artist => artist.SimilarArtists));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((artistRepository, context) => artistRepository
            .Invoking(repository => repository.UpdateById(_artists[1], InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((artistRepository, context) =>
        {
            Artist result = artistRepository.DeleteById(ValidArtistId);
            result.Should().BeEquivalentTo(_artists[0], options => options.Excluding(artist => artist.DeletedAt).Excluding(artist => artist.SimilarArtists));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((artistRepository, context) => artistRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId)));
    }
}