using AutoMapper;
using FluentAssertions;
using Moq;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.NotificationMock;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Unit.Services
{
    public class NotificationServiceTest
    {
        private readonly Mock<INotificationRepository> _notificationRepositoryMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _notificationService = new NotificationService(_notificationRepositoryMock.Object, _userRepositoryMock.Object, _mapper);
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedNotifications()
        {
            _notificationRepositoryMock.Setup(mock => mock.GetAllAsync(_notificationPayload)).ReturnsAsync(_notificationsPagination);
            (await _notificationService.GetAllAsync(_notificationPayload)).Should().BeEquivalentTo(_notificationDtosPagination);
        }

        [Fact] public async Task GetByIdAsync_ShouldReturnNotification_WhenNotificationIdIsValid()
        {
            _notificationRepositoryMock.Setup(mock => mock.GetByIdAsync(ValidNotificationId)).ReturnsAsync(_notifications[0]);
            (await _notificationService.GetByIdAsync(ValidNotificationId)).Should().BeEquivalentTo(_notificationDtos[0]);
        }

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid()
        {
            _notificationRepositoryMock.Setup(mock => mock.GetByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(NotificationNotFound, InvalidId)));
            await _notificationService.Invoking(service => service.GetByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(NotificationNotFound, InvalidId));
            _notificationRepositoryMock.Verify(mock => mock.GetByIdAsync(InvalidId));
        }

        [Fact] public async Task AddAsync_ShouldAddNewNotification_WhenNotificationDtoIsValid()
        {
            _userRepositoryMock.Setup(mock => mock.GetByIdAsync(ValidUserId)).ReturnsAsync(_users[0]);
            _userRepositoryMock.Setup(mock => mock.GetByIdAsync(ValidUserId2)).ReturnsAsync(_users[1]);
            _notificationRepositoryMock.Setup(mock => mock.AddAsync(It.IsAny<Notification>())).ReturnsAsync(_newNotification);
            (await _notificationService.AddAsync(_newNotificationDto)).Should().BeEquivalentTo(_newNotificationDto, options => options.Excluding(notification => notification.Id).Excluding(notification => notification.CreatedAt).Excluding(notification => notification.UpdatedAt));
            _notificationRepositoryMock.Verify(mock => mock.AddAsync(It.IsAny<Notification>()));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateNotification_WhenNotificationIdIsValid()
        {
            Notification updatedNotification = _notifications[0];
            updatedNotification.Type = _notifications[1].Type;
            updatedNotification.Message = _notifications[1].Message;
            updatedNotification.IsRead = _notifications[1].IsRead;
            NotificationDto updatedNotificationDto = updatedNotification.ToDto(_mapper);
            _notificationRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<Notification>(), ValidNotificationId)).ReturnsAsync(updatedNotification);
            (await _notificationService.UpdateByIdAsync(_notificationDtos[1], ValidNotificationId)).Should().BeEquivalentTo(updatedNotificationDto);
            _notificationRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<Notification>(), ValidNotificationId));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid()
        {
            _notificationRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<Notification>(), InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(NotificationNotFound, InvalidId)));
            await _notificationService.Invoking(service => service.UpdateByIdAsync(_notificationDtos[1], InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(NotificationNotFound, InvalidId));
            _notificationRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<Notification>(), InvalidId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteNotification_WhenNotificationIdIsValid()
        {
            Notification deletedNotification = _notifications[0];
            deletedNotification.DeletedAt = DateTime.UtcNow;
            NotificationDto deletedNotificationDto = deletedNotification.ToDto(_mapper);
            _notificationRepositoryMock.Setup(mock => mock.DeleteByIdAsync(ValidNotificationId)).ReturnsAsync(deletedNotification);
            (await _notificationService.DeleteByIdAsync(ValidNotificationId)).Should().BeEquivalentTo(deletedNotificationDto);
            _notificationRepositoryMock.Verify(mock => mock.DeleteByIdAsync(ValidNotificationId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid()
        {
            _notificationRepositoryMock.Setup(mock => mock.DeleteByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(NotificationNotFound, InvalidId)));
            await _notificationService.Invoking(service => service.DeleteByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(NotificationNotFound, InvalidId));
            _notificationRepositoryMock.Verify(mock => mock.DeleteByIdAsync(InvalidId));
        }
    }
}