using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        List<Notification> GetAll(NotificationPaginationRequest payload);

        Notification GetById(Guid id);

        Notification Add(Notification notification);

        Notification UpdateById(Notification notification, Guid id);

        Notification DeleteById(Guid id);

        void LinkNotificationToSender(Notification notification);

        void LinkNotificationToReceiver(Notification notification);
    }
}