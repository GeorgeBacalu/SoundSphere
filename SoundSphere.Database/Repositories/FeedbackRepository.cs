using Microsoft.EntityFrameworkCore;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;

namespace SoundSphere.Database.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext _context;

        public FeedbackRepository(AppDbContext context) => _context = context;

        public List<Feedback> GetAll() => _context.Feedbacks
            .Include(feedback => feedback.User)
            .Where(feedback => feedback.DeletedAt == null)
            .OrderBy(feedback => feedback.CreatedAt)
            .ToList();

        public Feedback GetById(Guid id) => _context.Feedbacks
            .Include(feedback => feedback.User)
            .Where(feedback => feedback.DeletedAt == null)
            .SingleOrDefault(feedback => feedback.Id == id)
            ?? throw new ResourceNotFoundException(string.Format(FeedbackNotFound, id));

        public Feedback Add(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return feedback;
        }

        public Feedback UpdateById(Feedback feedback, Guid id)
        {
            Feedback feedbackToUpdate = GetById(id);
            feedbackToUpdate.Type = feedback.Type;
            feedbackToUpdate.Message = feedback.Message;
            if (_context.Entry(feedbackToUpdate).State == EntityState.Modified)
                feedbackToUpdate.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return feedbackToUpdate;
        }

        public Feedback DeleteById(Guid id)
        {
            Feedback feedbackToDelete = GetById(id);
            feedbackToDelete.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return feedbackToDelete;
        }

        public void LinkFeedbackToUser(Feedback feedback)
        {
            if (_context.Users.Find(feedback.User.Id) is User existingUser)
                feedback.User = _context.Attach(existingUser).Entity;
        }
    }
}