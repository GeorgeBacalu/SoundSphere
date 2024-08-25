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
using static SoundSphere.Test.Mocks.ArtistMock;
using static SoundSphere.Test.Mocks.SongMock;

namespace SoundSphere.Test.Integration.Services
{
    public class SongServiceIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;
        private readonly IMapper _mapper;

        public SongServiceIntegrationTest(DbFixture dbFixture) => (_dbFixture, _mapper) = (dbFixture, new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper());

        private async Task ExecuteAsync(Func<SongService, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var songService = new SongService(new SongRepository(context), new AlbumRepository(context), new ArtistRepository(context), _mapper);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await _dbFixture.TrackAndAddAsync(context, _albums);
            await _dbFixture.TrackAndAddAsync(context, _artists);
            await _dbFixture.TrackAndAddAsync(context, _songs);
            await action(songService, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedSongs() => await ExecuteAsync(async (songService, context) => (await songService.GetAllAsync(_songPayload)).Should().BeEquivalentTo(_songDtosPagination));

        [Fact] public async Task GetByIdAsync_ShouldReturnSong_WhenSongIdIsValid() => await ExecuteAsync(async (songService, context) => (await songService.GetByIdAsync(ValidSongId)).Should().BeEquivalentTo(_songDtos[0]));

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenSongIdIsInvalid() => await ExecuteAsync(async (songService, context) => await songService
            .Invoking(service => service.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));

        [Fact] public async Task AddAsync_ShouldAddNewSong_WhenSongDtoIsValid() => await ExecuteAsync(async (songService, context) =>
        {
            SongDto result = await songService.AddAsync(_newSongDto);
            context.Songs.Find(result.Id).Should().BeEquivalentTo(_newSong, options => options.Excluding(song => song.Id).Excluding(song => song.CreatedAt).Excluding(song => song.UpdatedAt).Excluding(song => song.Artists));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateSong_WhenSongIdIsValid() => await ExecuteAsync(async (songService, context) =>
        {
            Song updatedSong = _songs[0];
            updatedSong.Title = _songs[1].Title;
            updatedSong.ImageUrl = _songs[1].ImageUrl;
            updatedSong.Genre = _songs[1].Genre;
            updatedSong.ReleaseDate = _songs[1].ReleaseDate;
            updatedSong.DurationSeconds = _songs[1].DurationSeconds;
            updatedSong.Album = _songs[1].Album;
            updatedSong.Artists = _songs[1].Artists;
            updatedSong.SimilarSongs = _songs[1].SimilarSongs;
            SongDto updatedSongDto = updatedSong.ToDto(_mapper);
            SongDto result = await songService.UpdateByIdAsync(_songDtos[1], ValidSongId);
            result.Should().BeEquivalentTo(updatedSongDto, options => options.Excluding(song => song.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdatUpdateByIdAsync_ShouldThrowException_WhenSongIdIsInvalideById_InvalidId_Test() => await ExecuteAsync(async (songService, context) => await songService
            .Invoking(service => service.UpdateByIdAsync(_songDtos[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteSong_WhenSongIdIsValid() => await ExecuteAsync(async (songService, context) =>
        {
            SongDto result = await songService.DeleteByIdAsync(ValidSongId);
            result.Should().BeEquivalentTo(_songDtos[0], options => options.Excluding(song => song.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenSongIdIsInvalid() => await ExecuteAsync(async (songService, context) => await songService
            .Invoking(service => service.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));
    }
}