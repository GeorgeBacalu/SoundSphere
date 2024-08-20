using FluentAssertions;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.FeedbackMock;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Integration.Repositories
{
    public class FeedbackRepositoryIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;

        public FeedbackRepositoryIntegrationTest(DbFixture dbFixture) => _dbFixture = dbFixture;

        private async Task ExecuteAsync(Func<FeedbackRepository, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var feedbackRepository = new FeedbackRepository(context);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await _dbFixture.TrackAndAddAsync(context, _users);
            await _dbFixture.TrackAndAddAsync(context, _feedbacks);
            await action(feedbackRepository, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAll_Test() => await ExecuteAsync(async (feedbackRepository, context) => (await feedbackRepository.GetAllAsync(_feedbackPayload)).Should().BeEquivalentTo(_feedbacksPagination));

        [Fact] public async Task GetById_ValidId_Test() => await ExecuteAsync(async (feedbackRepository, context) => (await feedbackRepository.GetByIdAsync(ValidFeedbackId)).Should().BeEquivalentTo(_feedbacks[0]));

        [Fact] public async Task GetById_InvalidId_Test() => await ExecuteAsync(async (feedbackRepository, context) => await feedbackRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId)));

        [Fact] public async Task Add_Test() => await ExecuteAsync(async (feedbackRepository, context) =>
        {
            feedbackRepository.LinkFeedbackToUser(_newFeedback);
            Feedback result = await feedbackRepository.AddAsync(_newFeedback);
            context.Feedbacks.Find(result.Id).Should().BeEquivalentTo(_newFeedback, options => options.Excluding(feedback => feedback.Id).Excluding(feedback => feedback.CreatedAt).Excluding(feedback => feedback.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateById_ValidId_Test() => await ExecuteAsync(async (feedbackRepository, context) =>
        {
            Feedback updatedFeedback = _feedbacks[0];
            updatedFeedback.Type = _feedbacks[1].Type;
            updatedFeedback.Message = _feedbacks[1].Message;
            Feedback result = await feedbackRepository.UpdateByIdAsync(_feedbacks[1], ValidFeedbackId);
            result.Should().BeEquivalentTo(updatedFeedback, options => options.Excluding(feedback => feedback.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateById_InvalidId_Test() => await ExecuteAsync(async (feedbackRepository, context) => await feedbackRepository
            .Invoking(repository => repository.UpdateByIdAsync(_feedbacks[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId)));

        [Fact] public async Task DeleteById_ValidId_Test() => await ExecuteAsync(async (feedbackRepository, context) =>
        {
            Feedback result = await feedbackRepository.DeleteByIdAsync(ValidFeedbackId);
            result.Should().BeEquivalentTo(_feedbacks[0], options => options.Excluding(feedback => feedback.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteById_InvalidId_Test() => await ExecuteAsync(async (feedbackRepository, context) => await feedbackRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId)));
    }
}