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

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedFeedbacks()
        {
            _feedbackRepositoryMock.Setup(mock => mock.GetAllAsync(_feedbackPayload)).ReturnsAsync(_feedbacksPagination);
            (await _feedbackService.GetAllAsync(_feedbackPayload)).Should().BeEquivalentTo(_feedbackDtosPagination);
        }

        [Fact] public async Task GetByIdAsync_ShouldReturnFeedback_WhenFeedbackIdIsValid()
        {
            _feedbackRepositoryMock.Setup(mock => mock.GetByIdAsync(ValidFeedbackId)).ReturnsAsync(_feedbacks[0]);
            (await _feedbackService.GetByIdAsync(ValidFeedbackId)).Should().BeEquivalentTo(_feedbackDtos[0]);
        }

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenFeedbackIdIsInvalid()
        {
            _feedbackRepositoryMock.Setup(mock => mock.GetByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(FeedbackNotFound, InvalidId)));
            await _feedbackService.Invoking(service => service.GetByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(FeedbackNotFound, InvalidId));
            _feedbackRepositoryMock.Verify(mock => mock.GetByIdAsync(InvalidId));
        }

        [Fact] public async Task AddAsync_ShouldAddNewFeedback_WhenFeedbackDtoIsValid()
        {
            _userRepositoryMock.Setup(mock => mock.GetByIdAsync(ValidUserId)).ReturnsAsync(_users[0]);
            _feedbackRepositoryMock.Setup(mock => mock.AddAsync(It.IsAny<Feedback>())).ReturnsAsync(_newFeedback);
            (await _feedbackService.AddAsync(_newFeedbackDto)).Should().BeEquivalentTo(_newFeedbackDto, options => options.Excluding(feedback => feedback.Id).Excluding(feedback => feedback.CreatedAt).Excluding(feedback => feedback.UpdatedAt));
            _feedbackRepositoryMock.Verify(mock => mock.AddAsync(It.IsAny<Feedback>()));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateFeedback_WhenFeedbackIdIsValid()
        {
            Feedback updatedFeedback = _feedbacks[0];
            updatedFeedback.Type = _feedbacks[1].Type;
            updatedFeedback.Message = _feedbacks[1].Message;
            FeedbackDto updatedFeedbackDto = updatedFeedback.ToDto(_mapper);
            _feedbackRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<Feedback>(), ValidFeedbackId)).ReturnsAsync(updatedFeedback);
            (await _feedbackService.UpdateByIdAsync(_feedbackDtos[1], ValidFeedbackId)).Should().BeEquivalentTo(updatedFeedbackDto);
            _feedbackRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<Feedback>(), ValidFeedbackId));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenFeedbackIdIsInvalid()
        {
            _feedbackRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<Feedback>(), InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(FeedbackNotFound, InvalidId)));
            await _feedbackService.Invoking(service => service.UpdateByIdAsync(_feedbackDtos[1], InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(FeedbackNotFound, InvalidId));
            _feedbackRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<Feedback>(), InvalidId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteFeedback_WhenFeedbackIdIsValid()
        {
            Feedback deletedFeedback = _feedbacks[0];
            deletedFeedback.DeletedAt = DateTime.UtcNow;
            FeedbackDto deletedFeedbackDto = deletedFeedback.ToDto(_mapper);
            _feedbackRepositoryMock.Setup(mock => mock.DeleteByIdAsync(ValidFeedbackId)).ReturnsAsync(deletedFeedback);
            (await _feedbackService.DeleteByIdAsync(ValidFeedbackId)).Should().BeEquivalentTo(deletedFeedbackDto);
            _feedbackRepositoryMock.Verify(mock => mock.DeleteByIdAsync(ValidFeedbackId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenFeedbackIdIsInvalid()
        {
            _feedbackRepositoryMock.Setup(mock => mock.DeleteByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(FeedbackNotFound, InvalidId)));
            await _feedbackService.Invoking(service => service.DeleteByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(FeedbackNotFound, InvalidId));
            _feedbackRepositoryMock.Verify(mock => mock.DeleteByIdAsync(InvalidId));
        }
    }
}