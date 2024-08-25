using AutoMapper;
using FluentAssertions;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services;
using SoundSphere.Database.Context;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.FeedbackMock;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Integration.Services
{
    public class FeedbackServiceIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;
        private readonly IMapper _mapper;

        public FeedbackServiceIntegrationTest(DbFixture dbFixture) => (_dbFixture, _mapper) = (dbFixture, new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper());

        private async Task ExecuteAsync(Func<FeedbackService, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var feedbackService = new FeedbackService(new FeedbackRepository(context), new UserRepository(context), _mapper);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await _dbFixture.TrackAndAddAsync(context, _users);
            await _dbFixture.TrackAndAddAsync(context, _feedbacks);
            await action(feedbackService, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedFeedbacks() => await ExecuteAsync(async (feedbackService, context) => (await feedbackService.GetAllAsync(_feedbackPayload)).Should().BeEquivalentTo(_feedbackDtosPagination));

        [Fact] public async Task GetByIdAsync_ShouldReturnFeedback_WhenFeedbackIdIsValid() => await ExecuteAsync(async (feedbackService, context) => (await feedbackService.GetByIdAsync(ValidFeedbackId)).Should().BeEquivalentTo(_feedbackDtos[0]));

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenFeedbackIdIsInvalid() => await ExecuteAsync(async (feedbackService, context) => await feedbackService
            .Invoking(service => service.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId)));

        [Fact] public async Task AddAsync_ShouldAddNewFeedback_WhenFeedbackDtoIsValid() => await ExecuteAsync(async (feedbackService, context) =>
        {
            FeedbackDto result = await feedbackService.AddAsync(_newFeedbackDto);
            (await context.Feedbacks.FindAsync(result.Id)).Should().BeEquivalentTo(_newFeedback, options => options.Excluding(feedback => feedback.Id).Excluding(feedback => feedback.CreatedAt).Excluding(feedback => feedback.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateFeedback_WhenFeedbackIdIsValid() => await ExecuteAsync(async (feedbackService, context) =>
        {
            Feedback updatedFeedback = _feedbacks[0];
            updatedFeedback.Type = _feedbacks[1].Type;
            updatedFeedback.Message = _feedbacks[1].Message;
            FeedbackDto updatedFeedbackDto = updatedFeedback.ToDto(_mapper);
            FeedbackDto result = await feedbackService.UpdateByIdAsync(_feedbackDtos[1], ValidFeedbackId);
            result.Should().BeEquivalentTo(updatedFeedbackDto, options => options.Excluding(feedback => feedback.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenFeedbackIdIsInvalid() => await ExecuteAsync(async (feedbackService, context) => await feedbackService
            .Invoking(service => service.UpdateByIdAsync(_feedbackDtos[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId)));

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteFeedback_WhenFeedbackIdIsValid() => await ExecuteAsync(async (feedbackService, context) =>
        {
            FeedbackDto result = await feedbackService.DeleteByIdAsync(ValidFeedbackId);
            result.Should().BeEquivalentTo(_feedbackDtos[0], options => options.Excluding(feedback => feedback.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenFeedbackIdIsInvalid() => await ExecuteAsync(async (feedbackService, context) => await feedbackService
            .Invoking(service => service.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId)));
    }
}