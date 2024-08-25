﻿using FluentAssertions;
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
using static SoundSphere.Test.Mocks.ArtistMock;
using static SoundSphere.Test.Mocks.UserMock;
using static System.Net.HttpStatusCode;

namespace SoundSphere.Test.Integration.Controllers
{
    public class ArtistControllerIntegrationTest : IDisposable
    {
        private readonly DbFixture _dbFixture;
        private readonly CustomWebAppFactory _factory;
        private readonly HttpClient _httpClient;

        public ArtistControllerIntegrationTest() { _dbFixture = new DbFixture(); _factory = new(_dbFixture); _httpClient = _factory.CreateClient(); }

        public void Dispose() { _factory.Dispose(); _httpClient.Dispose(); }

        private async Task ExecuteAsync(Func<Task> action)
        {
            using var scope = _factory.Services.CreateScope();
            var _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.ArtistPairs.RemoveRange(context.ArtistPairs);
            context.Artists.RemoveRange(context.Artists);
            context.UsersArtists.RemoveRange(context.UsersArtists);
            context.Users.RemoveRange(context.Users);
            await _dbFixture.TrackAndAddAsync(context, _artists);
            await _dbFixture.TrackAndAddAsync(context, _users);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userService.GenerateToken(_users[0]));
            await action();
            context.ArtistPairs.RemoveRange(context.ArtistPairs);
            context.Artists.RemoveRange(context.Artists);
            context.UsersArtists.RemoveRange(context.UsersArtists);
            context.Users.RemoveRange(context.Users);
            await context.SaveChangesAsync();
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedArtists() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PostAsync($"{ApiArtist}/get", new StringContent(SerializeObject(_artistPayload), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<List<ArtistDto>>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_artistDtosPagination, options => options.Excluding(artist => artist.SimilarArtistsIds));
        });

        [Fact] public async Task GetByIdAsync_ShouldReturnArtist_WhenArtistIdIsValid() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiArtist}/{ValidArtistId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<ArtistDto>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_artistDtos[0], options => options.Excluding(artist => artist.SimilarArtistsIds));
        });

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiArtist}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(ArtistNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task AddAsync_ShouldAddNewArtist_WhenArtistDtoIsValid() => await ExecuteAsync(async () =>
        {
            var addResponse = await _httpClient.PostAsync(ApiArtist, new StringContent(SerializeObject(_newArtistDto), Encoding.UTF8, MediaTypeNames.Application.Json));
            addResponse.Should().NotBeNull();
            addResponse.StatusCode.Should().Be(Created);
            var addResponseBody = DeserializeObject<ArtistDto>(await addResponse.Content.ReadAsStringAsync());
            addResponseBody.Should().BeEquivalentTo(_newArtistDto, options => options.Excluding(artist => artist.Id).Excluding(artist => artist.CreatedAt).Excluding(artist => artist.UpdatedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiArtist}/{addResponseBody?.Id}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<ArtistDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(addResponseBody);
        });

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateArtist_WhenArtistIdIsValid() => await ExecuteAsync(async () =>
        {
            ArtistDto updatedArtistDto = _artistDtos[0];
            updatedArtistDto.Name = _artistDtos[1].Name;
            updatedArtistDto.ImageUrl = _artistDtos[1].ImageUrl;
            updatedArtistDto.Bio = _artistDtos[1].Bio;
            updatedArtistDto.SimilarArtistsIds = _artistDtos[1].SimilarArtistsIds;
            var updateResponse = await _httpClient.PutAsync($"{ApiArtist}/{ValidArtistId}", new StringContent(SerializeObject(_artistDtos[1]), Encoding.UTF8, MediaTypeNames.Application.Json));
            updateResponse.Should().NotBeNull();
            updateResponse.StatusCode.Should().Be(OK);
            var updateResponseBody = DeserializeObject<ArtistDto>(await updateResponse.Content.ReadAsStringAsync());
            updateResponseBody.Should().BeEquivalentTo(updatedArtistDto, options => options.Excluding(artist => artist.UpdatedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiArtist}/{ValidArtistId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<ArtistDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(updatedArtistDto, options => options.Excluding(artist => artist.UpdatedAt));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PutAsync($"{ApiArtist}/{InvalidId}", new StringContent(SerializeObject(_artistDtos[1]), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(ArtistNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteArtist_WhenArtistIdIsValid() => await ExecuteAsync(async () =>
        {
            ArtistDto deletedArtistDto = _artistDtos[0];
            deletedArtistDto.DeletedAt = DateTime.UtcNow;
            var deleteResponse = await _httpClient.DeleteAsync($"{ApiArtist}/{ValidArtistId}");
            deleteResponse.Should().NotBeNull();
            deleteResponse.StatusCode.Should().Be(OK);
            var deleteResponseBody = DeserializeObject<ArtistDto>(await deleteResponse.Content.ReadAsStringAsync());
            deleteResponseBody.Should().BeEquivalentTo(deletedArtistDto, options => options.Excluding(artist => artist.DeletedAt).Excluding(artist => artist.SimilarArtistsIds));

            var getResponse = await _httpClient.GetAsync($"{ApiArtist}/{ValidArtistId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(NotFound);
            var getResponseBody = DeserializeObject<ProblemDetails>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(ArtistNotFound, ValidArtistId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.DeleteAsync($"{ApiArtist}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(ArtistNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });
    }
}