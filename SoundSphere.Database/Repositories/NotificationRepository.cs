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

        public List<Notification> GetAll(NotificationPaginationRequest payload) => _context.Notifications
            .Include(notification => notification.Sender)
            .Include(notification => notification.Receiver)
            .Where(notification => notification.DeletedAt == null)
            .ApplyPagination(payload)
            .ToList();

        public Notification GetById(Guid id) => _context.Notifications
            .Include(notification => notification.Sender)
            .Include(notification => notification.Receiver)
            .Where(notification => notification.DeletedAt == null)
            .SingleOrDefault(notification => notification.Id == id)
            ?? throw new ResourceNotFoundException(string.Format(NotificationNotFound, id));

        public Notification Add(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            return notification;
        }

        public Notification UpdateById(Notification notification, Guid id)
        {
            Notification notificationToUpdate = GetById(id);
            notificationToUpdate.Type = notification.Type;
            notificationToUpdate.Message = notification.Message;
            notificationToUpdate.IsRead = notification.IsRead;
            if (_context.Entry(notificationToUpdate).State == EntityState.Modified)
                notificationToUpdate.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return notificationToUpdate;
        }

        public Notification DeleteById(Guid id)
        {
            Notification notificationToDelete = GetById(id);
            notificationToDelete.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
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