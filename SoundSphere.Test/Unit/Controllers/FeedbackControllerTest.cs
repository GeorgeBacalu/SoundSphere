using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
using SoundSphere.Core.Mappings;
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
        private readonly IMapper _mapper;
        private readonly FeedbackDto _feedbackDto1 = GetFeedbackDto1();
        private readonly FeedbackDto _feedbackDto2 = GetFeedbackDto1();
        private readonly FeedbackDto _newFeedbackDto = GetNewFeedbackDto();
        private readonly List<FeedbackDto> _feedbackDtos = GetFeedbackDtos();

        public FeedbackControllerTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _feedbackController = new(_feedbackServiceMock.Object);
        }

        [Fact] public void GetAll_Test()
        {
            _feedbackServiceMock.Setup(mock => mock.GetAll()).Returns(_feedbackDtos);
            OkObjectResult? result = _feedbackController.GetAll() as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_feedbackDtos);
        }

        [Fact] public void GetById_Test()
        {
            _feedbackServiceMock.Setup(mock => mock.GetById(ValidFeedbackId)).Returns(_feedbackDto1);
            OkObjectResult? result = _feedbackController.GetById(ValidFeedbackId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_feedbackDto1);
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
            FeedbackDto updatedFeedbackDto = _feedbackDto1;
            updatedFeedbackDto.Type = _feedbackDto2.Type;
            updatedFeedbackDto.Message = _feedbackDto2.Message;
            _feedbackServiceMock.Setup(mock => mock.UpdateById(It.IsAny<FeedbackDto>(), ValidFeedbackId)).Returns(updatedFeedbackDto);
            OkObjectResult? result = _feedbackController.UpdateById(_feedbackDto2, ValidFeedbackId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedFeedbackDto);
        }

        [Fact] public void DeleteById_Test()
        {
            FeedbackDto deletedFeedbackDto = _feedbackDto1;
            deletedFeedbackDto.DeletedAt = DateTime.UtcNow;
            _feedbackServiceMock.Setup(mock => mock.DeleteById(ValidFeedbackId)).Returns(deletedFeedbackDto);
            OkObjectResult? result = _feedbackController.DeleteById(ValidFeedbackId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedFeedbackDto);
        }
    }
}