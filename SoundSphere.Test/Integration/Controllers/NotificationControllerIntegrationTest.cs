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
        private readonly List<Notification> _notifications = GetNotifications();
        private readonly NotificationDto _notificationDto1 = GetNotificationDto1();
        private readonly NotificationDto _notificationDto2 = GetNotificationDto2();
        private readonly NotificationDto _newNotificationDto = GetNewNotificationDto();
        private readonly List<NotificationDto> _notificationDtos = GetNotificationDtos();
        private readonly List<User> _users = GetUsers();

        public NotificationControllerIntegrationTest() { _dbFixture = new DbFixture(); _factory = new(_dbFixture); _httpClient = _factory.CreateClient(); }

        public void Dispose() { _factory.Dispose(); _httpClient.Dispose(); }

        private async Task Execute(Func<Task> action)
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.ChangeTracker.Clear();
            context.Notifications.RemoveRange(context.Notifications);
            context.Users.RemoveRange(context.Users);
            await context.SaveChangesAsync();
            _dbFixture.TrackAndAddEntities(context, _users);
            _dbFixture.TrackAndAddEntities(context, _notifications);
            await context.SaveChangesAsync();
            await action();
            context.Notifications.RemoveRange(context.Notifications);
            context.Users.RemoveRange(context.Users);
            await context.SaveChangesAsync();
        }

        [Fact] public async Task GetAll_Test() => await Execute(async () =>
        {
            var response = await _httpClient.GetAsync(ApiNotification);
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<List<NotificationDto>>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_notificationDtos);
        });

        [Fact] public async Task GetById_ValidId_Test() => await Execute(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiNotification}/{ValidNotificationId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<NotificationDto>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_notificationDto1);
        });

        [Fact] public async Task GetById_InvalidId_Test() => await Execute(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiNotification}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(NotificationNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task Add_Test() => await Execute(async () =>
        {
            var addResponse = await _httpClient.PostAsync(ApiNotification, new StringContent(SerializeObject(_newNotificationDto), Encoding.UTF8, MediaTypeNames.Application.Json));
            addResponse.Should().NotBeNull();
            addResponse.StatusCode.Should().Be(Created);
            var addResponseBody = DeserializeObject<NotificationDto>(await addResponse.Content.ReadAsStringAsync());
            addResponseBody.Should().BeEquivalentTo(_newNotificationDto, options => options.Excluding(notification => notification.Id).Excluding(notification => notification.CreatedAt).Excluding(notification => notification.UpdatedAt));

            var getAllResponse = await _httpClient.GetAsync(ApiNotification);
            getAllResponse.Should().NotBeNull();
            getAllResponse.StatusCode.Should().Be(OK);
            var getAllResponseBody = DeserializeObject<List<NotificationDto>>(await getAllResponse.Content.ReadAsStringAsync());
            getAllResponseBody.Should().ContainEquivalentOf(addResponseBody, options => options.Excluding(notification => notification.CreatedAt).Excluding(notification => notification.UpdatedAt));
        });

        [Fact] public async Task UpdateById_ValidId_Test() => await Execute(async () =>
        {
            NotificationDto updatedNotificationDto = _notificationDto1;
            updatedNotificationDto.Type = _notificationDto2.Type;
            updatedNotificationDto.Message = _notificationDto2.Message;
            updatedNotificationDto.IsRead = _notificationDto2.IsRead;
            var updateResponse = await _httpClient.PutAsync($"{ApiNotification}/{ValidNotificationId}", new StringContent(SerializeObject(_notificationDto2), Encoding.UTF8, MediaTypeNames.Application.Json));
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

        [Fact] public async Task UpdateById_InvalidId_Test() => await Execute(async () =>
        {
            var response = await _httpClient.PutAsync($"{ApiNotification}/{InvalidId}", new StringContent(SerializeObject(_notificationDto2), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(NotificationNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteById_ValidId_Test() => await Execute(async () =>
        {
            NotificationDto deletedNotificationDto = _notificationDto1;
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

        [Fact] public async Task DeleteById_InvalidId_Test() => await Execute(async () =>
        {
            var response = await _httpClient.DeleteAsync($"{ApiNotification}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(NotificationNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });
    }
}