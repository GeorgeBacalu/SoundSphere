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
        private readonly Notification _notification1 = GetNotification1();
        private readonly Notification _notification2 = GetNotification2();
        private readonly Notification _newNotification = GetNewNotification();
        private readonly List<Notification> _notifications = GetNotifications();

        public NotificationRepositoryTest()
        {
            IQueryable<Notification> queryableNotifications = _notifications.AsQueryable();
            _dbSetMock.As<IQueryable<Notification>>().Setup(mock => mock.Provider).Returns(queryableNotifications.Provider);
            _dbSetMock.As<IQueryable<Notification>>().Setup(mock => mock.Expression).Returns(queryableNotifications.Expression);
            _dbSetMock.As<IQueryable<Notification>>().Setup(mock => mock.ElementType).Returns(queryableNotifications.ElementType);
            _dbSetMock.As<IQueryable<Notification>>().Setup(mock => mock.GetEnumerator()).Returns(queryableNotifications.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Notifications).Returns(_dbSetMock.Object);
            _notificationRepository = new NotificationRepository(_dbContextMock.Object);
        }

        [Fact] public void GetAll_Test() => _notificationRepository.GetAll().Should().BeEquivalentTo(_notifications);

        [Fact] public void GetById_ValidId_Test() => _notificationRepository.GetById(ValidNotificationId).Should().BeEquivalentTo(_notification1);

        [Fact] public void GetById_InvalidId_Test() => _notificationRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId));

        [Fact] public void Add_Test()
        {
            Notification result = _notificationRepository.Add(_newNotification);
            result.Should().BeEquivalentTo(_newNotification, options => options.Excluding(notification => notification.Id).Excluding(notification => notification.CreatedAt).Excluding(notification => notification.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Mock<CustomEntityEntry<Notification>> entryMock = new();
            entryMock.SetupProperty(mock => mock.State, EntityState.Modified);
            _dbContextMock.Setup(mock => mock.Entry(It.IsAny<Notification>())).Returns(entryMock.Object);
            Notification updatedNotification = _notification1;
            updatedNotification.Type = _notification2.Type;
            updatedNotification.Message = _notification2.Message;
            updatedNotification.IsRead = _notification2.IsRead;
            Notification result = _notificationRepository.UpdateById(_notification2, ValidNotificationId);
            result.Should().BeEquivalentTo(updatedNotification, options => options.Excluding(notification => notification.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_InvalidId_Test() => _notificationRepository
            .Invoking(repository => repository.UpdateById(_notification2, InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId));

        [Fact] public void DeleteById_ValidId_Test()
        {
            Notification result = _notificationRepository.DeleteById(ValidNotificationId);
            result.Should().BeEquivalentTo(_notification1, options => options.Excluding(notification => notification.DeletedAt));
            result.DeletedAt.Should().NotBe(null);
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void DeleteById_InvalidId_Test() => _notificationRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId));
    }
}