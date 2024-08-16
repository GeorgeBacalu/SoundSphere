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

        private void Execute(Action<FeedbackService, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var feedbackService = new FeedbackService(new FeedbackRepository(context), new UserRepository(context), _mapper);
            using var transaction = context.Database.BeginTransaction();
            _dbFixture.TrackAndAddEntities(context, _users);
            _dbFixture.TrackAndAddEntities(context, _feedbacks);
            action(feedbackService, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((feedbackService, context) => feedbackService.GetAll(_feedbackPayload).Should().BeEquivalentTo(_feedbackDtosPagination));

        [Fact] public void GetById_ValidId_Test() => Execute((feedbackService, context) => feedbackService.GetById(ValidFeedbackId).Should().BeEquivalentTo(_feedbackDtos[0]));

        [Fact] public void GetById_InvalidId_Test() => Execute((feedbackService, context) => feedbackService
            .Invoking(service => service.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((feedbackService, context) =>
        {
            FeedbackDto result = feedbackService.Add(_newFeedbackDto);
            context.Feedbacks.Find(result.Id).Should().BeEquivalentTo(_newFeedback, options => options.Excluding(feedback => feedback.Id).Excluding(feedback => feedback.CreatedAt).Excluding(feedback => feedback.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((feedbackService, context) =>
        {
            Feedback updatedFeedback = _feedbacks[0];
            updatedFeedback.Type = _feedbacks[1].Type;
            updatedFeedback.Message = _feedbacks[1].Message;
            FeedbackDto updatedFeedbackDto = updatedFeedback.ToDto(_mapper);
            FeedbackDto result = feedbackService.UpdateById(_feedbackDtos[1], ValidFeedbackId);
            result.Should().BeEquivalentTo(updatedFeedbackDto, options => options.Excluding(feedback => feedback.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((feedbackService, context) => feedbackService
            .Invoking(service => service.UpdateById(_feedbackDtos[1], InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((feedbackService, context) =>
        {
            FeedbackDto result = feedbackService.DeleteById(ValidFeedbackId);
            result.Should().BeEquivalentTo(_feedbackDtos[0], options => options.Excluding(feedback => feedback.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((feedbackService, context) => feedbackService
            .Invoking(service => service.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(FeedbackNotFound, InvalidId)));
    }
}