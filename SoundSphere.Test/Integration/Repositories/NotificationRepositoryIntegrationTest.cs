using FluentAssertions;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.NotificationMock;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Integration.Repositories
{
    public class NotificationRepositoryIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;

        public NotificationRepositoryIntegrationTest(DbFixture dbFixture) => _dbFixture = dbFixture;

        private async Task ExecuteAsync(Func<NotificationRepository, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var notificationRepository = new NotificationRepository(context);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await _dbFixture.TrackAndAddAsync(context, _users);
            await _dbFixture.TrackAndAddAsync(context, _notifications);
            await action(notificationRepository, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedNotifications() => await ExecuteAsync(async (notificationRepository, context) => (await notificationRepository.GetAllAsync(_notificationPayload)).Should().BeEquivalentTo(_notificationsPagination));

        [Fact] public async Task GetByIdAsync_ShouldReturnNotification_WhenNotificationIdIsValid() => await ExecuteAsync(async (notificationRepository, context) => (await notificationRepository.GetByIdAsync(ValidNotificationId)).Should().BeEquivalentTo(_notifications[0]));

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid() => await ExecuteAsync(async (notificationRepository, context) => await notificationRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId)));

        [Fact] public async Task AddAsync_ShouldAddNewNotification_WhenNotificationDtoIsValid() => await ExecuteAsync(async (notificationRepository, context) =>
        {
            notificationRepository.LinkNotificationToSender(_newNotification);
            notificationRepository.LinkNotificationToReceiver(_newNotification);
            Notification result = await notificationRepository.AddAsync(_newNotification);
            context.Notifications.Find(result.Id).Should().BeEquivalentTo(_newNotification, options => options.Excluding(notification => notification.Id).Excluding(notification => notification.CreatedAt).Excluding(notification => notification.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateNotification_WhenNotificationIdIsValid() => await ExecuteAsync(async (notificationRepository, context) =>
        {
            Notification updatedNotification = _notifications[0];
            updatedNotification.Type = _notifications[1].Type;
            updatedNotification.Message = _notifications[1].Message;
            updatedNotification.IsRead = _notifications[1].IsRead;
            Notification result = await notificationRepository.UpdateByIdAsync(_notifications[1], ValidNotificationId);
            result.Should().BeEquivalentTo(updatedNotification, options => options.Excluding(notification => notification.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid() => await ExecuteAsync(async (notificationRepository, context) => await notificationRepository
            .Invoking(repository => repository.UpdateByIdAsync(_notifications[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId)));

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteNotification_WhenNotificationIdIsValid() => await ExecuteAsync(async (notificationRepository, context) =>
        {
            Notification result = await notificationRepository.DeleteByIdAsync(ValidNotificationId);
            result.Should().BeEquivalentTo(_notifications[0], options => options.Excluding(notification => notification.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid() => await ExecuteAsync(async (notificationRepository, context) => await notificationRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId)));
    }
}