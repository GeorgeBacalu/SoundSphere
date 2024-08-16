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
using static SoundSphere.Test.Mocks.FeedbackMock;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Unit.Services
{
    public class FeedbackServiceTest
    {
        private readonly Mock<IFeedbackRepository> _feedbackRepositoryMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly IFeedbackService _feedbackService;
        private readonly IMapper _mapper;

        public FeedbackServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _feedbackService = new FeedbackService(_feedbackRepositoryMock.Object, _userRepositoryMock.Object, _mapper);
        }

        [Fact] public void GetAll_Test()
        {
            _feedbackRepositoryMock.Setup(mock => mock.GetAll(_feedbackPayload)).Returns(_feedbacksPagination);
            _feedbackService.GetAll(_feedbackPayload).Should().BeEquivalentTo(_feedbackDtosPagination);
        }

        [Fact] public void GetById_ValidId_Test()
        {
            _feedbackRepositoryMock.Setup(mock => mock.GetById(ValidFeedbackId)).Returns(_feedbacks[0]);
            _feedbackService.GetById(ValidFeedbackId).Should().BeEquivalentTo(_feedbackDtos[0]);
        }

        [Fact] public void GetById_InvalidId_Test()
        {
            _feedbackRepositoryMock.Setup(mock => mock.GetById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(FeedbackNotFound, InvalidId)));
            _feedbackService.Invoking(service => service.GetById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(FeedbackNotFound, InvalidId));
            _feedbackRepositoryMock.Verify(mock => mock.GetById(InvalidId));
        }

        [Fact] public void Add_Test()
        {
            _userRepositoryMock.Setup(mock => mock.GetById(ValidUserId)).Returns(_users[0]);
            _feedbackRepositoryMock.Setup(mock => mock.Add(It.IsAny<Feedback>())).Returns(_newFeedback);
            _feedbackService.Add(_newFeedbackDto).Should().BeEquivalentTo(_newFeedbackDto, options => options.Excluding(feedback => feedback.Id).Excluding(feedback => feedback.CreatedAt).Excluding(feedback => feedback.UpdatedAt));
            _feedbackRepositoryMock.Verify(mock => mock.Add(It.IsAny<Feedback>()));
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Feedback updatedFeedback = _feedbacks[0];
            updatedFeedback.Type = _feedbacks[1].Type;
            updatedFeedback.Message = _feedbacks[1].Message;
            FeedbackDto updatedFeedbackDto = updatedFeedback.ToDto(_mapper);
            _feedbackRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Feedback>(), ValidFeedbackId)).Returns(updatedFeedback);
            _feedbackService.UpdateById(_feedbackDtos[1], ValidFeedbackId).Should().BeEquivalentTo(updatedFeedbackDto);
            _feedbackRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Feedback>(), ValidFeedbackId));
        }

        [Fact] public void UpdateById_InvalidId_Test()
        {
            _feedbackRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Feedback>(), InvalidId)).Throws(new ResourceNotFoundException(string.Format(FeedbackNotFound, InvalidId)));
            _feedbackService.Invoking(service => service.UpdateById(_feedbackDtos[1], InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(FeedbackNotFound, InvalidId));
            _feedbackRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Feedback>(), InvalidId));
        }

        [Fact] public void DeleteById_ValidId_Test()
        {
            Feedback deletedFeedback = _feedbacks[0];
            deletedFeedback.DeletedAt = DateTime.UtcNow;
            FeedbackDto deletedFeedbackDto = deletedFeedback.ToDto(_mapper);
            _feedbackRepositoryMock.Setup(mock => mock.DeleteById(ValidFeedbackId)).Returns(deletedFeedback);
            _feedbackService.DeleteById(ValidFeedbackId).Should().BeEquivalentTo(deletedFeedbackDto);
            _feedbackRepositoryMock.Verify(mock => mock.DeleteById(ValidFeedbackId));
        }

        [Fact] public void DeleteById_InvalidId_Test()
        {
            _feedbackRepositoryMock.Setup(mock => mock.DeleteById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(FeedbackNotFound, InvalidId)));
            _feedbackService.Invoking(service => service.DeleteById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(FeedbackNotFound, InvalidId));
            _feedbackRepositoryMock.Verify(mock => mock.DeleteById(InvalidId));
        }
    }
}