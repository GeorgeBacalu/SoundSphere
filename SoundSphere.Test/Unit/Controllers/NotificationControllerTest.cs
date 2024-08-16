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

        public NotificationControllerTest() => _notificationController = new(_notificationServiceMock.Object);

        [Fact] public void GetAll_Test()
        {
            _notificationServiceMock.Setup(mock => mock.GetAll(_notificationPayload)).Returns(_notificationDtosPagination);
            OkObjectResult? result = _notificationController.GetAll(_notificationPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_notificationDtosPagination);
        }

        [Fact] public void GetById_Test()
        {
            _notificationServiceMock.Setup(mock => mock.GetById(ValidNotificationId)).Returns(_notificationDtos[0]);
            OkObjectResult? result = _notificationController.GetById(ValidNotificationId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_notificationDtos[0]);
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
            NotificationDto updatedNotificationDto = _notificationDtos[0];
            updatedNotificationDto.Type = _notificationDtos[1].Type;
            updatedNotificationDto.Message = _notificationDtos[1].Message;
            updatedNotificationDto.IsRead = _notificationDtos[1].IsRead;
            _notificationServiceMock.Setup(mock => mock.UpdateById(It.IsAny<NotificationDto>(), ValidNotificationId)).Returns(updatedNotificationDto);
            OkObjectResult? result = _notificationController.UpdateById(_notificationDtos[1], ValidNotificationId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedNotificationDto);
        }

        [Fact] public void DeleteById_Test()
        {
            NotificationDto deletedNotificationDto = _notificationDtos[0];
            deletedNotificationDto.DeletedAt = DateTime.UtcNow;
            _notificationServiceMock.Setup(mock => mock.DeleteById(ValidNotificationId)).Returns(deletedNotificationDto);
            OkObjectResult? result = _notificationController.DeleteById(ValidNotificationId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedNotificationDto);
        }
    }
}