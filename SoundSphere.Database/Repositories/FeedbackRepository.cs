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
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext _context;

        public FeedbackRepository(AppDbContext context) => _context = context;

        public async Task<List<Feedback>> GetAllAsync(FeedbackPaginationRequest payload) => await _context.Feedbacks
            .Include(feedback => feedback.User)
            .Where(feedback => feedback.DeletedAt == null)
            .ApplyPagination(payload)
            .ToListAsync();

        public async Task<Feedback> GetByIdAsync(Guid id) => await _context.Feedbacks
            .Include(feedback => feedback.User)
            .Where(feedback => feedback.DeletedAt == null)
            .SingleOrDefaultAsync(feedback => feedback.Id == id)
            ?? throw new ResourceNotFoundException(string.Format(FeedbackNotFound, id));

        public async Task<Feedback> AddAsync(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }

        public async Task<Feedback> UpdateByIdAsync(Feedback feedback, Guid id)
        {
            Feedback feedbackToUpdate = await GetByIdAsync(id);
            feedbackToUpdate.Type = feedback.Type;
            feedbackToUpdate.Message = feedback.Message;
            if (_context.Entry(feedbackToUpdate).State == EntityState.Modified)
                feedbackToUpdate.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return feedbackToUpdate;
        }

        public async Task<Feedback> DeleteByIdAsync(Guid id)
        {
            Feedback feedbackToDelete = await GetByIdAsync(id);
            feedbackToDelete.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return feedbackToDelete;
        }

        public void LinkFeedbackToUser(Feedback feedback)
        {
            if (_context.Users.Find(feedback.User.Id) is User existingUser)
                feedback.User = _context.Attach(existingUser).Entity;
        }
    }
}