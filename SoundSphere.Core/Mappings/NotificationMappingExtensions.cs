using AutoMapper;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;

namespace SoundSphere.Core.Mappings
{
    public static class NotificationMappingExtensions
    {
        public static List<NotificationDto> ToDtos(this List<Notification> notifications, IMapper mapper) => notifications.Select(notification => notification.ToDto(mapper)).ToList();

        public static async Task<List<Notification>> ToEntitiesAsync(this List<NotificationDto> notificationDtos, IUserRepository userRepository, IMapper mapper) =>
            (await Task.WhenAll(notificationDtos.Select(notificationDto => notificationDto.ToEntityAsync(userRepository, mapper)))).ToList();

        public static NotificationDto ToDto(this Notification notification, IMapper mapper) => mapper.Map<NotificationDto>(notification);

        public static async Task<Notification> ToEntityAsync(this NotificationDto notificationDto, IUserRepository userRepository, IMapper mapper)
        {
            Notification notification = mapper.Map<Notification>(notificationDto);
            notification.Sender = await userRepository.GetByIdAsync(notification.SenderId);
            notification.Receiver = await userRepository.GetByIdAsync(notification.ReceiverId);
            return notification;
        }
    }
}