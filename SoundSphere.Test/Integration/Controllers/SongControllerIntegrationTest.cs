using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SoundSphere.Database.Context;
using SoundSphere.Database.Dtos.Common;
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

        public SongControllerIntegrationTest() { _dbFixture = new DbFixture(); _factory = new(_dbFixture); _httpClient = _factory.CreateClient(); }

        public void Dispose() { _factory.Dispose(); _httpClient.Dispose(); }

        private async Task ExecuteAsync(Func<Task> action)
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Albums.RemoveRange(context.Albums);
            context.Artists.RemoveRange(context.Artists);
            context.SongPairs.RemoveRange(context.SongPairs);
            context.Songs.RemoveRange(context.Songs);
            await _dbFixture.TrackAndAddAsync(context, _albums);
            await _dbFixture.TrackAndAddAsync(context, _artists);
            await _dbFixture.TrackAndAddAsync(context, _songs);
            await context.SaveChangesAsync();
            await action();
            context.Albums.RemoveRange(context.Albums);
            context.Artists.RemoveRange(context.Artists);
            context.SongPairs.RemoveRange(context.SongPairs);
            context.Songs.RemoveRange(context.Songs);
            await context.SaveChangesAsync();
        }

        [Fact] public async Task GetAll_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PostAsync($"{ApiSong}/get", new StringContent(SerializeObject(_songPayload), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<List<SongDto>>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_songDtosPagination, options => options.Excluding(song => song.ArtistsIds).Excluding(song => song.SimilarSongsIds));
        });

        [Fact] public async Task GetById_ValidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiSong}/{ValidSongId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<SongDto>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_songDtos[0], options => options.Excluding(song => song.ArtistsIds).Excluding(song => song.SimilarSongsIds));
        });

        [Fact] public async Task GetById_InvalidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiSong}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(SongNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task Add_Test() => await ExecuteAsync(async () =>
        {
            var addResponse = await _httpClient.PostAsync(ApiSong, new StringContent(SerializeObject(_newSongDto), Encoding.UTF8, MediaTypeNames.Application.Json));
            addResponse.Should().NotBeNull();
            addResponse.StatusCode.Should().Be(Created);
            var addResponseBody = DeserializeObject<SongDto>(await addResponse.Content.ReadAsStringAsync());
            addResponseBody.Should().BeEquivalentTo(_newSongDto, options => options.Excluding(song => song.Id).Excluding(song => song.CreatedAt).Excluding(song => song.UpdatedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiSong}/{addResponseBody?.Id}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<SongDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(addResponseBody);
        });

        [Fact] public async Task UpdateById_ValidId_Test() => await ExecuteAsync(async () =>
        {
            SongDto updatedSongDto = _songDtos[0];
            updatedSongDto.Title = _songDtos[1].Title;
            updatedSongDto.ImageUrl = _songDtos[1].ImageUrl;
            updatedSongDto.Genre = _songDtos[1].Genre;
            updatedSongDto.ReleaseDate = _songDtos[1].ReleaseDate;
            updatedSongDto.DurationSeconds = _songDtos[1].DurationSeconds;
            updatedSongDto.AlbumId = _songDtos[1].AlbumId;
            updatedSongDto.ArtistsIds = _songDtos[1].ArtistsIds;
            updatedSongDto.SimilarSongsIds = _songDtos[1].SimilarSongsIds;
            var updateResponse = await _httpClient.PutAsync($"{ApiSong}/{ValidSongId}", new StringContent(SerializeObject(_songDtos[1]), Encoding.UTF8, MediaTypeNames.Application.Json));
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

        [Fact] public async Task UpdateById_InvalidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PutAsync($"{ApiSong}/{InvalidId}", new StringContent(SerializeObject(_songDtos[1]), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(SongNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteById_ValidId_Test() => await ExecuteAsync(async () =>
        {
            SongDto deletedSongDto = _songDtos[0];
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

        [Fact] public async Task DeleteById_InvalidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.DeleteAsync($"{ApiSong}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(SongNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });
    }
}