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

        private void Execute(Action<SongService, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var songService = new SongService(new SongRepository(context), new AlbumRepository(context), new ArtistRepository(context), _mapper);
            using var transaction = context.Database.BeginTransaction();
            _dbFixture.TrackAndAddEntities(context, _albums);
            _dbFixture.TrackAndAddEntities(context, _artists);
            _dbFixture.TrackAndAddEntities(context, _songs);
            action(songService, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((songService, context) => songService.GetAll(_songPayload).Should().BeEquivalentTo(_songDtosPagination));

        [Fact] public void GetById_ValidId_Test() => Execute((songService, context) => songService.GetById(ValidSongId).Should().BeEquivalentTo(_songDtos[0]));

        [Fact] public void GetById_InvalidId_Test() => Execute((songService, context) => songService
            .Invoking(service => service.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((songService, context) =>
        {
            SongDto result = songService.Add(_newSongDto);
            context.Songs.Find(result.Id).Should().BeEquivalentTo(_newSong, options => options.Excluding(song => song.Id).Excluding(song => song.CreatedAt).Excluding(song => song.UpdatedAt).Excluding(song => song.Artists));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((songService, context) =>
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
            SongDto result = songService.UpdateById(_songDtos[1], ValidSongId);
            result.Should().BeEquivalentTo(updatedSongDto, options => options.Excluding(song => song.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((songService, context) => songService
            .Invoking(service => service.UpdateById(_songDtos[1], InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((songService, context) =>
        {
            SongDto result = songService.DeleteById(ValidSongId);
            result.Should().BeEquivalentTo(_songDtos[0], options => options.Excluding(song => song.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((songService, context) => songService
            .Invoking(service => service.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(SongNotFound, InvalidId)));
    }
}