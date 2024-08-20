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
using static SoundSphere.Test.Mocks.AlbumMock;

namespace SoundSphere.Test.Integration.Services
{
    public class AlbumServiceIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;
        private readonly IMapper _mapper;

        public AlbumServiceIntegrationTest(DbFixture dbFixture) => (_dbFixture, _mapper) = (dbFixture, new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper());

        private async Task ExecuteAsync(Func<AlbumService, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var albumService = new AlbumService(new AlbumRepository(context), _mapper);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await context.Albums.AddRangeAsync(_albums);
            await context.SaveChangesAsync();
            await action(albumService, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAll_Test() => await ExecuteAsync(async (albumService, context) => (await albumService.GetAllAsync(_albumPayload)).Should().BeEquivalentTo(_albumDtosPagination));

        [Fact] public async Task GetById_ValidId_Test() => await ExecuteAsync(async (albumService, context) => (await albumService.GetByIdAsync(ValidAlbumId)).Should().BeEquivalentTo(_albumDtos[0]));

        [Fact] public async Task GetById_InvalidId_Test() => await ExecuteAsync(async (albumService, context) => await albumService
            .Invoking(service => service.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));

        [Fact] public async Task Add_Test() => await ExecuteAsync(async (albumService, context) =>
        {
            AlbumDto result = await albumService.AddAsync(_newAlbumDto);
            context.Albums.Find(result.Id).Should().BeEquivalentTo(_newAlbum, options => options.Excluding(album => album.Id).Excluding(album => album.CreatedAt).Excluding(album => album.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateById_ValidId_Test() => await ExecuteAsync(async (albumService, context) =>
        {
            Album updatedAlbum = _albums[0];
            updatedAlbum.Title = _albums[1].Title;
            updatedAlbum.ImageUrl = _albums[1].ImageUrl;
            updatedAlbum.ReleaseDate = _albums[1].ReleaseDate;
            AlbumDto updatedAlbumDto = updatedAlbum.ToDto(_mapper);
            AlbumDto result = await albumService.UpdateByIdAsync(_albumDtos[1], ValidAlbumId);
            result.Should().BeEquivalentTo(updatedAlbumDto, options => options.Excluding(album => album.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateById_InvalidId_Test() => await ExecuteAsync(async (albumService, context) => await albumService
            .Invoking(service => service.UpdateByIdAsync(_albumDtos[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));

        [Fact] public async Task DeleteById_ValidId_Test() => await ExecuteAsync(async (albumService, context) =>
        {
            AlbumDto result = await albumService.DeleteByIdAsync(ValidAlbumId);
            result.Should().BeEquivalentTo(_albumDtos[0], options => options.Excluding(album => album.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteById_InvalidId_Test() => await ExecuteAsync(async (albumService, context) => await albumService
            .Invoking(service => service.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));
    }
}