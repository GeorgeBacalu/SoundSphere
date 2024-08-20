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
using static SoundSphere.Test.Mocks.PlaylistMock;
using static SoundSphere.Test.Mocks.SongMock;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Integration.Services
{
    public class PlaylistServiceIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;
        private readonly IMapper _mapper;

        public PlaylistServiceIntegrationTest(DbFixture dbFixture) => (_dbFixture, _mapper) = (dbFixture, new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper());

        private async Task ExecuteAsync(Func<PlaylistService, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var playlistService = new PlaylistService(new PlaylistRepository(context), new UserRepository(context), new SongRepository(context), _mapper);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await _dbFixture.TrackAndAddAsync(context, _albums);
            await _dbFixture.TrackAndAddAsync(context, _songs);
            await _dbFixture.TrackAndAddAsync(context, _users);
            await _dbFixture.TrackAndAddAsync(context, _playlists);
            await action(playlistService, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAll_Test() => await ExecuteAsync(async (playlistService, context) => (await playlistService.GetAllAsync(_playlistPayload)).Should().BeEquivalentTo(_playlistDtosPagination));

        [Fact] public async Task GetById_ValidId_Test() => await ExecuteAsync(async (playlistService, context) => (await playlistService.GetByIdAsync(ValidPlaylistId)).Should().BeEquivalentTo(_playlistDtos[0]));

        [Fact] public async Task GetById_InvalidId_Test() => await ExecuteAsync(async (playlistService, context) => await playlistService
            .Invoking(service => service.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));

        [Fact] public async Task Add_Test() => await ExecuteAsync(async (playlistService, context) =>
        {
            PlaylistDto result = await playlistService.AddAsync(_newPlaylistDto);
            context.Playlists.Find(result.Id).Should().BeEquivalentTo(_newPlaylist, options => options.Excluding(playlist => playlist.Id).Excluding(playlist => playlist.CreatedAt).Excluding(playlist => playlist.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateById_ValidId_Test() => await ExecuteAsync(async (playlistService, context) =>
        {
            Playlist updatedPlaylist = _playlists[0];
            updatedPlaylist.Title = _playlists[1].Title;
            PlaylistDto updatedPlaylistDto = updatedPlaylist.ToDto(_mapper);
            PlaylistDto result = await playlistService.UpdateByIdAsync(_playlistDtos[1], ValidPlaylistId);
            result.Should().BeEquivalentTo(updatedPlaylistDto, options => options.Excluding(playlist => playlist.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateById_InvalidId_Test() => await ExecuteAsync(async (playlistService, context) => await playlistService
            .Invoking(service => service.UpdateByIdAsync(_playlistDtos[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));

        [Fact] public async Task DeleteById_ValidId_Test() => await ExecuteAsync(async (playlistService, context) =>
        {
            PlaylistDto result = await playlistService.DeleteByIdAsync(ValidPlaylistId);
            result.Should().BeEquivalentTo(_playlistDtos[0], options => options.Excluding(playlist => playlist.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteById_InvalidId_Test() => await ExecuteAsync(async (playlistService, context) => await playlistService
            .Invoking(service => service.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId)));
    }
}