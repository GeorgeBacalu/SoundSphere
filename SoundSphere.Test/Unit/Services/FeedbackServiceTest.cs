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
        private readonly Feedback _feedback1 = GetFeedback1();
        private readonly Feedback _feedback2 = GetFeedback2();
        private readonly Feedback _newFeedback = GetNewFeedback();
        private readonly List<Feedback> _feedbacks = GetFeedbacks();
        private readonly FeedbackDto _feedbackDto1 = GetFeedbackDto1();
        private readonly FeedbackDto _feedbackDto2 = GetFeedbackDto2();
        private readonly FeedbackDto _newFeedbackDto = GetNewFeedbackDto();
        private readonly List<FeedbackDto> _feedbackDtos = GetFeedbackDtos();
        private readonly User _user1 = GetUser1();

        public FeedbackServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _feedbackService = new FeedbackService(_feedbackRepositoryMock.Object, _userRepositoryMock.Object, _mapper);
        }

        [Fact] public void GetAll_Test()
        {
            _feedbackRepositoryMock.Setup(mock => mock.GetAll()).Returns(_feedbacks);
            _feedbackService.GetAll().Should().BeEquivalentTo(_feedbackDtos);
        }

        [Fact] public void GetById_ValidId_Test()
        {
            _feedbackRepositoryMock.Setup(mock => mock.GetById(ValidFeedbackId)).Returns(_feedback1);
            _feedbackService.GetById(ValidFeedbackId).Should().BeEquivalentTo(_feedbackDto1);
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
            _userRepositoryMock.Setup(mock => mock.GetById(ValidUserId)).Returns(_user1);
            _feedbackRepositoryMock.Setup(mock => mock.Add(It.IsAny<Feedback>())).Returns(_newFeedback);
            _feedbackService.Add(_newFeedbackDto).Should().BeEquivalentTo(_newFeedbackDto, options => options.Excluding(feedback => feedback.Id).Excluding(feedback => feedback.CreatedAt).Excluding(feedback => feedback.UpdatedAt));
            _feedbackRepositoryMock.Verify(mock => mock.Add(It.IsAny<Feedback>()));
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Feedback updatedFeedback = _feedback1;
            updatedFeedback.Type = _feedback2.Type;
            updatedFeedback.Message = _feedback2.Message;
            FeedbackDto updatedFeedbackDto = updatedFeedback.ToDto(_mapper);
            _feedbackRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Feedback>(), ValidFeedbackId)).Returns(updatedFeedback);
            _feedbackService.UpdateById(_feedbackDto2, ValidFeedbackId).Should().BeEquivalentTo(updatedFeedbackDto);
            _feedbackRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Feedback>(), ValidFeedbackId));
        }

        [Fact] public void UpdateById_InvalidId_Test()
        {
            _feedbackRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Feedback>(), InvalidId)).Throws(new ResourceNotFoundException(string.Format(FeedbackNotFound, InvalidId)));
            _feedbackService.Invoking(service => service.UpdateById(_feedbackDto2, InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(FeedbackNotFound, InvalidId));
            _feedbackRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Feedback>(), InvalidId));
        }

        [Fact] public void DeleteById_ValidId_Test()
        {
            Feedback deletedFeedback = _feedback1;
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