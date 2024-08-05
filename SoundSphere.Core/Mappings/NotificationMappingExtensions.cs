using AutoMapper;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;

namespace SoundSphere.Core.Mappings
{
    public static class NotificationMappingExtensions
    {
        public static List<NotificationDto> ToDtos(this List<Notification> notifications, IMapper mapper) => notifications.Select(notification => notification.ToDto(mapper)).ToList();

        public static List<Notification> ToEntities(this List<NotificationDto> notificationDtos, IUserRepository userRepository, IMapper mapper) =>
            notificationDtos.Select(notificationDto => notificationDto.ToEntity(userRepository, mapper)).ToList();

        public static NotificationDto ToDto(this Notification notification, IMapper mapper) => mapper.Map<NotificationDto>(notification);

        public static Notification ToEntity(this NotificationDto notificationDto, IUserRepository userRepository, IMapper mapper)
        {
            Notification notification = mapper.Map<Notification>(notificationDto);
            notification.Sender = userRepository.GetById(notification.SenderId);
            notification.Receiver = userRepository.GetById(notification.ReceiverId);
            return notification;
        }
    }
}