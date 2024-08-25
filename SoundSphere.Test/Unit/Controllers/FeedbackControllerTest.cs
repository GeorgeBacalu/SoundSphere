using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.FeedbackMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class FeedbackControllerTest
    {
        private readonly Mock<IFeedbackService> _feedbackServiceMock = new();
        private readonly FeedbackController _feedbackController;

        public FeedbackControllerTest() => _feedbackController = new(_feedbackServiceMock.Object);

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedFeedbacks()
        {
            _feedbackServiceMock.Setup(mock => mock.GetAllAsync(_feedbackPayload)).ReturnsAsync(_feedbackDtosPagination);
            OkObjectResult? result = await _feedbackController.GetAllAsync(_feedbackPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_feedbackDtosPagination);
        }

        [Fact] public async Task GetByIdAsync_ShouldReturnFeedback_WhenFeedbackIdIsValid()
        {
            _feedbackServiceMock.Setup(mock => mock.GetByIdAsync(ValidFeedbackId)).ReturnsAsync(_feedbackDtos[0]);
            OkObjectResult? result = await _feedbackController.GetByIdAsync(ValidFeedbackId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_feedbackDtos[0]);
        }

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenFeedbackIdIsInvalid()
        {
            _feedbackServiceMock.Setup(mock => mock.GetByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(FeedbackNotFound, InvalidId)));
            await _feedbackController.Invoking(controller => controller.GetByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(FeedbackNotFound, InvalidId));
        }

        [Fact] public async Task AddAsync_ShouldAddNewFeedback_WhenFeedbackDtoIsValid()
        {
            _feedbackServiceMock.Setup(mock => mock.AddAsync(It.IsAny<FeedbackDto>())).ReturnsAsync(_newFeedbackDto);
            CreatedResult? result = await _feedbackController.AddAsync(_newFeedbackDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newFeedbackDto);
        }

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateFeedback_WhenFeedbackIdIsValid()
        {
            FeedbackDto updatedFeedbackDto = _feedbackDtos[0];
            updatedFeedbackDto.Type = _feedbackDtos[1].Type;
            updatedFeedbackDto.Message = _feedbackDtos[1].Message;
            _feedbackServiceMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<FeedbackDto>(), ValidFeedbackId)).ReturnsAsync(updatedFeedbackDto);
            OkObjectResult? result = await _feedbackController.UpdateByIdAsync(_feedbackDtos[1], ValidFeedbackId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedFeedbackDto);
        }

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenFeedbackIdIsInvalid()
        {
            _feedbackServiceMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<FeedbackDto>(), InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(FeedbackNotFound, InvalidId)));
            await _feedbackController.Invoking(controller => controller.UpdateByIdAsync(_feedbackDtos[1], InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(FeedbackNotFound, InvalidId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteFeedback_WhenFeedbackIdIsValid()
        {
            FeedbackDto deletedFeedbackDto = _feedbackDtos[0];
            deletedFeedbackDto.DeletedAt = DateTime.UtcNow;
            _feedbackServiceMock.Setup(mock => mock.DeleteByIdAsync(ValidFeedbackId)).ReturnsAsync(deletedFeedbackDto);
            OkObjectResult? result = await _feedbackController.DeleteByIdAsync(ValidFeedbackId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedFeedbackDto);
        }

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenFeedbackIdIsInvalid()
        {
            _feedbackServiceMock.Setup(mock => mock.DeleteByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(FeedbackNotFound, InvalidId)));
            await _feedbackController.Invoking(controller => controller.DeleteByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(FeedbackNotFound, InvalidId));
        }
    }
}