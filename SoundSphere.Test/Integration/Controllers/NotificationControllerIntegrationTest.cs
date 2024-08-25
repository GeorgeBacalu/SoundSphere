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
using static SoundSphere.Test.Mocks.NotificationMock;
using static SoundSphere.Test.Mocks.UserMock;
using static System.Net.HttpStatusCode;

namespace SoundSphere.Test.Integration.Controllers
{
    public class NotificationControllerIntegrationTest : IDisposable
    {
        private readonly DbFixture _dbFixture;
        private readonly CustomWebAppFactory _factory;
        private readonly HttpClient _httpClient;

        public NotificationControllerIntegrationTest() { _dbFixture = new DbFixture(); _factory = new(_dbFixture); _httpClient = _factory.CreateClient(); }

        public void Dispose() { _factory.Dispose(); _httpClient.Dispose(); }

        private async Task ExecuteAsync(Func<Task> action)
        {
            using var scope = _factory.Services.CreateScope();
            var _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.ChangeTracker.Clear();
            context.Notifications.RemoveRange(context.Notifications);
            context.Users.RemoveRange(context.Users);
            await _dbFixture.TrackAndAddAsync(context, _users);
            await _dbFixture.TrackAndAddAsync(context, _notifications);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userService.GenerateToken(_users[0]));
            await action();
            context.Notifications.RemoveRange(context.Notifications);
            context.Users.RemoveRange(context.Users);
            await context.SaveChangesAsync();
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedNotifications() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PostAsync($"{ApiNotification}/get", new StringContent(SerializeObject(_notificationPayload), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<List<NotificationDto>>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_notificationDtosPagination);
        });

        [Fact] public async Task GetByIdAsync_ShouldReturnNotification_WhenNotificationIdIsValid() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiNotification}/{ValidNotificationId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<NotificationDto>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_notificationDtos[0]);
        });

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiNotification}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(NotificationNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task AddAsync_ShouldAddNewNotification_WhenNotificationDtoIsValid() => await ExecuteAsync(async () =>
        {
            var addResponse = await _httpClient.PostAsync(ApiNotification, new StringContent(SerializeObject(_newNotificationDto), Encoding.UTF8, MediaTypeNames.Application.Json));
            addResponse.Should().NotBeNull();
            addResponse.StatusCode.Should().Be(Created);
            var addResponseBody = DeserializeObject<NotificationDto>(await addResponse.Content.ReadAsStringAsync());
            addResponseBody.Should().BeEquivalentTo(_newNotificationDto, options => options.Excluding(notification => notification.Id).Excluding(notification => notification.CreatedAt).Excluding(notification => notification.UpdatedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiNotification}/{addResponseBody?.Id}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<NotificationDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(addResponseBody);
        });

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateNotification_WhenNotificationIdIsValid() => await ExecuteAsync(async () =>
        {
            NotificationDto updatedNotificationDto = _notificationDtos[0];
            updatedNotificationDto.Type = _notificationDtos[1].Type;
            updatedNotificationDto.Message = _notificationDtos[1].Message;
            updatedNotificationDto.IsRead = _notificationDtos[1].IsRead;
            var updateResponse = await _httpClient.PutAsync($"{ApiNotification}/{ValidNotificationId}", new StringContent(SerializeObject(_notificationDtos[1]), Encoding.UTF8, MediaTypeNames.Application.Json));
            updateResponse.Should().NotBeNull();
            updateResponse.StatusCode.Should().Be(OK);
            var updateResponseBody = DeserializeObject<NotificationDto>(await updateResponse.Content.ReadAsStringAsync());
            updateResponseBody.Should().BeEquivalentTo(updatedNotificationDto, options => options.Excluding(notification => notification.UpdatedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiNotification}/{ValidNotificationId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<NotificationDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(updatedNotificationDto, options => options.Excluding(notification => notification.UpdatedAt));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PutAsync($"{ApiNotification}/{InvalidId}", new StringContent(SerializeObject(_notificationDtos[1]), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(NotificationNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteNotification_WhenNotificationIdIsValid() => await ExecuteAsync(async () =>
        {
            NotificationDto deletedNotificationDto = _notificationDtos[0];
            deletedNotificationDto.DeletedAt = DateTime.UtcNow;
            var deleteResponse = await _httpClient.DeleteAsync($"{ApiNotification}/{ValidNotificationId}");
            deleteResponse.Should().NotBeNull();
            deleteResponse.StatusCode.Should().Be(OK);
            var deleteResponseBody = DeserializeObject<NotificationDto>(await deleteResponse.Content.ReadAsStringAsync());
            deleteResponseBody.Should().BeEquivalentTo(deletedNotificationDto, options => options.Excluding(notification => notification.DeletedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiNotification}/{ValidNotificationId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(NotFound);
            var getResponseBody = DeserializeObject<ProblemDetails>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(NotificationNotFound, ValidNotificationId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.DeleteAsync($"{ApiNotification}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(NotificationNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });
    }
}