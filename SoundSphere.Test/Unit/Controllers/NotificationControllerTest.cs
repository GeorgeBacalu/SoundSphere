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

        [Fact] public async Task GetAll_Test()
        {
            _notificationServiceMock.Setup(mock => mock.GetAllAsync(_notificationPayload)).ReturnsAsync(_notificationDtosPagination);
            OkObjectResult? result = await _notificationController.GetAllAsync(_notificationPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_notificationDtosPagination);
        }

        [Fact] public async Task GetById_Test()
        {
            _notificationServiceMock.Setup(mock => mock.GetByIdAsync(ValidNotificationId)).ReturnsAsync(_notificationDtos[0]);
            OkObjectResult? result = await _notificationController.GetByIdAsync(ValidNotificationId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_notificationDtos[0]);
        }

        [Fact] public async Task Add_Test()
        {
            _notificationServiceMock.Setup(mock => mock.AddAsync(It.IsAny<NotificationDto>())).ReturnsAsync(_newNotificationDto);
            CreatedResult? result = await _notificationController.AddAsync(_newNotificationDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newNotificationDto);
        }

        [Fact] public async Task UpdateById_Test()
        {
            NotificationDto updatedNotificationDto = _notificationDtos[0];
            updatedNotificationDto.Type = _notificationDtos[1].Type;
            updatedNotificationDto.Message = _notificationDtos[1].Message;
            updatedNotificationDto.IsRead = _notificationDtos[1].IsRead;
            _notificationServiceMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<NotificationDto>(), ValidNotificationId)).ReturnsAsync(updatedNotificationDto);
            OkObjectResult? result = await _notificationController.UpdateByIdAsync(_notificationDtos[1], ValidNotificationId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedNotificationDto);
        }

        [Fact] public async Task DeleteById_Test()
        {
            NotificationDto deletedNotificationDto = _notificationDtos[0];
            deletedNotificationDto.DeletedAt = DateTime.UtcNow;
            _notificationServiceMock.Setup(mock => mock.DeleteByIdAsync(ValidNotificationId)).ReturnsAsync(deletedNotificationDto);
            OkObjectResult? result = await _notificationController.DeleteByIdAsync(ValidNotificationId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedNotificationDto);
        }
    }
}