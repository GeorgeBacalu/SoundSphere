using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SoundSphere.Database.Context;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using System.Net.Mime;
using System.Text;
using static Newtonsoft.Json.JsonConvert;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.AlbumMock;
using static SoundSphere.Test.Mocks.ArtistMock;
using static SoundSphere.Test.Mocks.SongMock;
using static System.Net.HttpStatusCode;

namespace SoundSphere.Test.Integration.Controllers
{
    public class SongControllerIntegrationTest : IDisposable
    {
        private readonly DbFixture _dbFixture;
        private readonly CustomWebAppFactory _factory;
        private readonly HttpClient _httpClient;
        private readonly List<Song> _songs = GetSongs();
        private readonly SongDto _songDto1 = GetSongDto1();
        private readonly SongDto _songDto2 = GetSongDto2();
        private readonly SongDto _newSongDto = GetNewSongDto();
        private readonly List<SongDto> _songDtos = GetSongDtos();
        private readonly List<Album> _albums = GetAlbums();
        private readonly List<Artist> _artists = GetArtists();

        public SongControllerIntegrationTest() { _dbFixture = new DbFixture(); _factory = new(_dbFixture); _httpClient = _factory.CreateClient(); }

        public void Dispose() { _factory.Dispose(); _httpClient.Dispose(); }

        private async Task Execute(Func<Task> action)
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Albums.RemoveRange(context.Albums);
            context.Artists.RemoveRange(context.Artists);
            context.SongPairs.RemoveRange(context.SongPairs);
            context.Songs.RemoveRange(context.Songs);
            await context.SaveChangesAsync();
            _dbFixture.TrackAndAddEntities(context, _albums);
            _dbFixture.TrackAndAddEntities(context, _artists);
            _dbFixture.TrackAndAddEntities(context, _songs);
            await context.SaveChangesAsync();
            await action();
            context.Albums.RemoveRange(context.Albums);
            context.Artists.RemoveRange(context.Artists);
            context.SongPairs.RemoveRange(context.SongPairs);
            context.Songs.RemoveRange(context.Songs);
            await context.SaveChangesAsync();
        }

        [Fact] public async Task GetAll_Test() => await Execute(async () =>
        {
            var response = await _httpClient.GetAsync(ApiSong);
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<List<SongDto>>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_songDtos, options => options.Excluding(song => song.ArtistsIds).Excluding(song => song.SimilarSongsIds));
        });

        [Fact] public async Task GetById_ValidId_Test() => await Execute(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiSong}/{ValidSongId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<SongDto>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_songDto1, options => options.Excluding(song => song.ArtistsIds).Excluding(song => song.SimilarSongsIds));
        });

        [Fact] public async Task GetById_InvalidId_Test() => await Execute(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiSong}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(SongNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task Add_Test() => await Execute(async () =>
        {
            var addResponse = await _httpClient.PostAsync(ApiSong, new StringContent(SerializeObject(_newSongDto), Encoding.UTF8, MediaTypeNames.Application.Json));
            addResponse.Should().NotBeNull();
            addResponse.StatusCode.Should().Be(Created);
            var addResponseBody = DeserializeObject<SongDto>(await addResponse.Content.ReadAsStringAsync());
            addResponseBody.Should().BeEquivalentTo(_newSongDto, options => options.Excluding(song => song.Id).Excluding(song => song.CreatedAt).Excluding(song => song.UpdatedAt));

            var getAllResponse = await _httpClient.GetAsync(ApiSong);
            getAllResponse.Should().NotBeNull();
            getAllResponse.StatusCode.Should().Be(OK);
            var getAllResponseBody = DeserializeObject<List<SongDto>>(await getAllResponse.Content.ReadAsStringAsync());
            getAllResponseBody.Should().ContainEquivalentOf(addResponseBody, options => options.Excluding(song => song.CreatedAt).Excluding(song => song.UpdatedAt));
        });

        [Fact] public async Task UpdateById_ValidId_Test() => await Execute(async () =>
        {
            SongDto updatedSongDto = _songDto1;
            updatedSongDto.Title = _songDto2.Title;
            updatedSongDto.ImageUrl = _songDto2.ImageUrl;
            updatedSongDto.Genre = _songDto2.Genre;
            updatedSongDto.ReleaseDate = _songDto2.ReleaseDate;
            updatedSongDto.DurationSeconds = _songDto2.DurationSeconds;
            updatedSongDto.AlbumId = _songDto2.AlbumId;
            updatedSongDto.ArtistsIds = _songDto2.ArtistsIds;
            updatedSongDto.SimilarSongsIds = _songDto2.SimilarSongsIds;
            var updateResponse = await _httpClient.PutAsync($"{ApiSong}/{ValidSongId}", new StringContent(SerializeObject(_songDto2), Encoding.UTF8, MediaTypeNames.Application.Json));
            updateResponse.Should().NotBeNull();
            updateResponse.StatusCode.Should().Be(OK);
            var updateResponseBody = DeserializeObject<SongDto>(await updateResponse.Content.ReadAsStringAsync());
            updateResponseBody.Should().BeEquivalentTo(updatedSongDto, options => options.Excluding(song => song.UpdatedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiSong}/{ValidSongId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<SongDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(updatedSongDto, options => options.Excluding(song => song.UpdatedAt));
        });

        [Fact] public async Task UpdateById_InvalidId_Test() => await Execute(async () =>
        {
            var response = await _httpClient.PutAsync($"{ApiSong}/{InvalidId}", new StringContent(SerializeObject(_songDto2), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(SongNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteById_ValidId_Test() => await Execute(async () =>
        {
            SongDto deletedSongDto = _songDto1;
            deletedSongDto.DeletedAt = DateTime.UtcNow;
            var deleteResponse = await _httpClient.DeleteAsync($"{ApiSong}/{ValidSongId}");
            deleteResponse.Should().NotBeNull();
            deleteResponse.StatusCode.Should().Be(OK);
            var deleteResponseBody = DeserializeObject<SongDto>(await deleteResponse.Content.ReadAsStringAsync());
            deleteResponseBody.Should().BeEquivalentTo(deletedSongDto, options => options.Excluding(song => song.DeletedAt).Excluding(song => song.ArtistsIds).Excluding(song => song.SimilarSongsIds));

            var getResponse = await _httpClient.GetAsync($"{ApiSong}/{ValidSongId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(NotFound);
            var getResponseBody = DeserializeObject<ProblemDetails>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(SongNotFound, ValidSongId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteById_InvalidId_Test() => await Execute(async () =>
        {
            var response = await _httpClient.DeleteAsync($"{ApiSong}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(SongNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });
    }
}