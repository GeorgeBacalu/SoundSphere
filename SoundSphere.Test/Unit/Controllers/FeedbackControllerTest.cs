using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.FeedbackMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class FeedbackControllerTest
    {
        private readonly Mock<IFeedbackService> _feedbackServiceMock = new();
        private readonly FeedbackController _feedbackController;

        public FeedbackControllerTest() => _feedbackController = new(_feedbackServiceMock.Object);

        [Fact] public void GetAll_Test()
        {
            _feedbackServiceMock.Setup(mock => mock.GetAll(_feedbackPayload)).Returns(_feedbackDtosPagination);
            OkObjectResult? result = _feedbackController.GetAll(_feedbackPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_feedbackDtosPagination);
        }

        [Fact] public void GetById_Test()
        {
            _feedbackServiceMock.Setup(mock => mock.GetById(ValidFeedbackId)).Returns(_feedbackDtos[0]);
            OkObjectResult? result = _feedbackController.GetById(ValidFeedbackId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_feedbackDtos[0]);
        }

        [Fact] public void Add_Test()
        {
            _feedbackServiceMock.Setup(mock => mock.Add(It.IsAny<FeedbackDto>())).Returns(_newFeedbackDto);
            CreatedResult? result = _feedbackController.Add(_newFeedbackDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newFeedbackDto);
        }

        [Fact] public void UpdateById_Test()
        {
            FeedbackDto updatedFeedbackDto = _feedbackDtos[0];
            updatedFeedbackDto.Type = _feedbackDtos[1].Type;
            updatedFeedbackDto.Message = _feedbackDtos[1].Message;
            _feedbackServiceMock.Setup(mock => mock.UpdateById(It.IsAny<FeedbackDto>(), ValidFeedbackId)).Returns(updatedFeedbackDto);
            OkObjectResult? result = _feedbackController.UpdateById(_feedbackDtos[1], ValidFeedbackId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedFeedbackDto);
        }

        [Fact] public void DeleteById_Test()
        {
            FeedbackDto deletedFeedbackDto = _feedbackDtos[0];
            deletedFeedbackDto.DeletedAt = DateTime.UtcNow;
            _feedbackServiceMock.Setup(mock => mock.DeleteById(ValidFeedbackId)).Returns(deletedFeedbackDto);
            OkObjectResult? result = _feedbackController.DeleteById(ValidFeedbackId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedFeedbackDto);
        }
    }
}