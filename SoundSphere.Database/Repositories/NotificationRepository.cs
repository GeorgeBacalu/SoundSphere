using Microsoft.EntityFrameworkCore;
using SoundSphere.Database.Context;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Extensions;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;

namespace SoundSphere.Database.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context) => _context = context;

        public async Task<List<Notification>> GetAllAsync(NotificationPaginationRequest payload) => await _context.Notifications
            .Include(notification => notification.Sender)
            .Include(notification => notification.Receiver)
            .Where(notification => notification.DeletedAt == null)
            .ApplyPagination(payload)
            .ToListAsync();

        public async Task<Notification> GetByIdAsync(Guid id) => await _context.Notifications
            .Include(notification => notification.Sender)
            .Include(notification => notification.Receiver)
            .Where(notification => notification.DeletedAt == null)
            .SingleOrDefaultAsync(notification => notification.Id == id)
            ?? throw new ResourceNotFoundException(string.Format(NotificationNotFound, id));

        public async Task<Notification> AddAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification> UpdateByIdAsync(Notification notification, Guid id)
        {
            Notification notificationToUpdate = await GetByIdAsync(id);
            notificationToUpdate.Type = notification.Type;
            notificationToUpdate.Message = notification.Message;
            notificationToUpdate.IsRead = notification.IsRead;
            if (_context.Entry(notificationToUpdate).State == EntityState.Modified)
                notificationToUpdate.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return notificationToUpdate;
        }

        public async Task<Notification> DeleteByIdAsync(Guid id)
        {
            Notification notificationToDelete = await GetByIdAsync(id);
            notificationToDelete.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return notificationToDelete;
        }

        public void LinkNotificationToSender(Notification notification)
        {
            if (_context.Users.Find(notification.SenderId) is User existingSender)
                notification.Sender = _context.Attach(existingSender).Entity;
        }

        public void LinkNotificationToReceiver(Notification notification)
        {
            if (_context.Users.Find(notification.ReceiverId) is User existingReceiver)
                notification.Receiver = _context.Attach(existingReceiver).Entity;
        }
    }
}