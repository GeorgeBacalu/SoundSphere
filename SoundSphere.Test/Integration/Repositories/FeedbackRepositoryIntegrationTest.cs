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

        private void Execute(Action<FeedbackRepository, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var feedbackRepository = new FeedbackRepository(context);
            using var transaction = context.Database.BeginTransaction();
            _dbFixture.TrackAndAddEntities(context, _users);
            _dbFixture.TrackAndAddEntities(context, _feedbacks);
            action(feedbackRepository, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((feedbackRepository, context) => feedbackRepository.GetAll(_feedbackPayload).Should().BeEquivalentTo(_feedbacksPagination));

        [Fact] public void GetById_ValidId_Test() => Execute((feedbackRepository, context) => feedbackRepository.GetById(ValidFeedbackId).Should().BeEquivalentTo(_feedbacks[0]));

        [Fact] public void GetById_InvalidId_Test() => Execute((feedbackRepository, context) => feedbackRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((feedbackRepository, context) =>
        {
            feedbackRepository.LinkFeedbackToUser(_newFeedback);
            Feedback result = feedbackRepository.Add(_newFeedback);
            context.Feedbacks.Find(result.Id).Should().BeEquivalentTo(_newFeedback, options => options.Excluding(feedback => feedback.Id).Excluding(feedback => feedback.CreatedAt).Excluding(feedback => feedback.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((feedbackRepository, context) =>
        {
            Feedback updatedFeedback = _feedbacks[0];
            updatedFeedback.Type = _feedbacks[1].Type;
            updatedFeedback.Message = _feedbacks[1].Message;
            Feedback result = feedbackRepository.UpdateById(_feedbacks[1], ValidFeedbackId);
            result.Should().BeEquivalentTo(updatedFeedback, options => options.Excluding(feedback => feedback.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((feedbackRepository, context) => feedbackRepository
            .Invoking(repository => repository.UpdateById(_feedbacks[1], InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((feedbackRepository, context) =>
        {
            Feedback result = feedbackRepository.DeleteById(ValidFeedbackId);
            result.Should().BeEquivalentTo(_feedbacks[0], options => options.Excluding(feedback => feedback.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((feedbackRepository, context) => feedbackRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId)));
    }
}