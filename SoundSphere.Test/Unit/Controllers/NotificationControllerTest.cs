using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.NotificationMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class NotificationControllerTest
    {
        private readonly Mock<INotificationService> _notificationServiceMock = new();
        private readonly NotificationController _notificationController;
        private readonly NotificationDto _notificationDto1 = GetNotificationDto1();
        private readonly NotificationDto _notificationDto2 = GetNotificationDto1();
        private readonly NotificationDto _newNotificationDto = GetNewNotificationDto();
        private readonly List<NotificationDto> _notificationDtos = GetNotificationDtos();

        public NotificationControllerTest() => _notificationController = new(_notificationServiceMock.Object);

        [Fact] public void GetAll_Test()
        {
            _notificationServiceMock.Setup(mock => mock.GetAll()).Returns(_notificationDtos);
            OkObjectResult? result = _notificationController.GetAll() as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_notificationDtos);
        }

        [Fact] public void GetById_Test()
        {
            _notificationServiceMock.Setup(mock => mock.GetById(ValidNotificationId)).Returns(_notificationDto1);
            OkObjectResult? result = _notificationController.GetById(ValidNotificationId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_notificationDto1);
        }

        [Fact] public void Add_Test()
        {
            _notificationServiceMock.Setup(mock => mock.Add(It.IsAny<NotificationDto>())).Returns(_newNotificationDto);
            CreatedResult? result = _notificationController.Add(_newNotificationDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newNotificationDto);
        }

        [Fact] public void UpdateById_Test()
        {
            NotificationDto updatedNotificationDto = _notificationDto1;
            updatedNotificationDto.Type = _notificationDto2.Type;
            updatedNotificationDto.Message = _notificationDto2.Message;
            updatedNotificationDto.IsRead = _notificationDto2.IsRead;
            _notificationServiceMock.Setup(mock => mock.UpdateById(It.IsAny<NotificationDto>(), ValidNotificationId)).Returns(updatedNotificationDto);
            OkObjectResult? result = _notificationController.UpdateById(_notificationDto2, ValidNotificationId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedNotificationDto);
        }

        [Fact] public void DeleteById_Test()
        {
            NotificationDto deletedNotificationDto = _notificationDto1;
            deletedNotificationDto.DeletedAt = DateTime.UtcNow;
            _notificationServiceMock.Setup(mock => mock.DeleteById(ValidNotificationId)).Returns(deletedNotificationDto);
            OkObjectResult? result = _notificationController.DeleteById(ValidNotificationId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedNotificationDto);
        }
    }
}