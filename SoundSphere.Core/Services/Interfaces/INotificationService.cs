using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> GetAllAsync(NotificationPaginationRequest payload);

        Task<NotificationDto> GetByIdAsync(Guid id);

        Task<NotificationDto> AddAsync(NotificationDto notificationDto);

        Task<NotificationDto> UpdateByIdAsync(NotificationDto notificationDto, Guid id);

        Task<NotificationDto> DeleteByIdAsync(Guid id);
    }
}