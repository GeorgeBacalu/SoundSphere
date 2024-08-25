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
using static SoundSphere.Test.Mocks.NotificationMock;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Integration.Services
{
    public class NotificationServiceIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;
        private readonly IMapper _mapper;

        public NotificationServiceIntegrationTest(DbFixture dbFixture) => (_dbFixture, _mapper) = (dbFixture, new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper());

        private async Task ExecuteAsync(Func<NotificationService, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var notificationService = new NotificationService(new NotificationRepository(context), new UserRepository(context), _mapper);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await _dbFixture.TrackAndAddAsync(context, _users);
            await _dbFixture.TrackAndAddAsync(context, _notifications);
            await action(notificationService, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedNotifications() => await ExecuteAsync(async (notificationService, context) => (await notificationService.GetAllAsync(_notificationPayload)).Should().BeEquivalentTo(_notificationDtosPagination ));

        [Fact] public async Task GetByIdAsync_ShouldReturnNotification_WhenNotificationIdIsValid() => await ExecuteAsync(async (notificationService, context) => (await notificationService.GetByIdAsync(ValidNotificationId)).Should().BeEquivalentTo(_notificationDtos[0]));

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid() => await ExecuteAsync(async (notificationService, context) => await notificationService
            .Invoking(service => service.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId)));

        [Fact] public async Task AddAsync_ShouldAddNewNotification_WhenNotificationDtoIsValid() => await ExecuteAsync(async (notificationService, context) =>
        {
            NotificationDto result = await notificationService.AddAsync(_newNotificationDto);
            (await context.Notifications.FindAsync(result.Id)).Should().BeEquivalentTo(_newNotification, options => options.Excluding(notification => notification.Id).Excluding(notification => notification.CreatedAt).Excluding(notification => notification.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateNotification_WhenNotificationIdIsValid() => await ExecuteAsync(async (notificationService, context) =>
        {
            Notification updatedNotification = _notifications[0];
            updatedNotification.Type = _notifications[1].Type;
            updatedNotification.Message = _notifications[1].Message;
            updatedNotification.IsRead = _notifications[1].IsRead;
            NotificationDto updatedNotificationDto = updatedNotification.ToDto(_mapper);
            NotificationDto result = await notificationService.UpdateByIdAsync(_notificationDtos[1], ValidNotificationId);
            result.Should().BeEquivalentTo(updatedNotificationDto, options => options.Excluding(notification => notification.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid() => await ExecuteAsync(async (notificationService, context) => await notificationService
            .Invoking(service => service.UpdateByIdAsync(_notificationDtos[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId)));

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteNotification_WhenNotificationIdIsValid() => await ExecuteAsync(async (notificationService, context) =>
        {
            NotificationDto result = await notificationService.DeleteByIdAsync(ValidNotificationId);
            result.Should().BeEquivalentTo(_notificationDtos[0], options => options.Excluding(notification => notification.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid() => await ExecuteAsync(async (notificationService, context) => await notificationService
            .Invoking(service => service.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId)));
    }
}