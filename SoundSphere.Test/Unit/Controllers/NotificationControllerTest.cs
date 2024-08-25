using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.NotificationMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class NotificationControllerTest
    {
        private readonly Mock<INotificationService> _notificationServiceMock = new();
        private readonly NotificationController _notificationController;

        public NotificationControllerTest() => _notificationController = new(_notificationServiceMock.Object);

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedNotifications()
        {
            _notificationServiceMock.Setup(mock => mock.GetAllAsync(_notificationPayload)).ReturnsAsync(_notificationDtosPagination);
            OkObjectResult? result = await _notificationController.GetAllAsync(_notificationPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_notificationDtosPagination);
        }

        [Fact] public async Task GetByIdAsync_ShouldReturnNotification_WhenNotificationIdIsValid()
        {
            _notificationServiceMock.Setup(mock => mock.GetByIdAsync(ValidNotificationId)).ReturnsAsync(_notificationDtos[0]);
            OkObjectResult? result = await _notificationController.GetByIdAsync(ValidNotificationId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_notificationDtos[0]);
        }

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid()
        {
            _notificationServiceMock.Setup(mock => mock.GetByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(NotificationNotFound, InvalidId)));
            await _notificationController.Invoking(controller => controller.GetByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(NotificationNotFound, InvalidId));
        }

        [Fact] public async Task AddAsync_ShouldAddNewNotification_WhenNotificationDtoIsValid()
        {
            _notificationServiceMock.Setup(mock => mock.AddAsync(It.IsAny<NotificationDto>())).ReturnsAsync(_newNotificationDto);
            CreatedResult? result = await _notificationController.AddAsync(_newNotificationDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newNotificationDto);
        }

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateNotification_WhenNotificationIdIsValid()
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

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid()
        {
            _notificationServiceMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<NotificationDto>(), InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(NotificationNotFound, InvalidId)));
            await _notificationController.Invoking(controller => controller.UpdateByIdAsync(_notificationDtos[1], InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(NotificationNotFound, InvalidId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteNotification_WhenNotificationIdIsValid()
        {
            NotificationDto deletedNotificationDto = _notificationDtos[0];
            deletedNotificationDto.DeletedAt = DateTime.UtcNow;
            _notificationServiceMock.Setup(mock => mock.DeleteByIdAsync(ValidNotificationId)).ReturnsAsync(deletedNotificationDto);
            OkObjectResult? result = await _notificationController.DeleteByIdAsync(ValidNotificationId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedNotificationDto);
        }

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenNotificationIdIsInvalid()
        {
            _notificationServiceMock.Setup(mock => mock.DeleteByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(NotificationNotFound, InvalidId)));
            await _notificationController.Invoking(controller => controller.DeleteByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(NotificationNotFound, InvalidId));
        }
    }
}