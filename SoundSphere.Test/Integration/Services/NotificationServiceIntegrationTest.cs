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
using static SoundSphere.Test.Mocks.NotificationMock;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Integration.Services
{
    public class NotificationServiceIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;
        private readonly IMapper _mapper;
        private readonly Notification _notification1 = GetNotification1();
        private readonly Notification _notification2 = GetNotification2();
        private readonly Notification _newNotification = GetNewNotification();
        private readonly List<Notification> _notifications = GetNotifications();
        private readonly NotificationDto _notificationDto1 = GetNotificationDto1();
        private readonly NotificationDto _notificationDto2 = GetNotificationDto2();
        private readonly NotificationDto _newNotificationDto = GetNewNotificationDto();
        private readonly List<NotificationDto> _notificationDtos = GetNotificationDtos();
        private readonly List<User> _users = GetUsers();

        public NotificationServiceIntegrationTest(DbFixture dbFixture) => (_dbFixture, _mapper) = (dbFixture, new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper());

        private void Execute(Action<NotificationService, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var notificationService = new NotificationService(new NotificationRepository(context), new UserRepository(context), _mapper);
            using var transaction = context.Database.BeginTransaction();
            _dbFixture.TrackAndAddEntities(context, _users);
            _dbFixture.TrackAndAddEntities(context, _notifications);
            action(notificationService, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((notificationService, context) => notificationService.GetAll().Should().BeEquivalentTo(_notificationDtos));

        [Fact] public void GetById_ValidId_Test() => Execute((notificationService, context) => notificationService.GetById(ValidNotificationId).Should().BeEquivalentTo(_notificationDto1));

        [Fact] public void GetById_InvalidId_Test() => Execute((notificationService, context) => notificationService
            .Invoking(service => service.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((notificationService, context) =>
        {
            NotificationDto result = notificationService.Add(_newNotificationDto);
            context.Notifications.Find(result.Id).Should().BeEquivalentTo(_newNotification, options => options.Excluding(notification => notification.Id).Excluding(notification => notification.CreatedAt).Excluding(notification => notification.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((notificationService, context) =>
        {
            Notification updatedNotification = _notification1;
            updatedNotification.Type = _notification2.Type;
            updatedNotification.Message = _notification2.Message;
            updatedNotification.IsRead = _notification2.IsRead;
            NotificationDto updatedNotificationDto = updatedNotification.ToDto(_mapper);
            NotificationDto result = notificationService.UpdateById(_notificationDto2, ValidNotificationId);
            result.Should().BeEquivalentTo(updatedNotificationDto, options => options.Excluding(notification => notification.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((notificationService, context) => notificationService
            .Invoking(service => service.UpdateById(_notificationDto2, InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((notificationService, context) =>
        {
            NotificationDto result = notificationService.DeleteById(ValidNotificationId);
            result.Should().BeEquivalentTo(_notificationDto1, options => options.Excluding(notification => notification.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((notificationService, context) => notificationService
            .Invoking(service => service.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(NotificationNotFound, InvalidId)));
    }
}