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

        private void Execute(Action<NotificationRepository, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var notificationRepository = new NotificationRepository(context);
            using var transaction = context.Database.BeginTransaction();
            _dbFixture.TrackAndAddEntities(context, _users);
            _dbFixture.TrackAndAddEntities(context, _notifications);
            action(notificationRepository, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((notificationRepository, context) => notificationRepository.GetAll(_notificationPayload).Should().BeEquivalentTo(_notificationsPagination));

        [Fact] public void GetById_ValidId_Test() => Execute((notificationRepository, context) => notificationRepository.GetById(ValidNotificationId).Should().BeEquivalentTo(_notifications[0]));

        [Fact] public void GetById_InvalidId_Test() => Execute((notificationRepository, context) => notificationRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((notificationRepository, context) =>
        {
            notificationRepository.LinkNotificationToSender(_newNotification);
            notificationRepository.LinkNotificationToReceiver(_newNotification);
            Notification result = notificationRepository.Add(_newNotification);
            context.Notifications.Find(result.Id).Should().BeEquivalentTo(_newNotification, options => options.Excluding(notification => notification.Id).Excluding(notification => notification.CreatedAt).Excluding(notification => notification.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((notificationRepository, context) =>
        {
            Notification updatedNotification = _notifications[0];
            updatedNotification.Type = _notifications[1].Type;
            updatedNotification.Message = _notifications[1].Message;
            updatedNotification.IsRead = _notifications[1].IsRead;
            Notification result = notificationRepository.UpdateById(_notifications[1], ValidNotificationId);
            result.Should().BeEquivalentTo(updatedNotification, options => options.Excluding(notification => notification.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((notificationRepository, context) => notificationRepository
            .Invoking(repository => repository.UpdateById(_notifications[1], InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((notificationRepository, context) =>
        {
            Notification result = notificationRepository.DeleteById(ValidNotificationId);
            result.Should().BeEquivalentTo(_notifications[0], options => options.Excluding(notification => notification.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((notificationRepository, context) => notificationRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId)));
    }
}