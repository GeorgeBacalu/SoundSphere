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

        [Fact] public void GetAll_Test()
        {
            _notificationRepositoryMock.Setup(mock => mock.GetAll(_notificationPayload)).Returns(_notificationsPagination);
            _notificationService.GetAll(_notificationPayload).Should().BeEquivalentTo(_notificationDtosPagination);
        }

        [Fact] public void GetById_ValidId_Test()
        {
            _notificationRepositoryMock.Setup(mock => mock.GetById(ValidNotificationId)).Returns(_notifications[0]);
            _notificationService.GetById(ValidNotificationId).Should().BeEquivalentTo(_notificationDtos[0]);
        }

        [Fact] public void GetById_InvalidId_Test()
        {
            _notificationRepositoryMock.Setup(mock => mock.GetById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(NotificationNotFound, InvalidId)));
            _notificationService.Invoking(service => service.GetById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(NotificationNotFound, InvalidId));
            _notificationRepositoryMock.Verify(mock => mock.GetById(InvalidId));
        }

        [Fact] public void Add_Test()
        {
            _userRepositoryMock.Setup(mock => mock.GetById(ValidUserId)).Returns(_users[0]);
            _userRepositoryMock.Setup(mock => mock.GetById(ValidUserId2)).Returns(_users[1]);
            _notificationRepositoryMock.Setup(mock => mock.Add(It.IsAny<Notification>())).Returns(_newNotification);
            _notificationService.Add(_newNotificationDto).Should().BeEquivalentTo(_newNotificationDto, options => options.Excluding(notification => notification.Id).Excluding(notification => notification.CreatedAt).Excluding(notification => notification.UpdatedAt));
            _notificationRepositoryMock.Verify(mock => mock.Add(It.IsAny<Notification>()));
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Notification updatedNotification = _notifications[0];
            updatedNotification.Type = _notifications[1].Type;
            updatedNotification.Message = _notifications[1].Message;
            updatedNotification.IsRead = _notifications[1].IsRead;
            NotificationDto updatedNotificationDto = updatedNotification.ToDto(_mapper);
            _notificationRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Notification>(), ValidNotificationId)).Returns(updatedNotification);
            _notificationService.UpdateById(_notificationDtos[1], ValidNotificationId).Should().BeEquivalentTo(updatedNotificationDto);
            _notificationRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Notification>(), ValidNotificationId));
        }

        [Fact] public void UpdateById_InvalidId_Test()
        {
            _notificationRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Notification>(), InvalidId)).Throws(new ResourceNotFoundException(string.Format(NotificationNotFound, InvalidId)));
            _notificationService.Invoking(service => service.UpdateById(_notificationDtos[1], InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(NotificationNotFound, InvalidId));
            _notificationRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Notification>(), InvalidId));
        }

        [Fact] public void DeleteById_ValidId_Test()
        {
            Notification deletedNotification = _notifications[0];
            deletedNotification.DeletedAt = DateTime.UtcNow;
            NotificationDto deletedNotificationDto = deletedNotification.ToDto(_mapper);
            _notificationRepositoryMock.Setup(mock => mock.DeleteById(ValidNotificationId)).Returns(deletedNotification);
            _notificationService.DeleteById(ValidNotificationId).Should().BeEquivalentTo(deletedNotificationDto);
            _notificationRepositoryMock.Verify(mock => mock.DeleteById(ValidNotificationId));
        }

        [Fact] public void DeleteById_InvalidId_Test()
        {
            _notificationRepositoryMock.Setup(mock => mock.DeleteById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(NotificationNotFound, InvalidId)));
            _notificationService.Invoking(service => service.DeleteById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(NotificationNotFound, InvalidId));
            _notificationRepositoryMock.Verify(mock => mock.DeleteById(InvalidId));
        }
    }
}