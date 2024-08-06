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
        private readonly Notification _notification1 = GetNotification1();
        private readonly Notification _notification2 = GetNotification2();
        private readonly Notification _newNotification = GetNewNotification();
        private readonly List<Notification> _notifications = GetNotifications();
        private readonly NotificationDto _notificationDto1 = GetNotificationDto1();
        private readonly NotificationDto _notificationDto2 = GetNotificationDto2();
        private readonly NotificationDto _newNotificationDto = GetNewNotificationDto();
        private readonly List<NotificationDto> _notificationDtos = GetNotificationDtos();
        private readonly User _user1 = GetUser1();
        private readonly User _user2 = GetUser2();

        public NotificationServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _notificationService = new NotificationService(_notificationRepositoryMock.Object, _userRepositoryMock.Object, _mapper);
        }

        [Fact] public void GetAll_Test()
        {
            _notificationRepositoryMock.Setup(mock => mock.GetAll()).Returns(_notifications);
            _notificationService.GetAll().Should().BeEquivalentTo(_notificationDtos);
        }

        [Fact] public void GetById_ValidId_Test()
        {
            _notificationRepositoryMock.Setup(mock => mock.GetById(ValidNotificationId)).Returns(_notification1);
            _notificationService.GetById(ValidNotificationId).Should().BeEquivalentTo(_notificationDto1);
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
            _userRepositoryMock.Setup(mock => mock.GetById(ValidUserId)).Returns(_user1);
            _userRepositoryMock.Setup(mock => mock.GetById(ValidUserId2)).Returns(_user2);
            _notificationRepositoryMock.Setup(mock => mock.Add(It.IsAny<Notification>())).Returns(_newNotification);
            _notificationService.Add(_newNotificationDto).Should().BeEquivalentTo(_newNotificationDto, options => options.Excluding(notification => notification.Id).Excluding(notification => notification.CreatedAt).Excluding(notification => notification.UpdatedAt));
            _notificationRepositoryMock.Verify(mock => mock.Add(It.IsAny<Notification>()));
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Notification updatedNotification = _notification1;
            updatedNotification.Type = _notification2.Type;
            updatedNotification.Message = _notification2.Message;
            updatedNotification.IsRead = _notification2.IsRead;
            NotificationDto updatedNotificationDto = updatedNotification.ToDto(_mapper);
            _notificationRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Notification>(), ValidNotificationId)).Returns(updatedNotification);
            _notificationService.UpdateById(_notificationDto2, ValidNotificationId).Should().BeEquivalentTo(updatedNotificationDto);
            _notificationRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Notification>(), ValidNotificationId));
        }

        [Fact] public void UpdateById_InvalidId_Test()
        {
            _notificationRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Notification>(), InvalidId)).Throws(new ResourceNotFoundException(string.Format(NotificationNotFound, InvalidId)));
            _notificationService.Invoking(service => service.UpdateById(_notificationDto2, InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(NotificationNotFound, InvalidId));
            _notificationRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Notification>(), InvalidId));
        }

        [Fact] public void DeleteById_ValidId_Test()
        {
            Notification deletedNotification = _notification1;
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