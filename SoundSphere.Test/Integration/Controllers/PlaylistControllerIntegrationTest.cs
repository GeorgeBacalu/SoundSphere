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
using static SoundSphere.Test.Mocks.PlaylistMock;
using static SoundSphere.Test.Mocks.SongMock;
using static SoundSphere.Test.Mocks.UserMock;
using static System.Net.HttpStatusCode;

namespace SoundSphere.Test.Integration.Controllers
{
    public class PlaylistControllerIntegrationTest : IDisposable
    {
        private readonly DbFixture _dbFixture;
        private readonly CustomWebAppFactory _factory;
        private readonly HttpClient _httpClient;

        public PlaylistControllerIntegrationTest() { _dbFixture = new DbFixture(); _factory = new(_dbFixture); _httpClient = _factory.CreateClient(); }

        public void Dispose() { _factory.Dispose(); _httpClient.Dispose(); }

        private async Task ExecuteAsync(Func<Task> action)
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.ChangeTracker.Clear();
            context.Albums.RemoveRange(context.Albums);
            context.Songs.RemoveRange(context.Songs);
            context.Users.RemoveRange(context.Users);
            context.Playlists.RemoveRange(context.Playlists);
            await _dbFixture.TrackAndAddAsync(context, _albums);
            await _dbFixture.TrackAndAddAsync(context, _songs);
            await _dbFixture.TrackAndAddAsync(context, _users);
            await _dbFixture.TrackAndAddAsync(context, _playlists);
            await context.SaveChangesAsync();
            await action();
            context.Albums.RemoveRange(context.Albums);
            context.Songs.RemoveRange(context.Songs);
            context.Users.RemoveRange(context.Users);
            context.Playlists.RemoveRange(context.Playlists);
            await context.SaveChangesAsync();
        }

        [Fact] public async Task GetAll_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PostAsync($"{ApiPlaylist}/get", new StringContent(SerializeObject(_playlistPayload), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<List<PlaylistDto>>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_playlistDtosPagination, options => options.Excluding(playlist => playlist.SongsIds));
        });

        [Fact] public async Task GetById_ValidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiPlaylist}/{ValidPlaylistId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<PlaylistDto>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_playlistDtos[0], options => options.Excluding(playlist => playlist.SongsIds));
        });

        [Fact] public async Task GetById_InvalidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiPlaylist}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(PlaylistNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task Add_Test() => await ExecuteAsync(async () =>
        {
            var addResponse = await _httpClient.PostAsync(ApiPlaylist, new StringContent(SerializeObject(_newPlaylistDto), Encoding.UTF8, MediaTypeNames.Application.Json));
            addResponse.Should().NotBeNull();
            addResponse.StatusCode.Should().Be(Created);
            var addResponseBody = DeserializeObject<PlaylistDto>(await addResponse.Content.ReadAsStringAsync());
            addResponseBody.Should().BeEquivalentTo(_newPlaylistDto, options => options.Excluding(playlist => playlist.Id).Excluding(playlist => playlist.CreatedAt).Excluding(playlist => playlist.UpdatedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiPlaylist}/{addResponseBody?.Id}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<PlaylistDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(addResponseBody);
        });

        [Fact] public async Task UpdateById_ValidId_Test() => await ExecuteAsync(async () =>
        {
            PlaylistDto updatedPlaylistDto = _playlistDtos[0];
            updatedPlaylistDto.Title = _playlistDtos[1].Title;
            var updateResponse = await _httpClient.PutAsync($"{ApiPlaylist}/{ValidPlaylistId}", new StringContent(SerializeObject(_playlistDtos[1]), Encoding.UTF8, MediaTypeNames.Application.Json));
            updateResponse.Should().NotBeNull();
            updateResponse.StatusCode.Should().Be(OK);
            var updateResponseBody = DeserializeObject<PlaylistDto>(await updateResponse.Content.ReadAsStringAsync());
            updateResponseBody.Should().BeEquivalentTo(updatedPlaylistDto, options => options.Excluding(playlist => playlist.UpdatedAt).Excluding(playlist => playlist.SongsIds));

            var getResponse = await _httpClient.GetAsync($"{ApiPlaylist}/{ValidPlaylistId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<PlaylistDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(updatedPlaylistDto, options => options.Excluding(playlist => playlist.UpdatedAt).Excluding(playlist => playlist.SongsIds));
        });

        [Fact] public async Task UpdateById_InvalidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PutAsync($"{ApiPlaylist}/{InvalidId}", new StringContent(SerializeObject(_playlistDtos[1]), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(PlaylistNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteById_ValidId_Test() => await ExecuteAsync(async () =>
        {
            PlaylistDto deletedPlaylistDto = _playlistDtos[0];
            deletedPlaylistDto.DeletedAt = DateTime.UtcNow;
            var deleteResponse = await _httpClient.DeleteAsync($"{ApiPlaylist}/{ValidPlaylistId}");
            deleteResponse.Should().NotBeNull();
            deleteResponse.StatusCode.Should().Be(OK);
            var deleteResponseBody = DeserializeObject<PlaylistDto>(await deleteResponse.Content.ReadAsStringAsync());
            deleteResponseBody.Should().BeEquivalentTo(deletedPlaylistDto, options => options.Excluding(playlist => playlist.DeletedAt).Excluding(playlist => playlist.SongsIds));

            var getResponse = await _httpClient.GetAsync($"{ApiPlaylist}/{ValidPlaylistId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(NotFound);
            var getResponseBody = DeserializeObject<ProblemDetails>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(PlaylistNotFound, ValidPlaylistId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteById_InvalidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.DeleteAsync($"{ApiPlaylist}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(PlaylistNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });
    }
}