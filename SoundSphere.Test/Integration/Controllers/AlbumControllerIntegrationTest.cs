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
using static System.Net.HttpStatusCode;

namespace SoundSphere.Test.Integration.Controllers
{
    public class AlbumControllerIntegrationTest : IDisposable
    {
        private readonly DbFixture _dbFixture;
        private readonly CustomWebAppFactory _factory;
        private readonly HttpClient _httpClient;
        private readonly List<Album> _albums = GetAlbums();
        private readonly AlbumDto _albumDto1 = GetAlbumDto1();
        private readonly AlbumDto _albumDto2 = GetAlbumDto2();
        private readonly AlbumDto _newAlbumDto = GetNewAlbumDto();
        private readonly List<AlbumDto> _albumDtos = GetAlbumDtos();

        public AlbumControllerIntegrationTest() { _dbFixture = new DbFixture(); _factory = new(_dbFixture); _httpClient = _factory.CreateClient(); }

        public void Dispose() { _factory.Dispose(); _httpClient.Dispose(); }

        private async Task Execute(Func<Task> action)
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.AlbumPairs.RemoveRange(context.AlbumPairs);
            context.Albums.RemoveRange(context.Albums);
            await context.SaveChangesAsync();
            await context.Albums.AddRangeAsync(_albums);
            await context.SaveChangesAsync();
            await action();
            context.AlbumPairs.RemoveRange(context.AlbumPairs);
            context.Albums.RemoveRange(context.Albums);
            await context.SaveChangesAsync();
        }

        [Fact] public async Task GetAll_Test() => await Execute(async () =>
        {
            var response = await _httpClient.GetAsync(ApiAlbum);
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<List<AlbumDto>>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_albumDtos);
        });

        [Fact] public async Task GetById_ValidId_Test() => await Execute(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiAlbum}/{ValidAlbumId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<AlbumDto>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_albumDto1);
        });

        [Fact] public async Task GetById_InvalidId_Test() => await Execute(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiAlbum}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(AlbumNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task Add_Test() => await Execute(async () =>
        {
            var addResponse = await _httpClient.PostAsync(ApiAlbum, new StringContent(SerializeObject(_newAlbumDto), Encoding.UTF8, MediaTypeNames.Application.Json));
            addResponse.Should().NotBeNull();
            addResponse.StatusCode.Should().Be(Created);
            var addResponseBody = DeserializeObject<AlbumDto>(await addResponse.Content.ReadAsStringAsync());
            addResponseBody.Should().BeEquivalentTo(_newAlbumDto, options => options.Excluding(album => album.Id).Excluding(album => album.CreatedAt).Excluding(album => album.UpdatedAt));
            
            var getAllResponse = await _httpClient.GetAsync(ApiAlbum);
            getAllResponse.Should().NotBeNull();
            getAllResponse.StatusCode.Should().Be(OK);
            var getAllResponseBody = DeserializeObject<List<AlbumDto>>(await getAllResponse.Content.ReadAsStringAsync());
            getAllResponseBody.Should().ContainEquivalentOf(addResponseBody, options => options.Excluding(album => album.CreatedAt).Excluding(album => album.UpdatedAt));
        });

        [Fact] public async Task UpdateById_ValidId_Test() => await Execute(async () =>
        {
            AlbumDto updatedAlbumDto = _albumDto1;
            updatedAlbumDto.Title = _albumDto2.Title;
            updatedAlbumDto.ImageUrl = _albumDto2.ImageUrl;
            updatedAlbumDto.ReleaseDate = _albumDto2.ReleaseDate;
            updatedAlbumDto.SimilarAlbumsIds = _albumDto2.SimilarAlbumsIds;
            var updateResponse = await _httpClient.PutAsync($"{ApiAlbum}/{ValidAlbumId}", new StringContent(SerializeObject(_albumDto2), Encoding.UTF8, MediaTypeNames.Application.Json));
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

        [Fact] public async Task UpdateById_InvalidId_Test() => await Execute(async () =>
        {
            var response = await _httpClient.PutAsync($"{ApiAlbum}/{InvalidId}", new StringContent(SerializeObject(_albumDto2), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(AlbumNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteById_ValidId_Test() => await Execute(async () =>
        {
            AlbumDto deletedAlbumDto = _albumDto1;
            deletedAlbumDto.DeletedAt = DateTime.UtcNow;
            var deleteResponse = await _httpClient.DeleteAsync($"{ApiAlbum}/{ValidAlbumId}");
            deleteResponse.Should().NotBeNull();
            deleteResponse.StatusCode.Should().Be(OK);
            var deleteResponseBody = DeserializeObject<AlbumDto>(await deleteResponse.Content.ReadAsStringAsync());
            deleteResponseBody.Should().BeEquivalentTo(deletedAlbumDto, options => options.Excluding(album => album.DeletedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiAlbum}/{ValidAlbumId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(NotFound);
            var getResponseBody = DeserializeObject<ProblemDetails>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(AlbumNotFound, ValidAlbumId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteById_InvalidId_Test() => await Execute(async () =>
        {
            var response = await _httpClient.DeleteAsync($"{ApiAlbum}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(AlbumNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });
    }
}