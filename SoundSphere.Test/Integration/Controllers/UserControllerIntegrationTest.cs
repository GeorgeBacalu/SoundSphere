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
using static SoundSphere.Test.Mocks.UserMock;
using static System.Net.HttpStatusCode;

namespace SoundSphere.Test.Integration.Controllers
{
    public class UserControllerIntegrationTest : IDisposable
    {
        private readonly DbFixture _dbFixture;
        private readonly CustomWebAppFactory _factory;
        private readonly HttpClient _httpClient;

        public UserControllerIntegrationTest() { _dbFixture = new DbFixture(); _factory = new(_dbFixture); _httpClient = _factory.CreateClient(); }

        public void Dispose() { _factory.Dispose(); _httpClient.Dispose(); }

        private async Task ExecuteAsync(Func<Task> action)
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Users.RemoveRange(context.Users);
            await context.Users.AddRangeAsync(_users);
            await context.SaveChangesAsync();
            await action();
            context.Users.RemoveRange(context.Users);
            await context.SaveChangesAsync();
        }

        [Fact] public async Task GetAll_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PostAsync($"{ApiUser}/get", new StringContent(SerializeObject(_userPayload), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<List<UserDto>>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_userDtos);
        });

        [Fact] public async Task GetById_ValidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiUser}/{ValidUserId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_userDtos[0]);
        });

        [Fact] public async Task GetById_InvalidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiUser}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(UserNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task Add_Test() => await ExecuteAsync(async () =>
        {
            var addResponse = await _httpClient.PostAsync(ApiUser, new StringContent(SerializeObject(_newUserDto), Encoding.UTF8, MediaTypeNames.Application.Json));
            addResponse.Should().NotBeNull();
            addResponse.StatusCode.Should().Be(Created);
            var addResponseBody = DeserializeObject<UserDto>(await addResponse.Content.ReadAsStringAsync());
            addResponseBody.Should().BeEquivalentTo(_newUserDto, options => options.Excluding(user => user.Id).Excluding(user => user.CreatedAt).Excluding(user => user.UpdatedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiUser}/{addResponseBody?.Id}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<UserDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(addResponseBody);
        });

        [Fact] public async Task UpdateById_ValidId_Test() => await ExecuteAsync(async () =>
        {
            UserDto updatedUserDto = _userDtos[0];
            updatedUserDto.Name = _newUserDto.Name;
            updatedUserDto.Email = _newUserDto.Email;
            updatedUserDto.Phone = _newUserDto.Phone;
            updatedUserDto.Address = _userDtos[1].Address;
            updatedUserDto.Birthday = _userDtos[1].Birthday;
            updatedUserDto.ImageUrl = _userDtos[1].ImageUrl;
            updatedUserDto.Role = _userDtos[1].Role;
            var updateResponse = await _httpClient.PutAsync($"{ApiUser}/{ValidUserId}", new StringContent(SerializeObject(updatedUserDto), Encoding.UTF8, MediaTypeNames.Application.Json));
            updateResponse.Should().NotBeNull();
            updateResponse.StatusCode.Should().Be(OK);
            var updateResponseBody = DeserializeObject<UserDto>(await updateResponse.Content.ReadAsStringAsync());
            updateResponseBody.Should().BeEquivalentTo(updatedUserDto, options => options.Excluding(user => user.UpdatedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiUser}/{ValidUserId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<UserDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(updatedUserDto, options => options.Excluding(user => user.UpdatedAt));
        });

        [Fact] public async Task UpdateById_InvalidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PutAsync($"{ApiUser}/{InvalidId}", new StringContent(SerializeObject(_userDtos[1]), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(UserNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteById_ValidId_Test() => await ExecuteAsync(async () =>
        {
            UserDto deletedUserDto = _userDtos[0];
            deletedUserDto.DeletedAt = DateTime.UtcNow;
            var deleteResponse = await _httpClient.DeleteAsync($"{ApiUser}/{ValidUserId}");
            deleteResponse.Should().NotBeNull();
            deleteResponse.StatusCode.Should().Be(OK);
            var deleteResponseBody = DeserializeObject<UserDto>(await deleteResponse.Content.ReadAsStringAsync());
            deleteResponseBody.Should().BeEquivalentTo(deletedUserDto, options => options.Excluding(user => user.DeletedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiUser}/{ValidUserId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(NotFound);
            var getResponseBody = DeserializeObject<ProblemDetails>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(UserNotFound, ValidUserId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteById_InvalidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.DeleteAsync($"{ApiUser}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(UserNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });
    }
}