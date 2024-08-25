using AutoMapper;
using FluentAssertions;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services;
using SoundSphere.Database.Context;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.ArtistMock;

namespace SoundSphere.Test.Integration.Services
{
    public class ArtistServiceIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;
        private readonly IMapper _mapper;

        public ArtistServiceIntegrationTest(DbFixture dbFixture) => (_dbFixture, _mapper) = (dbFixture, new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper());

        private async Task ExecuteAsync(Func<ArtistService, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var artistService = new ArtistService(new ArtistRepository(context), _mapper);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await _dbFixture.TrackAndAddAsync(context, _artists);
            await action(artistService, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedArtists() => await ExecuteAsync(async (artistService, context) => (await artistService.GetAllAsync(_artistPayload)).Should().BeEquivalentTo(_artistDtosPagination));

        [Fact] public async Task GetByIdAsync_ShouldReturnArtist_WhenArtistIdIsValid() => await ExecuteAsync(async (artistService, context) => (await artistService.GetByIdAsync(ValidArtistId)).Should().BeEquivalentTo(_artistDtos[0]));

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid() => await ExecuteAsync(async (artistService, context) => await artistService
            .Invoking(service => service.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId)));

        [Fact] public async Task AddAsync_ShouldAddNewArtist_WhenArtistDtoIsValid() => await ExecuteAsync(async (artistService, context) =>
        {
            ArtistDto result = await artistService.AddAsync(_newArtistDto);
            (await context.Artists.FindAsync(result.Id)).Should().BeEquivalentTo(_newArtist, options => options.Excluding(album => album.Id).Excluding(album => album.CreatedAt).Excluding(album => album.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateArtist_WhenArtistIdIsValid() => await ExecuteAsync(async (artistService, context) =>
        {
            Artist updatedArtist = _artists[0];
            updatedArtist.Name = _artists[1].Name;
            updatedArtist.ImageUrl = _artists[1].ImageUrl;
            updatedArtist.Bio = _artists[1].Bio;
            ArtistDto updatedArtistDto = updatedArtist.ToDto(_mapper);
            ArtistDto result = await artistService.UpdateByIdAsync(_artistDtos[1], ValidArtistId);
            result.Should().BeEquivalentTo(updatedArtistDto, options => options.Excluding(artist => artist.UpdatedAt).Excluding(artist => artist.SimilarArtistsIds));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid() => await ExecuteAsync(async (artistService, context) => await artistService
            .Invoking(service => service.UpdateByIdAsync(_artistDtos[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId)));

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteArtist_WhenArtistIdIsValid() => await ExecuteAsync(async (artistService, context) =>
        {
            ArtistDto result = await artistService.DeleteByIdAsync(ValidArtistId);
            result.Should().BeEquivalentTo(_artistDtos[0], options => options.Excluding(artist => artist.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid() => await ExecuteAsync(async (artistService, context) => await artistService
            .Invoking(service => service.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId)));
    }
}