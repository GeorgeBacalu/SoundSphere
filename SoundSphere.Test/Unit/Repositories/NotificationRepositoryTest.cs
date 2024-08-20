using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.NotificationMock;

namespace SoundSphere.Test.Unit.Repositories
{
    public class NotificationRepositoryTest
    {
        private readonly Mock<DbSet<Notification>> _dbSetMock = new();
        private readonly Mock<AppDbContext> _dbContextMock = new();
        private readonly INotificationRepository _notificationRepository;

        public NotificationRepositoryTest()
        {
            var asyncQueryableNotifications = (IQueryable<Notification>)new AsyncQueryable<Notification>(_notifications);
            _dbSetMock.As<IQueryable<Notification>>().Setup(mock => mock.Provider).Returns(asyncQueryableNotifications.Provider);
            _dbSetMock.As<IQueryable<Notification>>().Setup(mock => mock.Expression).Returns(asyncQueryableNotifications.Expression);
            _dbSetMock.As<IQueryable<Notification>>().Setup(mock => mock.ElementType).Returns(asyncQueryableNotifications.ElementType);
            _dbSetMock.As<IQueryable<Notification>>().Setup(mock => mock.GetEnumerator()).Returns(asyncQueryableNotifications.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Notifications).Returns(_dbSetMock.Object);
            _notificationRepository = new NotificationRepository(_dbContextMock.Object);
        }

        [Fact] public async Task GetAll_Test() => (await _notificationRepository.GetAllAsync(_notificationPayload)).Should().BeEquivalentTo(_notificationsPagination);

        [Fact] public async Task GetById_ValidId_Test() => (await _notificationRepository.GetByIdAsync(ValidNotificationId)).Should().BeEquivalentTo(_notifications[0]);

        [Fact] public async Task GetById_InvalidId_Test() => await _notificationRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId));

        [Fact] public async Task Add_Test()
        {
            Notification result = await _notificationRepository.AddAsync(_newNotification);
            result.Should().BeEquivalentTo(_newNotification, options => options.Excluding(notification => notification.Id).Excluding(notification => notification.CreatedAt).Excluding(notification => notification.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateById_ValidId_Test()
        {
            Mock<CustomEntityEntry<Notification>> entryMock = new();
            entryMock.SetupProperty(mock => mock.State, EntityState.Modified);
            _dbContextMock.Setup(mock => mock.Entry(It.IsAny<Notification>())).Returns(entryMock.Object);
            Notification updatedNotification = _notifications[0];
            updatedNotification.Type = _notifications[1].Type;
            updatedNotification.Message = _notifications[1].Message;
            updatedNotification.IsRead = _notifications[1].IsRead;
            Notification result = await _notificationRepository.UpdateByIdAsync(_notifications[1], ValidNotificationId);
            result.Should().BeEquivalentTo(updatedNotification, options => options.Excluding(notification => notification.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateById_InvalidId_Test() => await _notificationRepository
            .Invoking(repository => repository.UpdateByIdAsync(_notifications[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId));

        [Fact] public async Task DeleteById_ValidId_Test()
        {
            Notification result = await _notificationRepository.DeleteByIdAsync(ValidNotificationId);
            result.Should().BeEquivalentTo(_notifications[0], options => options.Excluding(notification => notification.DeletedAt));
            result.DeletedAt.Should().NotBe(null);
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task DeleteById_InvalidId_Test() => await _notificationRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId));
    }
}