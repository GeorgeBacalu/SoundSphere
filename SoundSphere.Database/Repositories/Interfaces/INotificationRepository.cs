using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetAllAsync(NotificationPaginationRequest payload);

        Task<Notification> GetByIdAsync(Guid id);

        Task<Notification> AddAsync(Notification notification);

        Task<Notification> UpdateByIdAsync(Notification notification, Guid id);

        Task<Notification> DeleteByIdAsync(Guid id);

        void LinkNotificationToSender(Notification notification);

        void LinkNotificationToReceiver(Notification notification);
    }
}