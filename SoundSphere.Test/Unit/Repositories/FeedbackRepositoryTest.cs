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
        private readonly Feedback _feedback1 = GetFeedback1();
        private readonly Feedback _feedback2 = GetFeedback2();
        private readonly Feedback _newFeedback = GetNewFeedback();
        private readonly List<Feedback> _feedbacks = GetFeedbacks();

        public FeedbackRepositoryTest()
        {
            IQueryable<Feedback> queryableFeedbacks = _feedbacks.AsQueryable();
            _dbSetMock.As<IQueryable<Feedback>>().Setup(mock => mock.Provider).Returns(queryableFeedbacks.Provider);
            _dbSetMock.As<IQueryable<Feedback>>().Setup(mock => mock.Expression).Returns(queryableFeedbacks.Expression);
            _dbSetMock.As<IQueryable<Feedback>>().Setup(mock => mock.ElementType).Returns(queryableFeedbacks.ElementType);
            _dbSetMock.As<IQueryable<Feedback>>().Setup(mock => mock.GetEnumerator()).Returns(queryableFeedbacks.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Feedbacks).Returns(_dbSetMock.Object);
            _feedbackRepository = new FeedbackRepository(_dbContextMock.Object);
        }

        [Fact] public void GetAll_Test() => _feedbackRepository.GetAll().Should().BeEquivalentTo(_feedbacks);

        [Fact] public void GetById_ValidId_Test() => _feedbackRepository.GetById(ValidFeedbackId).Should().BeEquivalentTo(_feedback1);

        [Fact] public void GetById_InvalidId_Test() => _feedbackRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId));

        [Fact] public void Add_Test()
        {
            Feedback result = _feedbackRepository.Add(_newFeedback);
            result.Should().BeEquivalentTo(_newFeedback, options => options.Excluding(feedback => feedback.Id).Excluding(feedback => feedback.CreatedAt).Excluding(feedback => feedback.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Mock<CustomEntityEntry<Feedback>> entryMock = new();
            entryMock.SetupProperty(mock => mock.State, EntityState.Modified);
            _dbContextMock.Setup(mock => mock.Entry(It.IsAny<Feedback>())).Returns(entryMock.Object);
            Feedback updatedFeedback = _feedback1;
            updatedFeedback.Type = _feedback2.Type;
            updatedFeedback.Message = _feedback2.Message;
            Feedback result = _feedbackRepository.UpdateById(_feedback2, ValidFeedbackId);
            result.Should().BeEquivalentTo(updatedFeedback, options => options.Excluding(feedback => feedback.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_InvalidId_Test() => _feedbackRepository
            .Invoking(repository => repository.UpdateById(_feedback2, InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId));

        [Fact] public void DeleteById_ValidId_Test()
        {
            Feedback result = _feedbackRepository.DeleteById(ValidFeedbackId);
            result.Should().BeEquivalentTo(_feedback1, options => options.Excluding(feedback => feedback.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void DeleteById_InvalidId_Test() => _feedbackRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId));
    }
}