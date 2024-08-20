using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.FeedbackMock;

namespace SoundSphere.Test.Unit.Repositories
{
    public class FeedbackRepositoryTest
    {
        private readonly Mock<DbSet<Feedback>> _dbSetMock = new();
        private readonly Mock<AppDbContext> _dbContextMock = new();
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackRepositoryTest()
        {
            var asyncQueryableFeedbacks = (IQueryable<Feedback>)new AsyncQueryable<Feedback>(_feedbacks);
            _dbSetMock.As<IQueryable<Feedback>>().Setup(mock => mock.Provider).Returns(asyncQueryableFeedbacks.Provider);
            _dbSetMock.As<IQueryable<Feedback>>().Setup(mock => mock.Expression).Returns(asyncQueryableFeedbacks.Expression);
            _dbSetMock.As<IQueryable<Feedback>>().Setup(mock => mock.ElementType).Returns(asyncQueryableFeedbacks.ElementType);
            _dbSetMock.As<IQueryable<Feedback>>().Setup(mock => mock.GetEnumerator()).Returns(asyncQueryableFeedbacks.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Feedbacks).Returns(_dbSetMock.Object);
            _feedbackRepository = new FeedbackRepository(_dbContextMock.Object);
        }

        [Fact] public async Task GetAll_Test() => (await _feedbackRepository.GetAllAsync(_feedbackPayload)).Should().BeEquivalentTo(_feedbacksPagination);

        [Fact] public async Task GetById_ValidId_Test() => (await _feedbackRepository.GetByIdAsync(ValidFeedbackId)).Should().BeEquivalentTo(_feedbacks[0]);

        [Fact] public async Task GetById_InvalidId_Test() => await _feedbackRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId));

        [Fact] public async Task Add_Test()
        {
            Feedback result = await _feedbackRepository.AddAsync(_newFeedback);
            result.Should().BeEquivalentTo(_newFeedback, options => options.Excluding(feedback => feedback.Id).Excluding(feedback => feedback.CreatedAt).Excluding(feedback => feedback.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateById_ValidId_Test()
        {
            Mock<CustomEntityEntry<Feedback>> entryMock = new();
            entryMock.SetupProperty(mock => mock.State, EntityState.Modified);
            _dbContextMock.Setup(mock => mock.Entry(It.IsAny<Feedback>())).Returns(entryMock.Object);
            Feedback updatedFeedback = _feedbacks[0];
            updatedFeedback.Type = _feedbacks[1].Type;
            updatedFeedback.Message = _feedbacks[1].Message;
            Feedback result = await _feedbackRepository.UpdateByIdAsync(_feedbacks[1], ValidFeedbackId);
            result.Should().BeEquivalentTo(updatedFeedback, options => options.Excluding(feedback => feedback.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateById_InvalidId_Test() => await _feedbackRepository
            .Invoking(repository => repository.UpdateByIdAsync(_feedbacks[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId));

        [Fact] public async Task DeleteById_ValidId_Test()
        {
            Feedback result = await _feedbackRepository.DeleteByIdAsync(ValidFeedbackId);
            result.Should().BeEquivalentTo(_feedbacks[0], options => options.Excluding(feedback => feedback.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task DeleteById_InvalidId_Test() => await _feedbackRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId));
    }
}