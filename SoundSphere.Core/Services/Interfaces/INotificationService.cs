using SoundSphere.Database.Dtos.Common;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface INotificationService
    {
        List<NotificationDto> GetAll();

        NotificationDto GetById(Guid id);

        NotificationDto Add(NotificationDto notificationDto);

        NotificationDto UpdateById(NotificationDto notificationDto, Guid id);

        NotificationDto DeleteById(Guid id);
    }
}