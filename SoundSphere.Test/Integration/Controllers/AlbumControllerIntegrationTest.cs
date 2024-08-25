using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Context;
using SoundSphere.Database.Dtos.Common;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using static Newtonsoft.Json.JsonConvert;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.AlbumMock;
using static SoundSphere.Test.Mocks.UserMock;
using static System.Net.HttpStatusCode;

namespace SoundSphere.Test.Integration.Controllers
{
    public class AlbumControllerIntegrationTest : IDisposable
    {
        private readonly DbFixture _dbFixture;
        private readonly CustomWebAppFactory _factory;
        private readonly HttpClient _httpClient;

        public AlbumControllerIntegrationTest() { _dbFixture = new DbFixture(); _factory = new(_dbFixture); _httpClient = _factory.CreateClient(); }

        public void Dispose() { _factory.Dispose(); _httpClient.Dispose(); }

        private async Task ExecuteAsync(Func<Task> action)
        {
            using var scope = _factory.Services.CreateScope();
            var _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.AlbumPairs.RemoveRange(context.AlbumPairs);
            context.Albums.RemoveRange(context.Albums);
            context.UsersArtists.RemoveRange(context.UsersArtists);
            context.Users.RemoveRange(context.Users);
            await _dbFixture.TrackAndAddAsync(context, _albums);
            await _dbFixture.TrackAndAddAsync(context, _users);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userService.GenerateToken(_users[0]));
            await action();
            context.AlbumPairs.RemoveRange(context.AlbumPairs);
            context.Albums.RemoveRange(context.Albums);
            context.UsersArtists.RemoveRange(context.UsersArtists);
            context.Users.RemoveRange(context.Users);
            await context.SaveChangesAsync();
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedAlbums() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PostAsync($"{ApiAlbum}/get", new StringContent(SerializeObject(_albumPayload), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<List<AlbumDto>>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_albumDtosPagination, options => options.Excluding(album => album.SimilarAlbumsIds));
        });

        [Fact] public async Task GetByIdAsync_ShouldReturnAlbum_WhenAlbumIdIsValid() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiAlbum}/{ValidAlbumId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<AlbumDto>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_albumDtos[0], options => options.Excluding(album => album.SimilarAlbumsIds));
        });

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenAlbumIdIsInvalid() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiAlbum}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(AlbumNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task AddAsync_ShouldAddNewAlbum_WhenAlbumDtoIsValid() => await ExecuteAsync(async () =>
        {
            var addResponse = await _httpClient.PostAsync(ApiAlbum, new StringContent(SerializeObject(_newAlbumDto), Encoding.UTF8, MediaTypeNames.Application.Json));
            addResponse.Should().NotBeNull();
            addResponse.StatusCode.Should().Be(Created);
            var addResponseBody = DeserializeObject<AlbumDto>(await addResponse.Content.ReadAsStringAsync());
            addResponseBody.Should().BeEquivalentTo(_newAlbumDto, options => options.Excluding(album => album.Id).Excluding(album => album.CreatedAt).Excluding(album => album.UpdatedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiAlbum}/{addResponseBody?.Id}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<AlbumDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(addResponseBody);
        });

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateAlbum_WhenAlbumIdIsValid() => await ExecuteAsync(async () =>
        {
            AlbumDto updatedAlbumDto = _albumDtos[0];
            updatedAlbumDto.Title = _albumDtos[1].Title;
            updatedAlbumDto.ImageUrl = _albumDtos[1].ImageUrl;
            updatedAlbumDto.ReleaseDate = _albumDtos[1].ReleaseDate;
            updatedAlbumDto.SimilarAlbumsIds = _albumDtos[1].SimilarAlbumsIds;
            var updateResponse = await _httpClient.PutAsync($"{ApiAlbum}/{ValidAlbumId}", new StringContent(SerializeObject(_albumDtos[1]), Encoding.UTF8, MediaTypeNames.Application.Json));
            updateResponse.Should().NotBeNull();
            updateResponse.StatusCode.Should().Be(OK);
            var updateResponseBody = DeserializeObject<AlbumDto>(await updateResponse.Content.ReadAsStringAsync());
            updateResponseBody.Should().BeEquivalentTo(updatedAlbumDto, options => options.Excluding(album => album.UpdatedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiAlbum}/{ValidAlbumId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<AlbumDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(updatedAlbumDto, options => options.Excluding(album => album.UpdatedAt));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenAlbumIdIsInvalid() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PutAsync($"{ApiAlbum}/{InvalidId}", new StringContent(SerializeObject(_albumDtos[1]), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(AlbumNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteAlbum_WhenAlbumIdIsValid() => await ExecuteAsync(async () =>
        {
            AlbumDto deletedAlbumDto = _albumDtos[0];
            deletedAlbumDto.DeletedAt = DateTime.UtcNow;
            var deleteResponse = await _httpClient.DeleteAsync($"{ApiAlbum}/{ValidAlbumId}");
            deleteResponse.Should().NotBeNull();
            deleteResponse.StatusCode.Should().Be(OK);
            var deleteResponseBody = DeserializeObject<AlbumDto>(await deleteResponse.Content.ReadAsStringAsync());
            deleteResponseBody.Should().BeEquivalentTo(deletedAlbumDto, options => options.Excluding(album => album.DeletedAt).Excluding(album => album.SimilarAlbumsIds));

            var getResponse = await _httpClient.GetAsync($"{ApiAlbum}/{ValidAlbumId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(NotFound);
            var getResponseBody = DeserializeObject<ProblemDetails>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(AlbumNotFound, ValidAlbumId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenAlbumIdIsInvalid() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.DeleteAsync($"{ApiAlbum}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(AlbumNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });
    }
}