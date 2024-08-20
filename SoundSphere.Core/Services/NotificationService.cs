using AutoMapper;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;

namespace SoundSphere.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository, IMapper mapper) =>
            (_notificationRepository, _userRepository, _mapper) = (notificationRepository, userRepository, mapper);

        public async Task<List<NotificationDto>> GetAllAsync(NotificationPaginationRequest payload) => (await _notificationRepository.GetAllAsync(payload)).ToDtos(_mapper);

        public async Task<NotificationDto> GetByIdAsync(Guid id) => (await _notificationRepository.GetByIdAsync(id)).ToDto(_mapper);

        public async Task<NotificationDto> AddAsync(NotificationDto notificationDto)
        {
            Notification notification = await notificationDto.ToEntityAsync(_userRepository, _mapper);
            _notificationRepository.LinkNotificationToSender(notification);
            _notificationRepository.LinkNotificationToReceiver(notification);
            return (await _notificationRepository.AddAsync(notification)).ToDto(_mapper);
        }

        public async Task<NotificationDto> UpdateByIdAsync(NotificationDto notificationDto, Guid id) => (await _notificationRepository.UpdateByIdAsync(await notificationDto.ToEntityAsync(_userRepository, _mapper), id)).ToDto(_mapper);

        public async Task<NotificationDto> DeleteByIdAsync(Guid id) => (await _notificationRepository.DeleteByIdAsync(id)).ToDto(_mapper);
    }
}